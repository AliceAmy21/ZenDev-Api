using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class PersonalGoalEntity
    {
        [Key]
        public long GoalId { get; set; }

        public DateTimeOffset GoalStartDate { get; set; }

        public DateTimeOffset GoalEndDate { get; set; }

        public long UserId { get; set; }

        public long ExerciseId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity { get; set; }

        [ForeignKey(nameof(ExerciseId))]
        public ExerciseEntity ExerciseEntity { get; set; }
    }
}