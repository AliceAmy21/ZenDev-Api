
namespace ZenDev.Api.ApiModels
{
    public class UserHomePageApiModel
    {
        public long ActivityRecordId {get;set;}
        public long UserId {get;set;}
        public string ExerciseName {get;set;} = string.Empty;
        public long Points {get;set;}
        public double? Distance {get;set;}
        public long? Duration {get;set;}
        public DateTimeOffset DateTime {get;set;}
        public string SummaryPolyline { get; set; } = string.Empty;
        public double Calories { get; set; }
        public double AverageSpeed { get; set; }
        public List<int> ActiveDays {get;set;} = [];
        public double StartLatitiude { get; set; }
        public double StartLongitude { get; set; }
        public double EndLatitude { get; set; }
        public double EndLongitude { get; set; }
    }
}