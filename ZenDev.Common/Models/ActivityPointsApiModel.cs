﻿namespace ZenDev.Common.Models
{
    public class ActivityPointsApiModel
    {
        public long Id { get; set; }
        public int MovingTime { get; set; }
        public int ElapsedTime { get; set; }
        public bool HasHeartrate { get; set; }
        public double? AverageHeartrate { get; set; }
        public double? MaxHeartrate { get; set; }
        public string Exercise { get; set; }
        public int? Duration { get; set; }
        public double? Distance { get; set; }
        public DateTime StartDateLocal { get; set; }
        public string SummaryPolyline { get; set; } = string.Empty;
        public double Kilojoules { get; set; }
        public double AverageSpeed { get; set; }
        public List<double> StartLatlng { get; set; } =[0,0];
        public List<double> EndLatlng { get; set; } = [0,0];

    }
}
