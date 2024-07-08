using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Helpers;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<GroupService> _logger;

        public GroupService(
            ZenDevDbContext dbContext,
            ILogger<GroupService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<GroupEntity>> getAllGroupsAsync(GroupQueryObject query, long userId)
        {
            var groups = _dbContext.Groups
                .Include(group => group.ExerciseTypeEntity)
                .AsQueryable();

            var mygroups = groups.Where(group => group.UserGroupBridgeEntities.Any(ug => ug.UserId == userId));

            if (query.ShowMyGroups.Equals(true) && userId > 0) // My groups: Filter to show groups that the user belongs to
            {
                groups = mygroups;
            }
            else if(query.ShowMyGroups.Equals(false) && userId > 0) // Available groups: Filter to show groups that the user does not belong to
            {
                groups = groups.Except(mygroups);
            }

            if(!string.IsNullOrEmpty(query.searchQuery))
            {
                groups = groups.Where(group => group.GroupName.Contains(query.searchQuery));
            }

            if (query.GroupExerciseTypeId.HasValue)
            {
                groups = groups.Where(group => group.ExerciseTypeEntity.ExerciseTypeId == query.GroupExerciseTypeId);
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                groups = query.SortBy switch
                {
                    "name" => groups.OrderBy(g => g.GroupName),
                    "members" => groups.OrderByDescending(g => g.MemberCount), 
                    "exercise" => groups.OrderBy(g => g.ExerciseTypeEntity.ExerciseType),
                    _ => groups
                };
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await groups
                .Skip(skipNumber)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<GroupEntity?> getGroupByIdAsync(long groupId)
        {
            return await _dbContext.Groups.FindAsync(groupId);
        }

        public GroupEntity getGroupById(long groupId)
        {
            return _dbContext.Groups.Find(groupId);
        }

        public UserGroupBridgeEntity getUserGroupBridgeById(long userGroupBridge)
        {
            return _dbContext.UserGroupBridge.Find(userGroupBridge);
        }

        public async Task<GroupResultModel> CreateGroupAsync(GroupResultModel groupResult)
        {
            GroupEntity group = new()
            {
                GroupName = groupResult.GroupName,
                GroupDescription = groupResult.GroupDescription,
                GroupIconUrl = groupResult.GroupIconUrl,
                ExerciseTypeId = groupResult.ExerciseType.ExerciseTypeId,
                MemberCount = groupResult.MemberCount,
            };

            UserGroupBridgeEntity userGroupBridge = new()
            {
                GroupAdmin = groupResult.GroupAdmin,
                UserId = groupResult.UserId,
            };

            try
            {
                await _dbContext.AddAsync(group);
                await _dbContext.SaveChangesAsync();
                userGroupBridge.GroupId = group.GroupId;
                UserGroupBridgeEntity newUserGroupBridge = CreateUserGroupBridge(userGroupBridge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create group");
                return new GroupResultModel();
            }

            groupResult.GroupId = group.GroupId;
            return groupResult;
        }

        public UserGroupBridgeEntity CreateUserGroupBridge(UserGroupBridgeEntity userGroupBridge)
        {
            try
            {
                _dbContext.Add(userGroupBridge);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create User-Group-Bridge entry");
                return new UserGroupBridgeEntity();
            }

            UserGroupBridgeEntity newUserGroupBridge = getUserGroupBridgeById(userGroupBridge.UserGroupId);

            return newUserGroupBridge;
        }

        public async Task<GroupEntity> UpdateGroupAsync(GroupEntity group)
        {
            try
            {
                _dbContext.Update(group);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update group");
                return new GroupEntity();
            }

            return group;
        }

        public async Task<long> DeleteGroupAsync(long groupId)
        {
            try
            {
                var recordToRemove = _dbContext.Groups.FirstOrDefault(group => group.GroupId == groupId);
                if (recordToRemove != null)
                {
                    _dbContext.Remove(recordToRemove);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete personal group");
                return groupId;
            }

            return groupId;
        }

        public async Task<UserGroupResultModel> LeaveGroupAsync(UserGroupResultModel userGroup)
        {
            userGroup.Success = false;

            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try 
            {
                //Delete UserGroupBridge
                 var userGroupBridge = _dbContext.UserGroupBridge.FirstOrDefault(bridge => bridge.GroupId == userGroup.GroupId && bridge.UserId == userGroup.UserId);
                _dbContext.UserGroupBridge.Remove(userGroupBridge);

                //Update member count in Groups table
                var group = _dbContext.Groups.FirstOrDefault(group => group.GroupId == userGroup.GroupId);
                if (group != null)
                {
                    group.MemberCount--;
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                userGroup.Success = true;
            }
            catch(Exception ex)
            {
                //Roll back on fail
                await transaction.RollbackAsync();

                userGroup.ErrorMessages = new List<string> { "Failed to leave group" };
                _logger.LogError(ex, "Failed to leave group");
                return userGroup;
            }

            return userGroup;
        }

        public async Task<UserGroupBridgeEntity> CreateUserGroupBridgeAsync(UserGroupBridgeEntity userGroupBridge)
        {
            try
            {
                await _dbContext.AddAsync(userGroupBridge);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create User-Group-Bridge entry");
                return new UserGroupBridgeEntity();
            }

            UserGroupBridgeEntity newUserGroupBridge = getUserGroupBridgeById(userGroupBridge.UserGroupId);

            return newUserGroupBridge;
        }

        public async Task<List<ExerciseTypeEntity>> GetGroupExercisesAsync()
        {
            var result = await _dbContext.ExerciseTypes.ToListAsync();

            return result;
        }

        public async Task<List<UserInviteModel>> GetGroupMembers(long? groupId)
        {
            var groupMembers = await _dbContext.UserGroupBridge
                .Where(userGroup => userGroup.GroupId == groupId)
                .ToListAsync();

            List<UserEntity> users = GetAllUsers();

            List<UserInviteModel> members = [];

            foreach (var member in groupMembers)
            {
                UserEntity user = users.Find(user => user.UserId == member.UserId);

                UserInviteModel userInviteModel = new()
                {
                    UserId = user.UserId,
                    UserName = user.UserName
                };

                members.Add(userInviteModel);
                
            }

            return members;
        }

        public List<UserEntity> GetAllUsers()
        {
            var result = _dbContext.Users.ToList();
            return result;
        }

        public async Task<UserGroupBridgeEntity> GetUserGroupBridgeByUserAndGroupIdAsync(long userId, long groupId)
        {
            var userGroups = await _dbContext.UserGroupBridge
                .FirstOrDefaultAsync(userGroupBridge => userGroupBridge.UserId == userId && userGroupBridge.GroupId == groupId);

            return userGroups;
        }
    }
}
