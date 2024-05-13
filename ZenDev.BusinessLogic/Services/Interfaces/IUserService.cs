using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserEntity?> GetUserByEmailAsync(string email);
        public Task<List<UserEntity>> GetAllUsers();
        public Task<ResultModel> CreateUserAsync(UserEntity user);
        public Task<ResultModel> AddRefreshTokenAsync(string email, string refreshToken);
    }
}
