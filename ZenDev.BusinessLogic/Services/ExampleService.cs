using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class ExampleService : IExampleService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<ExampleService> _logger;

        public ExampleService(
            ZenDevDbContext dbContext,
            ILogger<ExampleService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ExampleEntity?> GetExampleByIdAsync(long id)
        {
            var result = await _dbContext.Examples
                .FirstOrDefaultAsync(example => example.Id == id);

            return result;
        }

        public async Task<List<ExampleEntity>> GetAllExamplesAsync()
        {
            var result = await _dbContext.Examples
                .ToListAsync();

            return result;
        }

        public async Task<ResultModel> CreateExampleAsync(ExampleEntity example)
        {
            var result = new ResultModel
            {
                Success = false,
            };

            try
            {
                await _dbContext.AddAsync(example);

                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to create an example");
            }

            result.Success = true;

            return result;
        }
    }
}
