using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class MindfulnessService : IMindfulnessService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<MindfulnessService> _logger;

        public MindfulnessService(
            ZenDevDbContext dbContext,
            ILogger<MindfulnessService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<ResultModel> AddMindfulnessPoints(MindfulnessEntity mindfulnessEntity)
        {
            var result = new ResultModel
            {
                Success = false
            };

            var record = _dbContext.Mindfulness.FirstOrDefault(rec => rec.UserId == mindfulnessEntity.UserId);
            var userUpdate = _dbContext.Users.FirstOrDefault(user => user.UserId == mindfulnessEntity.UserId);
            var startOfWeek = GetStartOfWeek(DateTime.Now.DayOfWeek);

            if (record != null)
            {
                try
                {
                    record.TotalPoints += mindfulnessEntity.TodaysPoints;
                    userUpdate.TotalPoints += mindfulnessEntity.TodaysPoints;
                    record.TotalMinutes += mindfulnessEntity.TodaysMinutes;
                    record.TodaysMinutes += mindfulnessEntity.TodaysMinutes;
                    record.TodaysPoints += mindfulnessEntity.TodaysPoints;
                    record.LastUpdate = DateTime.Now;

                    if (startOfWeek == userUpdate.ActiveWeek)
                    {
                        userUpdate.WeekPoints += mindfulnessEntity.TodaysPoints;
                    }
                    else
                    {
                        UpdateStreak(userUpdate, startOfWeek);
                    }
                    
                    UnlockStreakAchievement(userUpdate);
                    UnlockPointsAchievement(userUpdate);

                    _dbContext.Update(record);
                    await _dbContext.SaveChangesAsync();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.ErrorMessages = new List<string> { "Failed to update mindfulness table." };
                    _logger.LogError(ex, "Failed to update mindfulness table.");
                    return result;
                }
            }
            else
            {
                try
                {
                    mindfulnessEntity.LastUpdate = DateTime.Now;
                    userUpdate.TotalPoints += mindfulnessEntity.TodaysPoints;

                    if (startOfWeek == userUpdate.ActiveWeek)
                    {
                        userUpdate.WeekPoints += mindfulnessEntity.TodaysPoints;
                    }
                    else
                    {
                        UpdateStreak(userUpdate, startOfWeek);
                    }
                    
                    UnlockStreakAchievement(userUpdate);
                    UnlockPointsAchievement(userUpdate);

                    await _dbContext.AddAsync(mindfulnessEntity);
                    await _dbContext.SaveChangesAsync();
                    result.Success = true;
                }
                catch (Exception ex)
                {

                    result.ErrorMessages = new List<string> { "Failed to update mindfulness table." };
                    _logger.LogError(ex, "Failed to update mindfulness table.");
                    return result;
                }
            }

            return result;
        }

        public async Task<MindfulnessEntity?> GetMindfulnessPoints(long userId)
        {
            var points = await _dbContext.Mindfulness.FirstOrDefaultAsync(rec => rec.UserId == userId);

            return points;
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
