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

       
            if(query.ShowMyGroups.Equals(true) && userId > 0) // My groups: Filter to show groups that the user belongs to
            {
                groups = groups.Where(group => group.UserGroupBridgeEntities.Any(ug => ug.UserId == userId));
            }
            else if(query.ShowMyGroups.Equals(false) && userId > 0) // Available groups: Filter to show groups that the user does not belong to
            {
                groups = groups.Where(group => group.UserGroupBridgeEntities.Any(ug => ug.UserId != userId));
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

        public async Task<List<ExerciseTypeEntity>> GetGroupExercisesAsync()
        {
            var result = await _dbContext.ExerciseTypes.ToListAsync();

            return result;
        }

        public async Task<List<UserInviteModel>> GetGroupMembers(long groupId)
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
    }
}
