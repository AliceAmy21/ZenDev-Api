using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class SaveMessageModel
    {
        public long MessageId { get; set; }
        public long UserId { get; set; }
        public string MessageContent { get; set; } = string.Empty;
        public DateTimeOffset DateSent { get; set; }
        public bool Shareable { get; set; } = false;
        public long ChatId { get; set; }
    }
}
