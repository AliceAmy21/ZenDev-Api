using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.BusinessLogic.Models
{
    public class ReactionModel
    {
        public long ReactionId { get; set; }
        public long UserId {get;set;}
        public String Reaction {get;set;}
        public long MessageId {get;set;}
    }
}