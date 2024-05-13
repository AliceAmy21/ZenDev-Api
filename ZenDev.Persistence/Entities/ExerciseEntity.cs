using System.ComponentModel.DataAnnotations;

namespace ZenDev.Persistence.Entities
{
    public class ExerciseEntity
    {
        [Key]
        public long ExerciseId { get; set; }

        [MaxLength(100)]
        public string ExerciseName { get; set; } = string.Empty;

        public string MeasurementUnit { get; set; } = string.Empty;
    }
}