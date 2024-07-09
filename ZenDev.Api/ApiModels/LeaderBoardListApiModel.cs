using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Api.ApiModels
{
    public class LeaderBoardListApiModel
    {
        public long ActivityRecordId {get;set;}
        public long UserId {get;set;}
        public UserInviteApiModel UserInviteApiModel {get;set;}
        public double Points {get;set;}
    }
}