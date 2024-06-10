using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IGroupInvitationService
    {
        public Task<GroupInvitationEntity> CreateGroupInvitationAsync(GroupInvitationEntity groupInvitation);
        public Task<List<GroupInvitationEntity?>> getGroupInvitationsByUserIdAsync(long userId);
        public Task<List<UserInviteModel>> getNonGroupMembers(long groupId);
    }
}
