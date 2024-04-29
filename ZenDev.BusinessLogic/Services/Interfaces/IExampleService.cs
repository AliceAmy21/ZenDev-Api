using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IExampleService
    {
        public Task<ExampleEntity?> GetExampleByIdAsync(long id);
        public Task<List<ExampleEntity>> GetAllExamplesAsync();
        public Task<ResultModel> CreateExampleAsync(ExampleEntity example);
    }
}
