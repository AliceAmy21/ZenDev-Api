﻿namespace ZenDev.Api.ApiModels.Strava
{
    public class ActivitySummaryResponse
    {
        public int resource_state { get; set; } 
        public AthleteResponse athlete { get; set; }
        public string name { get; set; }
        public double distance { get; set; }
        public int moving_time { get; set; }
        public int elapsed_time { get; set; }
        public double total_elevation_gain { get; set; }
        public string type { get; set; }
        public string sport_type { get; set; }
        public int? workout_type { get; set; }
        public long id { get; set; }
        public string external_id { get; set; }
        public long? upload_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime start_date_local { get; set; }
        public string timezone { get; set; }
        public double utc_offset { get; set; }
        public List<double> start_latlng { get; set; }
        public List<double> end_latlng { get; set; }
        public string location_city { get; set; }
        public string location_state { get; set; }
        public string location_country { get; set; }
        public int achievement_count { get; set; }
        public int kudos_count { get; set; }
        public int comment_count { get; set; }
        public int athlete_count { get; set; }
        public int photo_count { get; set; }
        public MapResponse map { get; set; }
        public bool trainer { get; set; }
        public bool commute { get; set; }
        public bool manual { get; set; }
        public bool _private { get; set; }
        public string visibility { get; set; }
        public bool flagged { get; set; }
        public string gear_id { get; set; }
        public bool from_accepted_tag { get; set; }
        public double average_speed { get; set; }
        public double max_speed { get; set; }
        public double? average_cadence { get; set; }
        public double? average_watts { get; set; }
        public double? weighted_average_watts { get; set; }
        public double? kilojoules { get; set; }
        public bool? device_watts { get; set; }
        public bool has_heartrate { get; set; }
        public bool heartrate_opt_out { get; set; }
        public bool display_hide_heartrate_option { get; set; }
        public double? average_heartrate { get; set; }
        public double? max_heartrate { get; set; }
        public double? max_watts { get; set; }
        public int pr_count { get; set; }
        public int total_photo_count { get; set; }
        public bool has_kudoed { get; set; }
        public double? suffer_score { get; set; }
    }
}
