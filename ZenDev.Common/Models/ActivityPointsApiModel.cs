namespace ZenDev.Common.Models
{
    public class ActivityPointsApiModel
    {
        public long Id { get; set; }
        public int MovingTime { get; set; }
        public int ElapsedTime { get; set; }
        public bool HasHeartrate { get; set; }
        public double? AverageHeartrate { get; set; }
        public double? MaxHeartrate { get; set; }
    }
}
