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

                    return result;
                }

                user.ActiveWeek = GetStartOfWeek(DayOfWeek.Monday);
                _logger.LogInformation(user.ActiveWeek + " Start of the week");
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

        public static DateTime GetStartOfWeek(DayOfWeek startOfWeek)
        {
            var currentDate = DateTime.Now;
            int diff = (7 + (currentDate.DayOfWeek - startOfWeek)) % 7; //Gets the number of days to subtract to get to the start of the week
            return currentDate.AddDays(-diff).Date; // .Date is used to set the time to midnight
        }

        public async Task<UserHomePageModel> GetLatestActivityRecord(long userId)
        {
            var userActivities = _dbContext.ActivityRecords
                .Include(e => e.ExerciseEntity)
                .Where(u=>u.UserId == userId)
                .OrderByDescending(d=>d.DateTime);

            if (userActivities.Count() <= 0)
            {
                return new UserHomePageModel();
            }

            var records = userActivities.ToList()[0];
            List<int> activeDays = [];
            var day = Convert.ToInt32(DateTime.Now.DayOfWeek)-1;
            if (day < 0)
            {
                day = 6;
            }

            for(int i = day; i>=0; i--){
                int j = day - i;
                var date = DateTime.Now.AddDays(-j).Date;
                if(await userActivities.AnyAsync(d=>d.DateTime.Date == date)){
                   activeDays.Add(i);
                }
            }
            UserHomePageModel userHomePageModel = new UserHomePageModel{
                ActivityRecordId = records.ActivityRecordId,
                UserId = records.UserId,
                ExerciseName = records.ExerciseEntity.ExerciseName,
                Points = records.Points,
                Distance = records.Distance,
                Duration = records.Duration,
                DateTime = records.DateTime,
                SummaryPolyline = records.SummaryPolyline,
                Calories = records.Calories,
                AverageSpeed = records.AverageSpeed,
                ActiveDays = activeDays,
                StartLatitiude = records.StartLatitiude,
                StartLongitude = records.StartLongitude,
                EndLatitude = records.EndLatitude,
                EndLongitude = records.EndLongitude
            };
            return userHomePageModel;
        }
    }
}
