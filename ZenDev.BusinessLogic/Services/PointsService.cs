using Microsoft.EntityFrameworkCore;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Enums;
using ZenDev.Common.Models;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;
using Microsoft.Extensions.Logging;

namespace ZenDev.BusinessLogic.Services
{
    public class PointsService: IPointsService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<PointsService> _logger;

        public PointsService(
            ZenDevDbContext dbContext,
            ILogger<PointsService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task UpdateTotalPoints(long userId, List<ActivityPointsApiModel> activities)
        {
            int totalPoints = 0;
            foreach (var activity in activities)
            {
                int movingTimeInMinutes = GetMinutes(activity.MovingTime);
                totalPoints += GetPointsForCategory(movingTimeInMinutes, activity.AverageHeartrate, activity.MaxHeartrate);
            }

            var user = _dbContext.Users.FirstOrDefault(user => user.UserId == userId);
            user.TotalPoints += totalPoints;

            UnlockPointsAchievement(user);

            await _dbContext.SaveChangesAsync();
        }

        public int CalculatePointsGroups(ActivityPointsApiModel activity)
        {
            int totalPoints = 0;
            int movingTimeInMinutes = GetMinutes(activity.MovingTime);
            totalPoints += GetPointsForCategory(movingTimeInMinutes, activity.AverageHeartrate, activity.MaxHeartrate);
            return totalPoints;
        }

        private static int GetMinutes(int movingTime)
        {
            return (movingTime / 60);
        }

        private static int GetPointsForCategory(int time, double? avgHeartRate, double? maxHeartRate)
        {
            double? percentage = avgHeartRate.HasValue && maxHeartRate.HasValue ? avgHeartRate.Value / maxHeartRate.Value : (double?)null;

            return (time, percentage) switch
            {
                ( >= 30, null) => (int)PointsCategory.Minutes30PlusWorkout,
                ( >= 90 and < 120, >= 0.70) => (int)PointsCategory.Minutes90To119HeartRate70PlusPercent,
                ( >= 30 and < 60, >= 0.60 and <= 0.69) => (int)PointsCategory.Minutes30To59HeartRate60To69Percent,
                ( >= 60 and < 90, >= 0.60 and <= 0.69) => (int)PointsCategory.Minutes60To89HeartRate60To69Percent,
                ( >= 90 and < 120, >= 0.60 and <= 0.69) => (int)PointsCategory.Minutes90To119HeartRate60To69Percent,
                ( >= 120 and < 180, >= 0.60 and <= 0.69) => (int)PointsCategory.Minutes120To179HeartRate60To69Percent,
                ( >= 180, >= 0.60) => (int)PointsCategory.Minutes180PlusHeartRate60PlusPercent,
                ( >= 15 and < 30, >= 0.70) => (int)PointsCategory.Minutes15To29HeartRate70PlusPercent,
                ( >= 30 and < 60, >= 0.70 and <= 0.79) => (int)PointsCategory.Minutes30To59HeartRate70To79Percent,
                ( >= 60 and < 90, >= 0.70) => (int)PointsCategory.Minutes60To89HeartRate70PlusPercent,
                ( >= 120, >= 0.70) => (int)PointsCategory.Minutes120PlusHeartRate70PlusPercent,
                ( >= 30, >= 0.80) => (int)PointsCategory.Minutes30PlusHeartRate80PlusPercent,
                _ => 0
            };
        }

        public async Task<DateTimeOffset?> SetLastSyncedDateAsync(long userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                user.LastSynced = DateTimeOffset.UtcNow;
                await _dbContext.SaveChangesAsync();
                return user.LastSynced;
            }
            return null;
        }

        public async Task<DateTimeOffset?> GetLastSyncedDateAsync(long userId)
        {
            return await _dbContext.Users
                .Where(user => user.UserId == userId)
                .Select(user => user.LastSynced)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAmountCompleteChallenges(long userId, List<ActivityPointsApiModel> activities)
        {
            var challenge = _dbContext.UserChallengeBridge
            .Include(challenge=>challenge.ChallengeEntity)
            .Include(exercise => exercise.ChallengeEntity.ExerciseEntity)
            .AsQueryable();
            foreach(var activity in activities){
                var bridges = challenge.Where(challenges => challenges.UserId == userId && 
                challenges.ChallengeEntity.ExerciseEntity.ExerciseName == activity.Exercise &&
                challenges.ChallengeEntity.ChallengeStartDate <= activity.StartDateLocal &&
                challenges.ChallengeEntity.ChallengeEndDate >= activity.StartDateLocal);
                foreach(var bridge in bridges){
                    if(bridge.ChallengeEntity.Measurement == Persistence.Constants.Measurement.Distance)
                        bridge.AmountCompleted += Convert.ToInt64(activity.Distance);
                    else
                        bridge.AmountCompleted += Convert.ToInt64(activity.Duration);
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePointsGroups(long userId, List<ActivityPointsApiModel> activities)
        {
            var group = _dbContext.UserGroupBridge
            .Include(challenge=>challenge.GroupEntity)
            .Include(exercise => exercise.GroupEntity.ExerciseTypeEntity)
            .AsQueryable();
            foreach(var activity in activities){
                var bridges = group.Where(groups => groups.UserId == userId && 
                groups.GroupEntity.ExerciseTypeEntity.ExerciseType == activity.Exercise);
                foreach(var bridge in bridges){
                    bridge.Points += CalculatePointsGroups(activity);
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public void UnlockPointsAchievement(UserEntity user){
            var pointAchievements = _dbContext.Achievements
                .Where(achievement => achievement.AchievementName.Contains("pts"))
                .ToArray();
            
            var myAchievements = _dbContext.UserAchievementBridge
                .Where(userAchievementBridge => userAchievementBridge.UserId == user.UserId)
                .Select(userAchievementBridge => userAchievementBridge.AchievementId)                               
                .ToArray();

            for (int i = 0; i < pointAchievements.Length; i++)
            {
                String[] achievementName = pointAchievements[i].AchievementName.Split(' ');
                long points = int.Parse(achievementName[0]);

                if (user.TotalPoints >= points)
                {
                    if (myAchievements.Contains(pointAchievements[i].AchievementId))
                    {
                        continue;
                    }
                    else{
                        UserAchievementBridgeEntity newPointsAchievement = new()
                        {
                            AchievementId = pointAchievements[i].AchievementId,
                            UserId = user.UserId
                        };
                        _dbContext.Add(newPointsAchievement);
                        _dbContext.SaveChanges();
                        _logger.LogInformation("New Achievement Unlocked with name " + pointAchievements[i].AchievementName);
                    }
                }
                else
                {
                    break;
                }
            }

        }

        public async Task UpdateTournamentPoints(long userId, List<ActivityPointsApiModel> activities)
        {
            var group = _dbContext.TournamentGroupBridge
            .Include(groups=>groups.TournamentGroupEntity)
            .Include(tournament=>tournament.TournamentEntity)
            .AsQueryable();
            var users = _dbContext.TournamentGroupUserBridge
            .Include(users=>users.UserEntity)
            .AsQueryable();
            var userGroups = users.Where(user=>user.UserId == userId).Select(groups=>groups.TournamentGroupEntity.TGroupId);
                foreach(var activity in activities){
                    var bridges = group.Where(e=>e.TournamentEntity.ExerciseEntity.ExerciseName == activity.Exercise &&
                    userGroups.Contains(e.TournamentGroupEntity.TGroupId));
                    foreach(var bridge in bridges){
                        bridge.Points += CalculatePointsGroups(activity);
                    }
                }
            await _dbContext.SaveChangesAsync();
        }
    }
}
