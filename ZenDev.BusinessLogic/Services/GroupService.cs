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

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                groups = groups.Where(group => group.ExerciseTypeEntity.ExerciseTypeId == query.GroupExerciseTypeId);
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
                   // "members" => groups.OrderByDescending(g => g.Members), 
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

        public async Task<GroupEntity> CreateGroupAsync(GroupEntity group)
        {
            try
            {
                await _dbContext.AddAsync(group);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create group");
                return new GroupEntity();
            }

            GroupEntity newGroup = getGroupById(group.GroupId);

            return newGroup;
        }
    }
}
