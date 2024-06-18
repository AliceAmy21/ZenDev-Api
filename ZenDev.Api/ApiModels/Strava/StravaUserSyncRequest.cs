namespace ZenDev.Api.ApiModels.Strava
{
    public class StravaUserSyncRequest
    {
        public long UserId { get; set; }
        public DateTimeOffset SyncDate { get; set; }
    }
}
