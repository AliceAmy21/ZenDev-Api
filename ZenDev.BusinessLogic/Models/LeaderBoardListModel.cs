using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class LeaderBoardListModel
    {
        public long ActivityRecordId {get;set;}
        public long UserId {get;set;}
        public UserInviteModel UserInviteModel {get;set;}
        public double Points {get;set;}
    }
}