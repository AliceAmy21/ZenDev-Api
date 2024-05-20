namespace ZenDev.Migrations
{
    public class ExerciseConfig
    {
        public long ExerciseId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public string MeasurementUnit { get; set; } = string.Empty;
    }
}