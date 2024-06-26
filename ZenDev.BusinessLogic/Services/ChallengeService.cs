using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;
using ZenDev.BusinessLogic.Models;

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

        public async Task AddUserToChallengeAsync(long challengeId, long userId)
        {
            var challengeBridge = _dbContext.UserChallengeBridge
                .AsQueryable();
            if(challengeId>0 && userId>0){
                try{
                    if(!challengeBridge.Any(user=>user.UserId == userId && user.ChallengeId == challengeId)){
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
                    }
                }
                catch(Exception ex){
                _logger.LogError(ex,"Failed to add user to challenge");
                }
            }
        }


        public async Task<ChallengeEntity> CreateChallengeAsync(ChallengeCreationModel challenge, long UserId)
        {
            var groups = _dbContext.Groups.Include(exercise=>exercise.ExerciseTypeEntity).AsQueryable();

            ChallengeEntity challenge1 = new()
            {
                ChallengeName = challenge.ChallengeName,
                ChallengeDescription = challenge.ChallengeDescription,
                ChallengeEndDate = challenge.ChallengeEndDate,
                ChallengeStartDate = challenge.ChallengeStartDate,
                AmountCompleted = 0,
                AmountToComplete = challenge.AmountToComplete,
                Measurement = challenge.Measurement,
                ExerciseId = challenge.ExerciseId,
                ExerciseEntity = _dbContext.Exercises.Find(challenge.ExerciseId),
                GroupId = challenge.GroupId,
                GroupEntity = groups.First(group=> group.GroupId == challenge.GroupId),
                Admin = challenge.UserId
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

            ChallengeEntity challengeEntity = await challenge.FirstAsync(challenge=>challenge.ChallengeId == ChallengeId);
            return challengeEntity;
        }

        public List<List<ChallengeListModel>> GetChallengesForGroupAsync(long groupId, long userId)
        {
            var userBridge = _dbContext.UserChallengeBridge
                .Include(group => group.ChallengeEntity.GroupEntity)
                .Include(challenge => challenge.ChallengeEntity)
                .Include(exercise => exercise.ChallengeEntity.ExerciseEntity)
                .Include(group=>group.ChallengeEntity.GroupEntity)
                .Include(exercise1=>exercise1.ChallengeEntity.GroupEntity.ExerciseTypeEntity)
                .AsQueryable();
            List<List<ChallengeListModel>> ListOfChallenges = new List<List<ChallengeListModel>>();
            List<ChallengeListModel> ListOfChallengesGroup = new List<ChallengeListModel>();
            List<ChallengeListModel> ListOfChallengesUser = new List<ChallengeListModel>();
            var ListOfBridges = userBridge.Where(group=>group.ChallengeEntity.GroupEntity.GroupId == groupId && group.UserId != userId).ToList();
            foreach(var Bridge in ListOfBridges){
                ChallengeListModel challengeListModel = new ChallengeListModel()
                {
                ChallengeName = Bridge.ChallengeEntity.ChallengeName,
                AmountCompleted = Bridge.ChallengeEntity.AmountCompleted,
                Measurement = Bridge.ChallengeEntity.Measurement,
                Admin = Bridge.ChallengeEntity.Admin,
                ChallengeId = Bridge.ChallengeId,
                ChallengeEndDate = Bridge.ChallengeEntity.ChallengeEndDate,
                ChallengeStartDate = Bridge.ChallengeEntity.ChallengeStartDate,
                AmountToComplete = Bridge.ChallengeEntity.AmountToComplete,
                ExerciseId = Bridge.ChallengeEntity.ExerciseId,
                ExerciseEntity = Bridge.ChallengeEntity.ExerciseEntity,
                GroupId = Bridge.ChallengeEntity.GroupId,
                GroupEntity = Bridge.ChallengeEntity.GroupEntity
                };
                ListOfChallengesGroup.Add(challengeListModel);
            }
            var ListOfBridges1 = userBridge.Where(group=>group.ChallengeEntity.GroupEntity.GroupId == groupId && group.UserId == userId).ToList();
            foreach(var Bridge in ListOfBridges1){
                ChallengeListModel challengeListModel = new ChallengeListModel()
                {
                ChallengeId = Bridge.ChallengeId,
                ChallengeName = Bridge.ChallengeEntity.ChallengeName,
                AmountCompleted = Bridge.ChallengeEntity.AmountCompleted,
                Measurement = Bridge.ChallengeEntity.Measurement,
                Admin = Bridge.ChallengeEntity.Admin,
                ChallengeEndDate = Bridge.ChallengeEntity.ChallengeEndDate,
                ChallengeStartDate = Bridge.ChallengeEntity.ChallengeStartDate,
                AmountToComplete = Bridge.ChallengeEntity.AmountToComplete,
                ExerciseId = Bridge.ChallengeEntity.ExerciseId,
                ExerciseEntity = Bridge.ChallengeEntity.ExerciseEntity,
                GroupId = Bridge.ChallengeEntity.GroupId,
                GroupEntity = Bridge.ChallengeEntity.GroupEntity
                };
                ListOfChallengesUser.Add(challengeListModel);
            }
            ListOfChallenges.Add(ListOfChallengesUser);
            ListOfChallenges.Add(ListOfChallengesGroup);
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
                ChallengeName = Bridge.ChallengeEntity.ChallengeName,
                AmountCompleted = Bridge.ChallengeEntity.AmountCompleted,
                Measurement = Bridge.ChallengeEntity.Measurement,
                Admin = Bridge.ChallengeEntity.Admin,
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

        public List<UserInviteModel> GetUsersForChallengeAsync(long challengeId)
        {
            var challengeBridge = _dbContext.UserChallengeBridge
            .Include(user=>user.UserEntity)
            .AsQueryable();

            if(challengeId > 0){
                var ListofBridges = challengeBridge.Where(challenge => challenge.ChallengeId == challengeId).ToList();
                List<UserInviteModel> ListofUsers = new List<UserInviteModel>();
                foreach(var Bridge in ListofBridges){
                    var user = new UserInviteModel{
                        UserId = Bridge.UserId,
                        UserName = Bridge.UserEntity.UserName,
                        AvatarIconUrl =Bridge.UserEntity.AvatarIconUrl
                    };
                    ListofUsers.Add(user);
                }
                return ListofUsers;
            }
            return new List<UserInviteModel>();
        }

        public List<UserInviteModel> GetUsersToInviteChallengeAsync(long challengeId, long groupId)
        {
            var userGroupBridge = _dbContext.UserGroupBridge
            .Include(user=>user.UserEntity)
            .AsQueryable();

            var challengeBridge = _dbContext.UserChallengeBridge
            .Include(user=>user.UserEntity)
            .AsQueryable();

            if(challengeId > 0){ 
                var ListofBridges = userGroupBridge.Where(group => group.GroupId == groupId).ToList();
                List<UserInviteModel> ListofUsers = new List<UserInviteModel>();
                foreach(var Bridge in ListofBridges){
                    if(!challengeBridge.Any(challenge => challenge.UserId == Bridge.UserId && challenge.ChallengeId == challengeId)){
                        var user = new UserInviteModel{
                            UserId = Bridge.UserId,
                            UserName = Bridge.UserEntity.UserName,
                            AvatarIconUrl =Bridge.UserEntity.AvatarIconUrl
                        };
                        ListofUsers.Add(user);
                    }
                }
                return ListofUsers;
            }
            return new List<UserInviteModel>();
        }

        public async Task RemoveUserFromChallengeAsync(long challengeId, long userId)
        {
            try{
            var challengeBridge = _dbContext.UserChallengeBridge
            .AsQueryable();
            UserChallengeBridgeEntity user = await challengeBridge.FirstAsync(userGroup => userGroup.UserId == userId && userGroup.ChallengeId == challengeId);
            _dbContext.UserChallengeBridge.Remove(user);
            await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex){
                _logger.LogError(ex,"Failed to remove user from challenge");
            }
        }

        public async Task<ChallengeEntity> UpdateChallengeAsync(ChallengeUpdateModel challenge)
        {
            var challenge1 = await _dbContext.Challenges.FindAsync(challenge.ChallengeId);

            if(challenge.ChallengeName != null && challenge.ChallengeName != challenge1.ChallengeName)
            challenge1.ChallengeName = challenge.ChallengeName;

            if(challenge.ChallengeEndDate != null && challenge.ChallengeEndDate != challenge1.ChallengeEndDate)
            challenge1.ChallengeEndDate = challenge.ChallengeEndDate;

            if(challenge.ExerciseId != null && challenge.ExerciseId != challenge1.ExerciseId){
            challenge1.ExerciseId = challenge.ExerciseId;
            challenge1.ExerciseEntity = _dbContext.Exercises.Find(challenge.ExerciseId);
            }

            if(challenge.AmountToComplete != null && challenge.AmountToComplete != challenge1.AmountToComplete)
            challenge1.AmountToComplete = challenge.AmountToComplete;

            if(challenge.Admin != null && challenge.Admin != challenge1.Admin)
            challenge1.Admin = challenge.Admin;

            if(challenge.ChallengeDescription  != null && challenge.ChallengeDescription != challenge1.ChallengeDescription)
            challenge1.ChallengeDescription = challenge.ChallengeDescription;

            if(challenge.Measurement  != null && challenge.Measurement.Equals(challenge1.Measurement))
            challenge1.Measurement = challenge.Measurement;

            _dbContext.Update(challenge1);
            await _dbContext.SaveChangesAsync();
            return await GetChallengeByIdAsync(challenge1.ChallengeId);
        }
        public async Task<long> DeleteChallengeAsync(long challengeId)
        {
            var challenge = _dbContext.Challenges.FirstOrDefault(challenge => challenge.ChallengeId == challengeId);
            if(challenge != null)
            _dbContext.Remove(challenge);
            await _dbContext.SaveChangesAsync();
            return challengeId;
        }
        public async Task<List<ExerciseEntity>> GetAllExercisesAsync()
        {
            return await _dbContext.Exercises.ToListAsync();
        }
    }
}