using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Persistence.Entities
{
    public class ActivityRecordEntity
    {
        [Key]
        public long ActivityRecordId {get;set;}
        public long UserId {get;set;}
        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntities {get;set;}
        public long Points {get;set;}
        public long Distance {get;set;}
        public long Duration {get;set;}
        public DateTimeOffset DateTime {get;set;}
    }
}