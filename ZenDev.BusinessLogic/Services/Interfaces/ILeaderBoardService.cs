using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenDev.BusinessLogic.Models;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface ILeaderBoardService
    {
        public List<LeaderBoardListModel> GetAllLeaderBoardDataForChallenge(long challengeId);
        public List<LeaderBoardListModel> GetAllLeaderBoardDataForGroups(long groupId);
    }
}