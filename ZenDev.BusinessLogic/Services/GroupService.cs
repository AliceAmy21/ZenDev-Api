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
                .Include(e => e.ExerciseTypeEntity)
                .AsQueryable();

            if (query.GroupExerciseTypeId.HasValue)
            {
                groups = groups.Where(group => group.ExerciseTypeEntity.ExerciseTypeId == query.GroupExerciseTypeId);
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
