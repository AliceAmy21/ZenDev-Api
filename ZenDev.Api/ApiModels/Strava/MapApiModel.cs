namespace ZenDev.Api.ApiModels.Strava
{
    public class MapApiModel
    {
        public int Id { get; set; }
        public string summaryPolyline { get; set; } = string.Empty;
        public int resourceState { get; set; }
    }
}
