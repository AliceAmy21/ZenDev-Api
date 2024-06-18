using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Helpers;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ZenDev.BusinessLogic.Services
{
    public class GroupInvitationService : IGroupInvitationService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<GroupService> _logger;

        public GroupInvitationService(
            ZenDevDbContext dbContext,
            ILogger<GroupService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<GroupInvitationEntity?>> GetGroupInvitationsByUserIdAsync(long userId)
        {
            var result = await _dbContext.GroupInvitations
                .Where(invitation => invitation.UserId == userId)
                .ToListAsync();

            return result;
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
            var usersQuery = _dbContext.Users.Where(user => user.UserId != query.UserToExclude).AsQueryable();

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
}