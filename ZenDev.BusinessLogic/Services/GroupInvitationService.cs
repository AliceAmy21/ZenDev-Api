using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Helpers;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class GroupInvitationService : IGroupInvitationService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<GroupService> _logger;
        private readonly IGroupService _groupService;

        public GroupInvitationService(
            ZenDevDbContext dbContext,
            ILogger<GroupService> logger,
            IGroupService groupService) 
        {
            _dbContext = dbContext;
            _logger = logger;
            _groupService = groupService;
        }

        public async Task<List<NotificationModel>> GetGroupInvitationsByUserIdAsync(long userId)
        {
            var inivtations = await _dbContext.GroupInvitations
                .Where(invitation => invitation.InvitedUserId == userId)
                .ToListAsync();

            var notificationModels = new List<NotificationModel>();

            var invitedUser = getUserById(userId);

            foreach (var i in inivtations)
            {
                var inviteSender = getUserById(i.InviteSenderId);
                var group = await _groupService.getGroupByIdAsync(i.GroupId);

                var model = new NotificationModel
                {
                    InvitedUserId = invitedUser.UserId,
                    InvitedUserName = invitedUser.UserName,
                    InviteSenderId = i.InviteSenderId,
                    InviteSenderUserName = inviteSender.UserName,
                    InviteSenderAvatarUrlIcon = inviteSender.AvatarIconUrl,
                    groupId = group.GroupId,
                    groupName = group.GroupName,
                };

                notificationModels.Add(model);
            }
          

            return notificationModels;
        }

        public async Task<List<GroupInvitationEntity>> CreateGroupInvitationsAsync(List<GroupInvitationEntity> groupInvitations)
        {
            try
            {
                await _dbContext.AddRangeAsync(groupInvitations);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create group invitations.");
                return [];
            }

            _logger.LogInformation("Group invitations created successfully");
            return groupInvitations;
        }

        public GroupInvitationEntity GetGroupInvitationById(long id)
        {
            var invitation = _dbContext.GroupInvitations
                .FirstOrDefault(invite => invite.GroupInvitationId == id);

            return invitation;
        }

        public async Task<List<UserInviteModel>> GetNonGroupMembers(long groupId)
        {
            var groupMembers = await _dbContext.UserGroupBridge
                .Where(userGroup => userGroup.GroupId == groupId)
                .ToListAsync();

            List<UserEntity> users = GetAllUsers();

            List<UserInviteModel> nonGroupMembers = [];

            foreach (var member in groupMembers)
            {
                UserEntity user = users.Find(user => user.UserId == member.UserId);

                if (user != null)
                {
                    users.Remove(user);
                }
                
            }

            foreach (var user in users)
            {
                UserInviteModel userInviteModel = new()
                {
                    UserId = user.UserId,
                    UserName = user.UserName
                };

                nonGroupMembers.Add(userInviteModel);
            }

            return nonGroupMembers;
        }

        public UserEntity getUserById(long userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public List<UserEntity> GetAllUsers()
        {
            var result = _dbContext.Users.ToList();
            return result;
        }

        public async Task<List<UserInviteModel>> GetAllUsersAsync(GroupInvitationQueryObject query)
        {
            if (query.UserToExclude > 0 && query.GroupId < 0)
            {
                var usersQuery = _dbContext.Users
               .Where(user => user.UserId != query.UserToExclude)
               .AsQueryable();

                if (!string.IsNullOrEmpty(query.SearchQuery)) //search query
                {
                    usersQuery = usersQuery.Where(user => user.UserName.Contains(query.SearchQuery));
                }

                var usersToInvite = usersQuery.Select(user => new UserInviteModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    AvatarIconUrl = user.AvatarIconUrl,
                });

                var skipNumber = (query.PageNumber - 1) * query.PageSize;

                return await usersToInvite
                    .Skip(skipNumber)
                    .Take(query.PageSize)
                    .ToListAsync();
            }
            else
            {
                var groupMembers = await _groupService.GetGroupMembers(query.GroupId);

                var usersToExclude = groupMembers.Select(user => user.UserId);

                var usersQuery = _dbContext.Users
                    .Where(user => !usersToExclude.Contains(user.UserId))
                    .AsQueryable();

                if (!string.IsNullOrEmpty(query.SearchQuery)) //search query
                {
                    usersQuery = usersQuery.Where(user => user.UserName.Contains(query.SearchQuery));
                }

                var usersToInvite = usersQuery.Select(user => new UserInviteModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    AvatarIconUrl = user.AvatarIconUrl,
                });

                var skipNumber = (query.PageNumber - 1) * query.PageSize;

                return await usersToInvite
                    .Skip(skipNumber)
                    .Take(query.PageSize)
                    .ToListAsync();
            }

        }

        public async Task<ResultModel> DeleteGroupInvitationAsync(GroupInvitationEntity groupInvitationEntity)
        {
            var result = new ResultModel()
            {
                Success = false
            };

            try
            {
                var recordToRemove = _dbContext.GroupInvitations.FirstOrDefault(record => record.GroupId == groupInvitationEntity.GroupId &&
                                      record.InvitedUserId == groupInvitationEntity.InvitedUserId);
                if (recordToRemove != null)
                {
                    _dbContext.Remove(recordToRemove);
                    await _dbContext.SaveChangesAsync();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to remove user from group invitation table");
                result.ErrorMessages = new List<string>() { "Failed to remove user from group invitation table" };
                return result;
            }

            return result;
        }

        public async Task<ResultModel> AcceptGroupInvitationAsync(UserGroupBridgeEntity userGroupBridgeEntity)
        {
            var result = new ResultModel()
            {
                Success = false
            };

            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try 
            {
                //Add user to UserGroupBridge
                UserGroupBridgeEntity userGroupBridge = new()
                {
                    GroupAdmin = false,
                    UserId = userGroupBridgeEntity.UserId,
                    GroupId = userGroupBridgeEntity.GroupId,
                };
                var addUserGroupBridge = await _groupService.CreateUserGroupBridgeAsync(userGroupBridge);

                //Remove user from GroupInvitation
                var groupInvitationEntity = new GroupInvitationEntity()
                {
                    InvitedUserId = userGroupBridgeEntity.UserId,
                    GroupId = userGroupBridgeEntity.GroupId,
                };
                var deleteGroupInvitation = await DeleteGroupInvitationAsync(groupInvitationEntity);

                //Update member count in Groups table
                var group = _dbContext.Groups.FirstOrDefault(group => group.GroupId == groupInvitationEntity.GroupId);

                if (group != null)
                {
                    group.MemberCount++;
                }

                // Save changes
                await _dbContext.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                result.Success = true;
            }
            catch(Exception ex)
            {
                //Roll back on fail
                await transaction.RollbackAsync();

                result.ErrorMessages = new List<string> { "Failed to accept invitation" };
                _logger.LogError(ex, "Failed to accept invitation");
                return result;
            }

            return result;
        }
    }
}