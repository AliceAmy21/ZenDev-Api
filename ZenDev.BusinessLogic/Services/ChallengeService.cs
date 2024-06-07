using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<GroupService> _logger;

        private readonly GroupService _groupService;
        
        public ChallengeService(
            ZenDevDbContext dbContext,
            ILogger<GroupService> logger,
            GroupService groupService) 
        {
            _dbContext = dbContext;
            _logger = logger;
            _groupService = groupService;
        }

        public async Task<ChallengeEntity> AddUserToChallenge(long challengeId, long userGroupId)
        {
            var challengeBridge = _dbContext.UserGroupChallengeBridge
                .Include(user=> user.UserGroupBridgeEntity.UserId)
                .AsQueryable();
            if(challengeId>0 && userGroupId>0){
                if(!challengeBridge.Any(user=>user.UserGroupBridgeEntity.UserGroupId == userGroupId) && !challengeBridge.Any(challenge => challenge.ChallengeId == challengeId)){
                    UserGroupChallengeBridgeEntity userGroupChallengeBridgeEntity = new UserGroupChallengeBridgeEntity()
                    {
                        UserGroupId = userGroupId,
                        UserGroupBridgeEntity = _groupService.getUserGroupBridgeById(userGroupId),
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

        public Task<ChallengeEntity> AddUserToChallengeAsync(long challengeId, long userGroupId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChallengeEntity> CreateChallengeAsync(ChallengeEntity challenge)
        {
            ChallengeEntity challenge1 = new()
            {
                ChallengeEndDate = challenge.ChallengeEndDate,
                ChallengeStartDate = challenge.ChallengeStartDate,
                UserGroupChallengeBridgeEntities = [],
                ExerciseId = challenge.ExerciseId,
                ExerciseEntity = challenge.ExerciseEntity,
                GroupId = challenge.GroupId,
                GroupEntity = challenge.GroupEntity
            };

            try{
                await _dbContext.AddAsync(challenge1);
                await _dbContext.SaveChangesAsync();

            }
            catch(Exception ex){
                _logger.LogError(ex,"Failed to create challenge");
                return challenge1;
            }
            return challenge1;
        }

        public async Task<ChallengeEntity> GetChallengeByIdAsync(long ChallengeId){
            return await _dbContext.Challenges.FindAsync(ChallengeId);
        }

        public List<ChallengeEntity> GetChallengesForGroupAsync(long groupId)
        {
            var userGroupBridge = _dbContext.UserGroupChallengeBridge
                .Include(group => group.UserGroupBridgeEntity)
                .Include(challenges => challenges.ChallengeEntity)
                .AsQueryable();
            List<ChallengeEntity> ListOfChallenges = new List<ChallengeEntity>();
            var ListOfBridges = userGroupBridge.Where(group=>group.UserGroupBridgeEntity.GroupEntity.GroupId == groupId).ToList();
            foreach(var Bridge in ListOfBridges){
                ListOfChallenges.Add(Bridge.ChallengeEntity);
            }
            return ListOfChallenges;
        }

        public List<ChallengeEntity> GetChallengesForUserAsync(long userId)
        {
            var userGroupBridge = _dbContext.UserGroupChallengeBridge
                .Include(group => group.UserGroupBridgeEntity)
                .Include(challenges => challenges.ChallengeEntity)
                .AsQueryable();
            List<ChallengeEntity> ListOfChallenges = new List<ChallengeEntity>();
            var ListOfBridges = userGroupBridge.Where(group=>group.UserGroupBridgeEntity.UserEntity.UserId == userId).ToList();
            foreach(var Bridge in ListOfBridges){
                ListOfChallenges.Add(Bridge.ChallengeEntity);
            }
            return ListOfChallenges;
        }

        public List<UserEntity> GetUsersForChallengeAsync(long challengeId)
        {
            throw new NotImplementedException();
        }

        public List<UserEntity> GetUsersToInviteChallengeAsync(long challengeId)
        {
            throw new NotImplementedException();
        }

        public List<UserEntity> GetUsersToInviteChallengeAsync(long challengeId, long userGroupBridgeId)
        {
            throw new NotImplementedException();
        }

        public Task<ChallengeEntity> RemoveUserFromChallengeAsync(long challengeId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<ChallengeEntity> UpdateChallengeAsync(ChallengeEntity challenge)
        {
            throw new NotImplementedException();
        }
    }
}