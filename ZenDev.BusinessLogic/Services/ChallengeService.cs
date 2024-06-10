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

        public async Task<UserGroupBridgeEntity> FindUserGroupBridge(long userId, long groupId){
            var userGroupBridge = _dbContext.UserGroupBridge
            .AsQueryable();
            var userGroupBridgeEntity = userGroupBridge.First(user=> user.UserId == userId && user.GroupId == groupId);
            return userGroupBridgeEntity;
        }

        public async Task<ChallengeEntity> AddUserToChallengeAsync(long challengeId, long userGroupId)
        {
            var challengeBridge = _dbContext.UserGroupChallengeBridge
                .Include(userBridge =>userBridge.UserGroupBridgeEntity)
                .AsQueryable();
            if(challengeId>0 && userGroupId>0){
                if(!challengeBridge.Any(user=>user.UserGroupBridgeEntity.UserGroupId == userGroupId) && !challengeBridge.Any(challenge => challenge.ChallengeId == challengeId)){
                    UserGroupChallengeBridgeEntity userGroupChallengeBridgeEntity = new UserGroupChallengeBridgeEntity()
                    {
                        UserGroupId = userGroupId,
                        UserGroupBridgeEntity = _dbContext.UserGroupBridge.Find(userGroupId),
                        ChallengeId = challengeId,
                        ChallengeEntity = await GetChallengeByIdAsync(challengeId)
                    };
                    try{
                        await _dbContext.AddAsync(userGroupChallengeBridgeEntity);
                        await _dbContext.SaveChangesAsync();

                    }
                    catch(Exception ex){
                        _logger.LogError(ex,"Failed to add user");
                    }
                    return await GetChallengeByIdAsync(challengeId);
                }
            else{
                return await GetChallengeByIdAsync(challengeId);
            }
            }
            return null;
            }

        public async Task<ChallengeEntity> CreateChallengeAsync(ChallengeEntity challenge, long UserId)
        {
            ChallengeEntity challenge1 = new()
            {
                ChallengeEndDate = challenge.ChallengeEndDate,
                ChallengeStartDate = challenge.ChallengeStartDate,
                AmountToComplete = challenge.AmountToComplete,
                UserEntities = challenge.UserEntities,
                ExerciseId = challenge.ExerciseId,
                ExerciseEntity = _dbContext.Exercises.Find(challenge.ExerciseId),
                GroupId = challenge.GroupId,
                GroupEntity = _dbContext.Groups.Find(challenge.GroupId)
            };

            UserGroupChallengeBridgeEntity userGroupChallengeBridgeEntity = new(){
                UserGroupBridgeEntity = await FindUserGroupBridge(UserId,challenge1.GroupId),
                ChallengeEntity = challenge1
            };


            try{
                await _dbContext.AddAsync(challenge1);
                await _dbContext.AddAsync(userGroupChallengeBridgeEntity);
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
            .AsQueryable();

            ChallengeEntity challengeEntity = challenge.First(challenge=>challenge.ChallengeId == ChallengeId);
            List<UserEntity> userEntities = GetUsersForChallengeAsync(ChallengeId);
            challengeEntity.UserEntities = userEntities;
            return challengeEntity;
        }

        public List<ChallengeListModel> GetChallengesForGroupAsync(long groupId)
        {
            var userGroupBridge = _dbContext.UserGroupChallengeBridge
                .Include(group => group.UserGroupBridgeEntity.GroupEntity)
                .Include(challenge => challenge.ChallengeEntity)
                .Include(exercise => exercise.ChallengeEntity.ExerciseEntity)
                .Include(group=>group.ChallengeEntity.GroupEntity)
                .Include(exercise1=>exercise1.ChallengeEntity.GroupEntity.ExerciseTypeEntity)
                .AsQueryable();
            List<ChallengeListModel> ListOfChallenges = new List<ChallengeListModel>();
            var ListOfBridges = userGroupBridge.Where(group=>group.UserGroupBridgeEntity.GroupEntity.GroupId == groupId).ToList();
            Console.WriteLine(ListOfBridges.Count);
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
            var userGroupBridge = _dbContext.UserGroupChallengeBridge
                .Include(user => user.UserGroupBridgeEntity.UserEntity)
                .Include(challenge => challenge.ChallengeEntity)
                .Include(exercise => exercise.ChallengeEntity.ExerciseEntity)
                .Include(group=>group.ChallengeEntity.GroupEntity)
                .Include(exercise1=>exercise1.ChallengeEntity.GroupEntity.ExerciseTypeEntity)
                .AsQueryable();
            List<ChallengeListModel> ListOfChallenges = new List<ChallengeListModel>();
            var ListOfBridges = userGroupBridge.Where(group=>group.UserGroupBridgeEntity.UserEntity.UserId == userId).ToList();
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

        public List<UserEntity> GetUsersForChallengeAsync(long challengeId)
        {
            var challengeBridge = _dbContext.UserGroupChallengeBridge
            .Include(user=>user.UserGroupBridgeEntity)
            .Include(users=>users.UserGroupBridgeEntity.UserEntity)
            .AsQueryable();

            if(challengeId > 0){
                var ListofBridges = challengeBridge.Where(challenge => challenge.ChallengeId == challengeId).ToList();
                List<UserEntity> userEntities = new List<UserEntity>();
                foreach(var Bridge in ListofBridges){
                    userEntities.Add(Bridge.UserGroupBridgeEntity.UserEntity);
                }
                return userEntities;
            }

            else{
                return new List<UserEntity>();
            }
        }

        public List<UserEntity> GetUsersToInviteChallengeAsync(long challengeId, long userGroupId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChallengeEntity> RemoveUserFromChallengeAsync(long challengeId, long userGroupId)
        {
            var challengeBridge = _dbContext.UserGroupChallengeBridge
            .AsQueryable();
            UserGroupChallengeBridgeEntity user = challengeBridge.First(userGroup => userGroup.UserGroupBridgeEntity.UserGroupId == userGroupId && userGroup.ChallengeId == challengeId);
            _dbContext.UserGroupChallengeBridge.Remove(user);
            await _dbContext.SaveChangesAsync();
            return await GetChallengeByIdAsync(challengeId);
        }

        public async Task<ChallengeEntity> UpdateChallengeAsync(ChallengeEntity challenge)
        {
            var ChallengeOld = _dbContext.Challenges.Find(challenge.ChallengeId);
            ChallengeOld.ChallengeStartDate = challenge.ChallengeStartDate;
            ChallengeOld.ChallengeEndDate= challenge.ChallengeEndDate;
            ChallengeOld.AmountToComplete = challenge.AmountToComplete;
            ChallengeOld.ExerciseId = challenge.ExerciseId;
            ChallengeOld.ExerciseEntity = _dbContext.Exercises.Find(challenge.ExerciseId);
            await _dbContext.SaveChangesAsync();
            return await GetChallengeByIdAsync(challenge.ChallengeId);
        }
    }
}