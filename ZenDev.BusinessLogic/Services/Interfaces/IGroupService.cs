using ZenDev.Common.Helpers;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<List<GroupEntity>> getAllGroupsAsync(GroupQueryObject query, long userId);

        public Task<GroupEntity?> getGroupByIdAsync(long groupId);

        public Task<GroupEntity> CreateGroupAsync(GroupEntity group, UserGroupBridgeEntity userGroupBridge);

        public Task<List<ExerciseTypeEntity>> GetGroupExercisesAsync();
    }
}
