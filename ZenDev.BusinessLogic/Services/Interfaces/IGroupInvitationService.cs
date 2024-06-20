using ZenDev.BusinessLogic.Models;
using ZenDev.Common.Helpers;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IGroupInvitationService
    {
        public Task<GroupInvitationEntity> CreateGroupInvitationAsync(GroupInvitationEntity groupInvitation);
        public Task<List<GroupInvitationEntity?>> GetGroupInvitationsByUserIdAsync(long userId);
        public Task<List<UserInviteModel>> GetNonGroupMembers(long groupId);
        public Task<List<UserInviteModel>> GetAllUsersAsync(GroupInvitationQueryObject query);
        public Task<ResultModel> DeleteGroupInvitationAsync(GroupInvitationEntity groupInvitationEntity);
        public Task<ResultModel> AcceptGroupInvitationAsync(UserGroupBridgeEntity userGroupBridgeEntity);
    }
}
