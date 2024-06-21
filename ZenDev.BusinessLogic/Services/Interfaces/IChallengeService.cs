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

        public List<UserInviteModel> GetUsersForChallengeAsync(long challengeId);

        public List<UserInviteModel> GetUsersToInviteChallengeAsync(long challengeId, long groupId);

        public Task<ChallengeEntity> GetChallengeByIdAsync(long ChallengeId);

        public Task AddUserToChallengeAsync(long challengeId,long userId);

        public Task RemoveUserFromChallengeAsync(long challengeId,long userId);

        public Task<List<ExerciseEntity>> GetAllExercisesAsync();

        public Task<long> DeleteChallengeAsync(long challengeId);
    }
}