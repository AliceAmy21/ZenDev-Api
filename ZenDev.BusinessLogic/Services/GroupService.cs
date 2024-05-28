using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Helpers;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        private readonly ZenDevDbContext _dbContext;

        public GroupService(ZenDevDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<GroupEntity>> getAllGroupsAsync(GroupQueryObject query, long userId)
        {
            var groups = _dbContext.Groups
                .Include(group => group.ExerciseTypeEntity)
                .AsQueryable();

       
            if(query.showMyGroups.Equals(true)) // My groups: Filter to show groups that the user belongs to
            {
                groups = groups.Where(group => group.UserGroupBridgeEntities.Any(ug => ug.UserId == userId));
            }
            else if(query.showMyGroups.Equals(false)) // Available groups: Filter to show groups that the user does not belong to
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

            return await groups.ToListAsync();
        }

        public async Task<List<GroupEntity>> getAvailableGroupsAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<GroupEntity?> getGroupByIdAsync(long groupId)
        {
            return await _dbContext.Groups.FindAsync(groupId);
        }
    }
}
