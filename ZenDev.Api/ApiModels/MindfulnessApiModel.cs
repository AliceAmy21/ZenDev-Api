﻿namespace ZenDev.Api.ApiModels
{
    public class MindfulnessApiModel
    {
        public long MindfulnessId { get; set; }
        public long TotalPoints { get; set; }
        public Double TotalMinutes { get; set; }
        public DateTimeOffset? LastUpdate { get; set; }
        public long UserId { get; set; }
    }
}
