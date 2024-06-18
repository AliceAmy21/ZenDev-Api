using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;


namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IChallengeService
    {
        public Task<ChallengeEntity> CreateChallengeAsync(ChallengeCreationModel challenge, long UserId);

        public List<List<ChallengeListModel>> GetChallengesForGroupAsync(long groupId, long userId);

        public List<ChallengeListModel> GetChallengesForUserAsync(long userId);

        public Task<ChallengeEntity> UpdateChallengeAsync(ChallengeUpdateModel challenge);

        public List<UserChallengeBridgeEntity> GetUsersForChallengeAsync(long challengeId);

        public List<UserChallengeBridgeEntity> GetUsersToInviteChallengeAsync(long challengeId, long userId);

        public Task<ChallengeEntity> GetChallengeByIdAsync(long ChallengeId);

        public Task<ChallengeEntity> AddUserToChallengeAsync(long challengeId,long userId);

        public Task<ChallengeEntity> RemoveUserFromChallengeAsync(long challengeId,long userId);
    }
}