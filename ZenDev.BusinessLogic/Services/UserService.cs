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
        private readonly ILogger<ExampleService> _logger;

        public UserService(
            ZenDevDbContext dbContext,
            ILogger<ExampleService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user=> user.UserEmail == email);

            return user;
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            var result = await _dbContext.Users.ToListAsync();

            return result;
        }

        public async Task<ResultModel> CreateUserAsync(UserEntity user)
        {
            var result = new ResultModel
            {
                Success = false
            };

            try
            {
                var matchingUserEntries = _dbContext.Users
                                        .Where((record) => record.UserEmail == user.UserEmail)
                                        .ToList();

                if(matchingUserEntries.Count != 0)
                {
                    _logger.LogInformation("User already exists!");
                    result.Success = true;

                    return result;
                }

                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
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
    }
}
