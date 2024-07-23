﻿using Microsoft.EntityFrameworkCore;
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
            var user = _dbContext.Users.FirstOrDefault(user => user.UserId == userId);
            foreach (var activity in activities)
            {
                var startOfWeek = GetStartOfWeek(activity.StartDateLocal.DayOfWeek);
                int movingTimeInMinutes = GetMinutes(activity.MovingTime);
                int points = GetPointsForCategory(movingTimeInMinutes, activity.AverageHeartrate, activity.MaxHeartrate);

                if (startOfWeek == user.ActiveWeek)
                {
                    user.WeekPoints += points;
                }
                else
                {
                    UpdateStreak(user, startOfWeek);
                }

                user.TotalPoints += points;
            }

            UnlockStreakAchievement(user);
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

        public DateTime GetStartOfWeek(DayOfWeek startOfWeek)
        {
            var currentDate = DateTime.Now;
            int diff = (7 + (currentDate.DayOfWeek - startOfWeek)) % 7; //Gets the number of days to subtract to get to the start of the week
            return currentDate.AddDays(-diff).Date; // .Date is used to set the time to midnight
        }

        public long UpdateStreak(UserEntity user, DateTimeOffset activeWeek){
           if (user.WeekPoints >= 500)
           {
                user.Streak += 1;
           }
           else
           {
                user.Streak = 0;
           }
           user.WeekPoints = 0;
           user.ActiveWeek = activeWeek;

            _dbContext.Update(user);
            _dbContext.SaveChanges();

            _logger.LogInformation(user.Streak.ToString() + " week streak");
            return user.Streak;
        }

        public void UnlockStreakAchievement(UserEntity user){
            var streakAchievements = _dbContext.Achievements
                .Where(achievement => achievement.AchievementName.Contains("Streak"))
                .ToArray();
            
            var myAchievements = _dbContext.UserAchievementBridge
                .Where(userAchievementBridge => userAchievementBridge.UserId == user.UserId)
                .Select(userAchievementBridge => userAchievementBridge.AchievementId)                               
                .ToArray();

            for (int i = 0; i < streakAchievements.Length; i++)
            {
                String[] achievementName = streakAchievements[i].AchievementName.Split(' ');
                long streak = int.Parse(achievementName[0]);

                if (user.Streak >= streak)
                {
                    if (myAchievements.Contains(streakAchievements[i].AchievementId))
                    {
                        continue;
                    }
                    else
                    {
                        UserAchievementBridgeEntity newStreakAchievement = new()
                        {
                            AchievementId = streakAchievements[i].AchievementId,
                            UserId = user.UserId
                        };
                        _dbContext.Add(newStreakAchievement);
                        _dbContext.SaveChanges();
                         _logger.LogInformation("New Achievement Unlocked with name " + streakAchievements[i].AchievementName);
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
