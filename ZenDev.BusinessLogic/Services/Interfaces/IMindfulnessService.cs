using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IMindfulnessService
    {
        public Task<ResultModel> AddMindfulnessPoints(MindfulnessEntity mindfulnessEntity);
        public Task<MindfulnessEntity?> GetMindfulnessPoints(long userId);
    }
}
