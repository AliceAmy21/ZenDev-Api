using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Api.ApiModels
{
    public class UserHomePageApiModel
    {
        public long ActivityRecordId {get;set;}
        public long UserId {get;set;}
        public long Points {get;set;}
        public double? Distance {get;set;}
        public long? Duration {get;set;}
        public DateTimeOffset DateTime {get;set;}
        public string SummaryPolyline { get; set; } = string.Empty;
        public double Calories { get; set; }
        public double AverageSpeed { get; set; }
        public List<int> ActiveDays {get;set;} = [];
    }
}