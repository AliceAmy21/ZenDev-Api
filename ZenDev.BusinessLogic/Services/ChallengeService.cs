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
                    if(!await challengeBridge.AnyAsync(user=>user.UserId == userId && user.ChallengeId == challengeId)){
                        UserChallengeBridgeEntity userChallengeBridgeEntity = new UserChallengeBridgeEntity()
                        {
                            UserId = userId,
                            ChallengeId = challengeId,
                            AmountCompleted = 0
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
                AmountToComplete = challenge.AmountToComplete,
                Measurement = challenge.Measurement,
                ExerciseId = challenge.ExerciseId,
                ExerciseEntity = await _dbContext.Exercises.FindAsync(challenge.ExerciseId),
                GroupId = challenge.GroupId,
                GroupEntity = await groups.FirstAsync(group=> group.GroupId == challenge.GroupId),
                Admin = challenge.UserId
            };

            try{
                await _dbContext.AddAsync(challenge1);
                await _dbContext.SaveChangesAsync();
                UserChallengeBridgeEntity userChallengeBridgeEntity = new(){
                UserId = UserId,
                ChallengeId = challenge1.ChallengeId,
                AmountCompleted = 0
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

        public async Task<ChallengeViewModel> GetChallengeByIdAsync(long challengeId, long userId){
            var challenge = _dbContext.Challenges
            .Include(groups=>groups.GroupEntity)
            .Include(exercise=>exercise.ExerciseEntity)
            .Include(exerciseT=>exerciseT.GroupEntity.ExerciseTypeEntity)
            .AsQueryable();

            var amountCompleted = _dbContext.UserChallengeBridge.FirstOrDefaultAsync(u => u.UserId == userId && u.ChallengeId == challengeId).Result.AmountCompleted;
            ChallengeEntity challengeEntity = await challenge.FirstAsync(challenge=>challenge.ChallengeId == challengeId);
            ChallengeViewModel challengeView = new ChallengeViewModel{
                ChallengeId = challengeEntity.ChallengeId,
                ChallengeName = challengeEntity.ChallengeName,
                ChallengeDescription = challengeEntity.ChallengeDescription,
                ChallengeStartDate = challengeEntity.ChallengeStartDate, 
                ChallengeEndDate = challengeEntity.ChallengeEndDate,
                AmountCompleted = amountCompleted,
                Measurement = challengeEntity.Measurement,
                AmountToComplete = challengeEntity.AmountToComplete,
                ExerciseId = challengeEntity.ExerciseId,
                ExerciseEntity = challengeEntity.ExerciseEntity,
                GroupId = challengeEntity.GroupId,
                GroupEntity = challengeEntity.GroupEntity,
                UserId = userId,
                Admin = challengeEntity.Admin
            };

            return challengeView;
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
            var challenges = _dbContext.Challenges
                .Include(group => group.GroupEntity)
                .Include(exercise => exercise.ExerciseEntity)
                .Include(group=>group.GroupEntity)
                .Include(exercise1=>exercise1.GroupEntity.ExerciseTypeEntity)
                .AsQueryable();
            List<List<ChallengeListModel>> ListOfChallenges = new List<List<ChallengeListModel>>();
            List<ChallengeListModel> ListOfChallengesGroup = new List<ChallengeListModel>();
            List<ChallengeListModel> ListOfChallengesUser = new List<ChallengeListModel>();
            var ListOfBridges = challenges.Where(group=>group.GroupEntity.GroupId == groupId).ToList();
            foreach(var Bridge in ListOfBridges){
                if(userBridge.Any(user=>user.UserId == userId && user.ChallengeId == Bridge.ChallengeId)){
                    var challenge = userBridge.First(user=> user.UserId == userId && user.ChallengeId == Bridge.ChallengeId);
                    ChallengeListModel challengeListModel = new ChallengeListModel()
                    {
                    ChallengeName = Bridge.ChallengeName,
                    AmountCompleted = challenge.AmountCompleted,
                    Measurement = Bridge.Measurement,
                    Admin = Bridge.Admin,
                    ChallengeId = Bridge.ChallengeId,
                    ChallengeEndDate = Bridge.ChallengeEndDate,
                    ChallengeStartDate = Bridge.ChallengeStartDate,
                    AmountToComplete = Bridge.AmountToComplete,
                    ExerciseId = Bridge.ExerciseId,
                    ExerciseEntity = Bridge.ExerciseEntity,
                    GroupId = Bridge.GroupId,
                    GroupEntity = Bridge.GroupEntity
                    };
                ListOfChallengesUser.Add(challengeListModel);
            }   
            else{
                ChallengeListModel challengeListModel = new ChallengeListModel()
                {
                ChallengeId = Bridge.ChallengeId,
                ChallengeName = Bridge.ChallengeName,
                AmountCompleted = 0,
                Measurement = Bridge.Measurement,
                Admin = Bridge.Admin,
                ChallengeEndDate = Bridge.ChallengeEndDate,
                ChallengeStartDate = Bridge.ChallengeStartDate,
                AmountToComplete = Bridge.AmountToComplete,
                ExerciseId = Bridge.ExerciseId,
                ExerciseEntity = Bridge.ExerciseEntity,
                GroupId = Bridge.GroupId,
                GroupEntity = Bridge.GroupEntity
                };
            ListOfChallengesGroup.Add(challengeListModel);
            }
            }
            ListOfChallenges.Add(ListOfChallengesUser);
            ListOfChallenges.Add(ListOfChallengesGroup);
            return ListOfChallenges;
        }

        public List<List<ChallengeListModel>> GetChallengesForUserAsync(long userId)
        {
            var userChallengeBridge = _dbContext.UserChallengeBridge
                .Include(user => user.UserEntity)
                .Include(challenge => challenge.ChallengeEntity)
                .Include(exercise => exercise.ChallengeEntity.ExerciseEntity)
                .Include(group=>group.ChallengeEntity.GroupEntity)
                .Include(exercise1=>exercise1.ChallengeEntity.GroupEntity.ExerciseTypeEntity)
                .AsQueryable();
            var userGroupBridge = _dbContext.UserGroupBridge.AsQueryable();
            List<ChallengeListModel> ListOfChallengesMine = new List<ChallengeListModel>();
            List<ChallengeListModel> ListOfChallengesAvailable = new List<ChallengeListModel>();
            List<List<ChallengeListModel>> ListOfChallenges = new List<List<ChallengeListModel>>();
            var ListOfBridges = userGroupBridge.Where(group=>group.UserEntity.UserId == userId).ToList();
            var ListOfBridges2 = userChallengeBridge.Where(user => user.UserId == userId).ToList();
            foreach(var Bridge in ListOfBridges){
                var ListofBridges3 = userChallengeBridge.Where(user=> user.ChallengeEntity.GroupId == Bridge.GroupId && user.UserId != userId).ToList();
                    foreach(var Bridge3 in ListofBridges3){
                        ChallengeListModel challengeListModel = new ChallengeListModel()
                        {
                            ChallengeId = Bridge3.ChallengeId,
                            ChallengeName = Bridge3.ChallengeEntity.ChallengeName,
                            AmountCompleted = Bridge3.AmountCompleted,
                            Measurement = Bridge3.ChallengeEntity.Measurement,
                            Admin = Bridge3.ChallengeEntity.Admin,
                            ChallengeEndDate = Bridge3.ChallengeEntity.ChallengeEndDate,
                            ChallengeStartDate = Bridge3.ChallengeEntity.ChallengeStartDate,
                            AmountToComplete = Bridge3.ChallengeEntity.AmountToComplete,
                            ExerciseId = Bridge3.ChallengeEntity.ExerciseId,
                            ExerciseEntity = Bridge3.ChallengeEntity.ExerciseEntity,
                            GroupId = Bridge3.ChallengeEntity.GroupId,
                            GroupEntity = Bridge3.ChallengeEntity.GroupEntity
                        };
                        ListOfChallengesAvailable.Add(challengeListModel);
                    }
            }
            foreach(var Bridge in ListOfBridges2){
                    ChallengeListModel challengeListModel = new ChallengeListModel()
                    {
                        ChallengeId = Bridge.ChallengeId,
                        ChallengeName = Bridge.ChallengeEntity.ChallengeName,
                        AmountCompleted = Bridge.AmountCompleted,
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
                    ListOfChallengesMine.Add(challengeListModel);
            }
            ListOfChallenges.Add(ListOfChallengesMine);
            ListOfChallenges.Add(ListOfChallengesAvailable);
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

        public List<UserInviteModel> GetUsersToInviteChallengeAsync(long challengeId)
        {
            var userGroupBridge = _dbContext.UserGroupBridge
            .Include(user=>user.UserEntity)
            .AsQueryable();

            var challengeBridge = _dbContext.UserChallengeBridge
            .Include(user=>user.UserEntity)
            .AsQueryable();

            long groupId = _dbContext.Challenges.Find(challengeId).GroupId;

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
            if(challenge1!=null){
            challenge1.ChallengeName = challenge.ChallengeName;
            challenge1.ChallengeEndDate = challenge.ChallengeEndDate;
            challenge1.ExerciseId = challenge.ExerciseId;
            var ex = await _dbContext.Exercises.FindAsync(challenge.ExerciseId);
            if(ex !=null){
            challenge1.ExerciseEntity = ex;
            }
            challenge1.AmountToComplete = challenge.AmountToComplete;
            challenge1.Admin = challenge.Admin;
            challenge1.ChallengeDescription = challenge.ChallengeDescription;
            challenge1.Measurement = challenge.Measurement;

            _dbContext.Update(challenge1);
            await _dbContext.SaveChangesAsync();
            return challenge1;
            }
            return null;
        }
        public async Task<long> DeleteChallengeAsync(long challengeId)
        {
            var challenge = await _dbContext.Challenges.FirstOrDefaultAsync(challenge => challenge.ChallengeId == challengeId);
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