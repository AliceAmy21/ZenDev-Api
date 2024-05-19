using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IPersonalGoalService
    {
        public Task<PersonalGoalEntity?> GetGoalByIdAsync(long id);
        public Task<List<PersonalGoalEntity>> GetAllGoalsAsync(long userId);
        public Task<PersonalGoalEntity> CreateGoalAsync(PersonalGoalEntity goal);
        public Task<ResultModel> UpdateGoalAsync(PersonalGoalEntity goal);
        public Task<ResultModel> DeleteGoalAsync(long id);
    }
}
