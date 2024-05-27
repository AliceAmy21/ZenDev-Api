using System.ComponentModel.DataAnnotations;

namespace ZenDev.Persistence.Entities
{
    public class ExerciseTypeEntity
    {
        [Key]
        public long ExerciseTypeId { get; set; }

        [MaxLength(100)]
        public string ExerciseType { get; set; } = string.Empty;
    }
}