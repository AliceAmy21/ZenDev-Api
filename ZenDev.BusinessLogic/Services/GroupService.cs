using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        public async Task<List<GroupEntity>> getAllGroupsAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GroupEntity>> getAvailableGroupsAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<GroupEntity?> getGroupByIdAsync(long groupId)
        {
            throw new NotImplementedException();
        }
    }
}
