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
    }
}
