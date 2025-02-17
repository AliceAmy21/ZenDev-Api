using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IAchievementService
    {
        public Task<List<List<AchievementEntity>>> GetMyAchievements(long userId);

        public Task<List<List<AchievementEntity>>> GetAllAchievements();
    }
}
