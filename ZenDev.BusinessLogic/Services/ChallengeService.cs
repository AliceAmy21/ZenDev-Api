using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;
using ZenDev.BusinessLogic.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZenDev.BusinessLogic.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<ChallengeService> _logger;
        
        public ChallengeService(
            ZenDevDbContext dbContext,
            ILogger<ChallengeService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ChallengeEntity> AddUserToChallengeAsync(long challengeId, long userId)
        {
            var challengeBridge = _dbContext.UserChallengeBridge
                .AsQueryable();
            if(challengeId>0 && userId>0){
                if(!(challengeBridge.Any(user=>user.UserId == userId) && challengeBridge.Any(challenge => challenge.ChallengeId == challengeId))){
                    UserChallengeBridgeEntity userChallengeBridgeEntity = new UserChallengeBridgeEntity()
                    {
                        UserId = userId,
                        ChallengeId = challengeId
                    };
                    try{
                        await _dbContext.UserChallengeBridge.AddAsync(userChallengeBridgeEntity);
                        await _dbContext.SaveChangesAsync();

                    }
                    catch(Exception ex){
                        _logger.LogError(ex,"Failed to add user");
                    }
                    return await GetChallengeByIdAsync(challengeId);
                }
                return await GetChallengeByIdAsync(challengeId);
            }
            return null;
        }


        public async Task<ChallengeEntity> CreateChallengeAsync(ChallengeCreationModel challenge, long UserId)
        {
            var groups = _dbContext.Groups.Include(exercise=>exercise.ExerciseTypeEntity).AsQueryable();

            ChallengeEntity challenge1 = new()
            {
                ChallengeEndDate = challenge.ChallengeEndDate,
                ChallengeStartDate = challenge.ChallengeStartDate,
                AmountToComplete = challenge.AmountToComplete,
                ExerciseId = challenge.ExerciseId,
                ExerciseEntity = _dbContext.Exercises.Find(challenge.ExerciseId),
                GroupId = challenge.GroupId,
                GroupEntity = groups.First(group=> group.GroupId == challenge.GroupId)
            };

            try{
                await _dbContext.AddAsync(challenge1);
                await _dbContext.SaveChangesAsync();
                UserChallengeBridgeEntity userChallengeBridgeEntity = new(){
                UserId = UserId,
                ChallengeId = challenge1.ChallengeId
                };
                await _dbContext.AddAsync(userChallengeBridgeEntity);
                await _dbContext.SaveChangesAsync();

            }
            catch(Exception ex){
                _logger.LogError(ex,"Failed to create challenge");
                return challenge1;
            }
            return challenge1;
        }

        public async Task<ChallengeEntity> GetChallengeByIdAsync(long ChallengeId){
            var challenge = _dbContext.Challenges
            .Include(groups=>groups.GroupEntity)
            .Include(exercise=>exercise.ExerciseEntity)
            .Include(exerciseT=>exerciseT.GroupEntity.ExerciseTypeEntity)
            .AsQueryable();

            ChallengeEntity challengeEntity = challenge.First(challenge=>challenge.ChallengeId == ChallengeId);
            return challengeEntity;
        }

        public List<ChallengeListModel> GetChallengesForGroupAsync(long groupId)
        {
            var userBridge = _dbContext.UserChallengeBridge
                .Include(group => group.ChallengeEntity.GroupEntity)
                .Include(challenge => challenge.ChallengeEntity)
                .Include(exercise => exercise.ChallengeEntity.ExerciseEntity)
                .Include(group=>group.ChallengeEntity.GroupEntity)
                .Include(exercise1=>exercise1.ChallengeEntity.GroupEntity.ExerciseTypeEntity)
                .AsQueryable();
            List<ChallengeListModel> ListOfChallenges = new List<ChallengeListModel>();
            var ListOfBridges = userBridge.Where(group=>group.ChallengeEntity.GroupEntity.GroupId == groupId).ToList();
            foreach(var Bridge in ListOfBridges){
                ChallengeListModel challengeListModel = new ChallengeListModel()
                {
                ChallengeId = Bridge.ChallengeId,
                ChallengeEndDate = Bridge.ChallengeEntity.ChallengeEndDate,
                ChallengeStartDate = Bridge.ChallengeEntity.ChallengeStartDate,
                AmountToComplete = Bridge.ChallengeEntity.AmountToComplete,
                ExerciseId = Bridge.ChallengeEntity.ExerciseId,
                ExerciseEntity = Bridge.ChallengeEntity.ExerciseEntity,
                GroupId = Bridge.ChallengeEntity.GroupId,
                GroupEntity = Bridge.ChallengeEntity.GroupEntity
                };
                ListOfChallenges.Add(challengeListModel);
            }
            return ListOfChallenges;
        }

        public List<ChallengeListModel> GetChallengesForUserAsync(long userId)
        {
            var userGroupBridge = _dbContext.UserChallengeBridge
                .Include(user => user.UserEntity)
                .Include(challenge => challenge.ChallengeEntity)
                .Include(exercise => exercise.ChallengeEntity.ExerciseEntity)
                .Include(group=>group.ChallengeEntity.GroupEntity)
                .Include(exercise1=>exercise1.ChallengeEntity.GroupEntity.ExerciseTypeEntity)
                .AsQueryable();
            List<ChallengeListModel> ListOfChallenges = new List<ChallengeListModel>();
            var ListOfBridges = userGroupBridge.Where(group=>group.UserEntity.UserId == userId).ToList();
            foreach(var Bridge in ListOfBridges){
                ChallengeListModel challengeListModel = new ChallengeListModel()
                {
                ChallengeId = Bridge.ChallengeId,
                ChallengeEndDate = Bridge.ChallengeEntity.ChallengeEndDate,
                ChallengeStartDate = Bridge.ChallengeEntity.ChallengeStartDate,
                AmountToComplete = Bridge.ChallengeEntity.AmountToComplete,
                ExerciseId = Bridge.ChallengeEntity.ExerciseId,
                ExerciseEntity = Bridge.ChallengeEntity.ExerciseEntity,
                GroupId = Bridge.ChallengeEntity.GroupId,
                GroupEntity = Bridge.ChallengeEntity.GroupEntity
                };
                ListOfChallenges.Add(challengeListModel);
            }
            return ListOfChallenges;
        }

        public List<UserChallengeBridgeEntity> GetUsersForChallengeAsync(long challengeId)
        {
            var challengeBridge = _dbContext.UserChallengeBridge
            .Include(user=>user.UserEntity)
            .AsQueryable();

            if(challengeId > 0){
                var ListofBridges = challengeBridge.Where(challenge => challenge.ChallengeId == challengeId).ToList();
                return ListofBridges;
            }
            return new List<UserChallengeBridgeEntity>();
        }

        public List<UserChallengeBridgeEntity> GetUsersToInviteChallengeAsync(long challengeId, long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChallengeEntity> RemoveUserFromChallengeAsync(long challengeId, long userId)
        {
            var challengeBridge = _dbContext.UserChallengeBridge
            .AsQueryable();
            UserChallengeBridgeEntity user = challengeBridge.First(userGroup => userGroup.UserId == userId && userGroup.ChallengeId == challengeId);
            _dbContext.UserChallengeBridge.Remove(user);
            await _dbContext.SaveChangesAsync();
            return await GetChallengeByIdAsync(challengeId);
        }

        public async Task<ChallengeEntity> UpdateChallengeAsync(ChallengeUpdateModel challenge)
        {
            var challenge1 = _dbContext.Challenges.Find(challenge.ChallengeId);

            if(challenge.ChallengeEndDate != null && challenge.ChallengeEndDate != challenge1.ChallengeEndDate)
            challenge1.ChallengeEndDate = challenge.ChallengeEndDate;

            if(challenge.ExerciseId != null && challenge.ExerciseId != challenge1.ExerciseId){
            challenge1.ExerciseId = challenge.ExerciseId;
            challenge1.ExerciseEntity = _dbContext.Exercises.Find(challenge.ExerciseId);
            }

            if(challenge.AmountToComplete != null && challenge.AmountToComplete != challenge1.AmountToComplete)
            challenge1.AmountToComplete = challenge.AmountToComplete;

            _dbContext.Update(challenge1);
            await _dbContext.SaveChangesAsync();
            return await GetChallengeByIdAsync(challenge1.ChallengeId);
        }
    }
}