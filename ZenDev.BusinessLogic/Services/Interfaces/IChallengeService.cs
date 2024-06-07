using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;


namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IChallengeService
    {
        public Task<ChallengeEntity> CreateChallengeAsync(ChallengeEntity challenge);

        public List<ChallengeEntity> GetChallengesForGroupAsync(long groupId);

        public List<ChallengeEntity> GetChallengesForUserAsync(long userId);

        public Task<ChallengeEntity> UpdateChallengeAsync(ChallengeEntity challenge);

        public List<UserEntity> GetUsersForChallengeAsync(long challengeId);

        public List<UserEntity> GetUsersToInviteChallengeAsync(long challengeId, long userGroupBridgeId);

        public Task<ChallengeEntity> GetChallengeByIdAsync(long ChallengeId);

        public Task<ChallengeEntity> AddUserToChallengeAsync(long challengeId,long userGroupId);

        public Task<ChallengeEntity> RemoveUserFromChallengeAsync(long challengeId,long userId);
    }
}