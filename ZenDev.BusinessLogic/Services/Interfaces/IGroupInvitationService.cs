using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IGroupInvitationService
    {
        public Task<List<GroupInvitationEntity?>> getGroupInvitationsByUserIdAsync(long userId);
    }
}
