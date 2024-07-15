using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenDev.BusinessLogic.Models;
using ZenDev.Common.Enums;
using ZenDev.Common.Models;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IPointsService
    {
        public Task<DateTimeOffset?> SetLastSyncedDateAsync(long userId);
        public Task<DateTimeOffset?> GetLastSyncedDateAsync(long userId);
        public void CalculatePoints(List<ActivityPointsApiModel> activities);
        public int CalculatePointsGroups(ActivityPointsApiModel activity);
        public Task UpdateAmountCompleteChallenges(long userId, List<ActivityPointsApiModel> activities);
        public Task UpdatePointsGroups(long userId, List<ActivityPointsApiModel> activities);
        public Task UpdateTournamentPoints(long userId, List<ActivityPointsApiModel> activities);
    }
}
