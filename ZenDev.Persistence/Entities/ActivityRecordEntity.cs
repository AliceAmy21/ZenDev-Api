using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public double? Distance {get;set;}
        public long? Duration {get;set;}
        public DateTimeOffset DateTime {get;set;}
        public string SummaryPolyline { get; set; } = string.Empty;
        public double Calories { get; set; }
        public double AverageSpeed { get; set; }
    }
}