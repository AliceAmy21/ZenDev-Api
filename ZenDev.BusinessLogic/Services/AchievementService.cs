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

        public async Task<List<List<AchievementEntity>>> GetAchievements(long userId)
        {
            var myAchievements = _dbContext.UserAchievementBridge
            .Where(userAchievementBridge => userAchievementBridge.UserId == userId)
            .Select(userAchievementBridge => userAchievementBridge.AchievementId)
            .ToList();

            var myAchievementResult = _dbContext.Achievements
                .Where(achievement => myAchievements.Contains(achievement.AchievementId))
                .ToList();
                
             var otherAchievementResult = _dbContext.Achievements
                .Where(achievement => !myAchievements.Contains(achievement.AchievementId))
                .ToList();

            List<List<AchievementEntity>> result = [];
            result.Add(myAchievementResult);
            result.Add(otherAchievementResult);

            return result;
        }
    }
}
