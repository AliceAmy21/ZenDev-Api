

namespace ZenDev.Api.ApiModels
{
    public class TournamentListApiModel
    {
        public long TournamentId {get;set;}
        public string TournamentName {get;set;} = string.Empty;
        public string ExerciseName {get;set;} = string.Empty;
        public DateTimeOffset StartDate {get;set;}
        public DateTimeOffset EndDate {get;set;}
    }
}