using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Migrations.configurations
{
    public class UCBridgeConfig
    {
        public long UserChallengeId {get;set;}
        public long UserId {get;set;}
        public long ChallengeId{get;set;}
        public DateTimeOffset DateCompleted {get;set;}
        public long AmountCompleted {get;set;}
    }
}