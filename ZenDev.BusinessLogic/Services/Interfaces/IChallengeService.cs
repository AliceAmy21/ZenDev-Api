using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;


namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IChallengeService
    {
        public Task<ChallengeEntity> CreateChallengeAsync(ChallengeEntity challenge, long UserId);

        public List<ChallengeListModel> GetChallengesForGroupAsync(long groupId);

        public List<ChallengeListModel> GetChallengesForUserAsync(long userId);

        public Task<ChallengeEntity> UpdateChallengeAsync(ChallengeEntity challenge);

        public List<UserChallengeBridgeEntity> GetUsersForChallengeAsync(long challengeId);

        public List<UserChallengeBridgeEntity> GetUsersToInviteChallengeAsync(long challengeId, long userId);

        public Task<ChallengeEntity> GetChallengeByIdAsync(long ChallengeId);

        public Task<ChallengeEntity> AddUserToChallengeAsync(long challengeId,long userId);

        public Task<ChallengeEntity> RemoveUserFromChallengeAsync(long challengeId,long userId);
    }
}