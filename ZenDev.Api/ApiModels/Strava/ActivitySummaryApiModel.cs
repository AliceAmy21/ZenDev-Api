namespace ZenDev.Api.ApiModels.Strava
{
    public class ActivitySummaryApiModel
    {
        public long Id { get; set; }
        public AthleteApiModel Athlete { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public int MovingTime { get; set; }
        public int ElapsedTime { get; set; }
        public double TotalElevationGain { get; set; }
        public string Type { get; set; }
        public string SportType { get; set; }
        public int? WorkoutType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartDateLocal { get; set; }
        public string Timezone { get; set; }
        public double UtcOffset { get; set; }
        public List<double> StartLatlng { get; set; }
        public List<double> EndLatlng { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationCountry { get; set; }
        public MapApiModel Map { get; set; }
        public bool Flagged { get; set; }
        public string GearId { get; set; }
        public bool FromAcceptedTag { get; set; }
        public double AverageSpeed { get; set; }
        public double MaxSpeed { get; set; }
        public double? AverageCadence { get; set; }
        public double? AverageWatts { get; set; }
        public double? WeightedAverageWatts { get; set; }
        public double? Kilojoules { get; set; }
        public bool? DeviceWatts { get; set; }
        public bool HasHeartrate { get; set; }
        public double? AverageHeartrate { get; set; }
        public double? MaxHeartrate { get; set; }
        public double? MaxWatts { get; set; }
    }
}