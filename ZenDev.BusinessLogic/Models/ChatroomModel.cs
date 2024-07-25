using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    internal class ChatroomModel
    {
        public long ChatId { get; set; }
        public required GroupEntity GroupEntity { get; set; }
        public long GroupId { get; set; } 
        public string GroupName {  get; set; }
        public string GroupIconUrl {  get; set; }
        
    }
}
