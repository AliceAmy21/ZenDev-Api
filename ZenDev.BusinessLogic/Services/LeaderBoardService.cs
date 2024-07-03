using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class LeaderBoardService : ILeaderBoardService
    {

        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<LeaderBoardService> _logger;
        
        public LeaderBoardService(
            ZenDevDbContext dbContext,
            ILogger<LeaderBoardService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<LeaderBoardListModel> GetAllLeaderBoardDataForChallenge(long challengeId)
        {
            var activityRecords = _dbContext.UserChallengeBridge
            .Include(users => users.UserEntity)
            .Include(challenges => challenges.ChallengeEntity)
            .AsQueryable();

            var bridges = activityRecords.Where(user=>user.ChallengeId == challengeId);
            List<LeaderBoardListModel> leaderBoardListModels = new List<LeaderBoardListModel>(); 

            foreach(var activity in bridges){
                var leaderBoardModel = new LeaderBoardListModel{
                    UserId = activity.UserId,
                    UserInviteModel = new UserInviteModel{
                        UserId = activity.UserEntity.UserId,
                        UserName = activity.UserEntity.UserName,
                        AvatarIconUrl = activity.UserEntity.AvatarIconUrl
                    },
                    Points = Math.Round(Convert.ToDouble(activity.AmountCompleted)/Convert.ToDouble(activity.ChallengeEntity.AmountToComplete)*100,2)
                };
                leaderBoardListModels.Add(leaderBoardModel);
            }
            return leaderBoardListModels.OrderByDescending(challenge => challenge.Points).ToList();           
        }

        public List<LeaderBoardListModel> GetAllLeaderBoardDataForGroups(long groupId)
        {
            var activityRecords = _dbContext.UserGroupBridge
            .Include(users => users.UserEntity)
            .Include(group => group.GroupEntity)
            .AsQueryable();

            var bridges = activityRecords.Where(user=>user.GroupId == groupId);
            List<LeaderBoardListModel> leaderBoardListModels = new List<LeaderBoardListModel>(); 

            foreach(var activity in bridges){
                var leaderBoardModel = new LeaderBoardListModel{
                    UserId = activity.UserId,
                    UserInviteModel = new UserInviteModel{
                        UserId = activity.UserEntity.UserId,
                        UserName = activity.UserEntity.UserName,
                        AvatarIconUrl = activity.UserEntity.AvatarIconUrl
                    },
                    Points = activity.Points 
                };
                leaderBoardListModels.Add(leaderBoardModel);
            }
            return leaderBoardListModels.OrderByDescending(challenge => challenge.Points).ToList();
        }
    }
}