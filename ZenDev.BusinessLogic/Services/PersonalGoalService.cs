﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class PersonalGoalService : IPersonalGoalService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<PersonalGoalService> _logger;

        public PersonalGoalService(
            ZenDevDbContext dbContext,
            ILogger<PersonalGoalService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ResultModel> CreateGoalAsync(PersonalGoalEntity goal)
        {
            var result = new ResultModel
            {
                Success = false
            };

            try
            {
                await _dbContext.AddAsync(goal);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create a personal goal");
                result.ErrorMessages = new List<string>() { "Failed to create a personal goal" };
                return result;
            }

            result.Success = true;

            return result;
        }

        public async Task<ResultModel> DeleteGoalAsync(long id)
        {
            var result = new ResultModel
            {
                Success = false
            };

            try
            {
                var recordToRemove = _dbContext.PersonalGoals.FirstOrDefault(goal => goal.GoalId == id);
                if (recordToRemove != null)
                {
                    _dbContext.Remove(recordToRemove);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete personal goal");
                result.ErrorMessages = new List<string>() { "Failed to delete personal goal" };
                return result;
            }

            result.Success = true;

            return result;
        }

        public async Task<List<PersonalGoalEntity>> GetAllGoalsAsync(long userId)
        {
            var result = await _dbContext.PersonalGoals
                .Where(goal => goal.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<PersonalGoalEntity?> GetGoalByIdAsync(long id)
        {
            var goal = await _dbContext.PersonalGoals.FirstOrDefaultAsync(goal => goal.GoalId == id);

            return goal;
        }

        public async Task<ResultModel> UpdateGoalAsync(PersonalGoalEntity goal)
        {
            var result = new ResultModel
            {
                Success = false
            };

            try
            {
                _dbContext.Update(goal);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update personal goal");
                result.ErrorMessages = new List<string>() { "Failed to update personal goal" };
                return result;
            }

            result.Success = true;

            return result;
        }
    }
}
