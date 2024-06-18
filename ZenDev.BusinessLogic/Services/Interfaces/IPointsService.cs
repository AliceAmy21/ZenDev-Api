using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenDev.Common.Enums;
using ZenDev.Common.Models;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IPointsService
    {
        public Task<DateTimeOffset?> SetLastSyncedDateAsync(long userId);
        public Task<DateTimeOffset?> GetLastSyncedDateAsync(long userId);
        public int CalculatePoints(List<ActivityPointsApiModel> activities);
    }
}
