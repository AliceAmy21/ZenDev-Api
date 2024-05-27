using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<List<GroupEntity>> getAllGroupsAsync(long userId);

        public Task<List<GroupEntity>> getAvailableGroupsAsync(long userId);

        public Task<GroupEntity?> getGroupByIdAsync(long groupId);
    }
}
