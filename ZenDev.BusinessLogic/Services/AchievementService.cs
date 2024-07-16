using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public AchievementService(
            ZenDevDbContext dbContext,
            ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<List<AchievementEntity>>> GetMyAchievements(long userId)
        {
            var myAchievements = _dbContext.UserAchievementBridge
                .Where(userAchievementBridge => userAchievementBridge.UserId == userId)
                .Select(userAchievementBridge => userAchievementBridge.AchievementId)
                .ToList();

            var myAchievementResult = await _dbContext.Achievements
                .Where(achievement => myAchievements.Contains(achievement.AchievementId))
                .ToListAsync();
                
            var otherAchievementResult = await _dbContext.Achievements
                .Where(achievement => !myAchievements.Contains(achievement.AchievementId))
                .ToListAsync();

            List<List<AchievementEntity>> result = [];
            result.Add(myAchievementResult);
            result.Add(otherAchievementResult);

            return result;
        }

        public Task<List<List<AchievementEntity>>> GetAllAchievements()
        {
            var achievements = _dbContext.Achievements.ToArray();

            List<AchievementEntity> streakAchievements = [];
            List<AchievementEntity> distanceAchievements = [];
            List<AchievementEntity> timeAchievements = [];

            foreach (var achievement in achievements)
            {
                if (achievement.AchievementName.Contains("Streak"))
                {
                    streakAchievements.Add(achievement);
                }
                else if (achievement.AchievementName.Contains("km"))
                {
                    distanceAchievements.Add(achievement);
                }
                else if (achievement.AchievementName.Contains("Hours"))
                {
                    timeAchievements.Add(achievement);
                }
            }

            List<List<AchievementEntity>> result = [];
            result.Add(streakAchievements);
            result.Add(distanceAchievements);
            result.Add(timeAchievements);

            return Task.FromResult(result);
        }
    }
}
