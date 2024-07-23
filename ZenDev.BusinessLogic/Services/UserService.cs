using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(
            ZenDevDbContext dbContext,
            ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user=> user.UserEmail == email);

            return user;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            var result = await _dbContext.Users.ToListAsync();

            return result;
        }

        public async Task<UserResultModel> CreateUserAsync(UserEntity user)
        {
            var result = new UserResultModel
            {
                Success = false,
                UserId = -1,
            };
        
            try
            {
                var matchingUserEntries = _dbContext.Users
                    .FirstOrDefault(record => record.UserEmail == user.UserEmail);
        
                if(matchingUserEntries != null)
                {
                    _logger.LogInformation("User already exists!");
                    result.Success = true;
                    result.UserId = matchingUserEntries.UserId;

                    var lastActive = matchingUserEntries.LastActive;
                    UpdateLastActive(matchingUserEntries);

                    long streak = UpdateStreak(lastActive, matchingUserEntries);

                    UnlockStreakAchievement(streak, matchingUserEntries);
        
                    return result;
                }
        
                var newUser = await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                result.UserId = newUser.Entity.UserId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create the user.");
        
                return result;
            }
            _logger.LogInformation("User created successfully");
            result.Success = true;
        
            return result;
        }

        public async Task<ResultModel> AddRefreshTokenAsync(string email, string refreshToken)
        {
            var result = new ResultModel
            {
                Success = false
            };

            var user = await _dbContext.Users.FirstOrDefaultAsync(user=> user.UserEmail == email);

            if(user != null)
            {
                try
                {
                    user.StravaRefreshToken = refreshToken;

                    _dbContext.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    result.ErrorMessages = new List<string> { "Failed to add token." };
                    _logger.LogError(ex, "Failed to add token.");
                    return result;
                }

                result.Success = true;
            }

            return result;
        }

        public void UpdateLastActive(UserEntity user){
            user.LastActive = DateTime.Now;
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }

        public long UpdateStreak(DateTimeOffset lastActive, UserEntity user){
            var updatedLastActive = user.LastActive;
            int daysSinceLastActive = (updatedLastActive.Date - lastActive.Date).Days;

            if (daysSinceLastActive == 1)
            {
                user.Streak++;
            }
            else if (daysSinceLastActive > 1)
            {
                user.Streak = 0;
            }

            _dbContext.Update(user);
            _dbContext.SaveChanges();

            _logger.LogInformation(user.Streak.ToString() + " day streak");
            return user.Streak;
        }

        public void UnlockStreakAchievement(long userStreak, UserEntity user){
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

                if (userStreak >= streak)
                {
                    if (myAchievements.Contains(streakAchievements[i].AchievementId))
                    {
                        continue;
                    }
                    else{
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

        public async Task<UserHomePageModel> GetLatestActivityRecord(long userId)
        {
            var records = _dbContext.ActivityRecords.Where(u=>u.UserId == userId).OrderByDescending(d=>d.DateTime).ToListAsync().Result.ElementAt(0);
            List<int> activeDays = [];
            var day = DateTime.Now.DayOfWeek;
            UserHomePageModel userHomePageModel = new UserHomePageModel{
                ActivityRecordId = records.ActivityRecordId,
                UserId = records.UserId,
                Points = records.Points,
                Distance = records.Distance,
                Duration = records.Duration,
                DateTime = records.DateTime,
                SummaryPolyline = records.SummaryPolyline,
                Calories = records.Calories,
                AverageSpeed = records.AverageSpeed,
                ActiveDays = activeDays,
            };
            
            return userHomePageModel;
        }
    }
}
