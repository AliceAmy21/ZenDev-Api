using Microsoft.EntityFrameworkCore;
using ZenDev.BusinessLogic.Services.Interfaces;
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

        public async Task<List<GroupEntity>> getAllGroupsAsync(long userId)
        {
            return await _dbContext.Groups.ToListAsync();
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
