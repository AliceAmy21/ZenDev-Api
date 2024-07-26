using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class LastMessageModel
    {
        public long MessageId { get; set; }
        public string MessageContent { get; set; }
        public DateTimeOffset DateSent { get; set; }
        public long ChatId { get; set; }
        public long GroupId { get; set; }
    }
}
