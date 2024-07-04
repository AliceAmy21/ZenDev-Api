using ZenDev.BusinessLogic.Models;
using ZenDev.Common.Helpers;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<List<GroupEntity>> getAllGroupsAsync(GroupQueryObject query, long userId);

        public Task<GroupEntity?> getGroupByIdAsync(long groupId);

        public Task<GroupResultModel> CreateGroupAsync(GroupResultModel groupResult);

        public Task<UserGroupBridgeEntity> CreateUserGroupBridgeAsync(UserGroupBridgeEntity userGroupBridge);

        public Task<GroupEntity> UpdateGroupAsync(GroupEntity group);

        public Task<long> DeleteGroupAsync(long groupId);

        public Task<List<ExerciseTypeEntity>> GetGroupExercisesAsync();

         public Task<List<UserInviteModel>> GetGroupMembers(long groupId);
    }
}
