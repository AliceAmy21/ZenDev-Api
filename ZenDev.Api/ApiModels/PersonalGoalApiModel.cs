using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZenDev.Persistence.Entities;

namespace ZenDev.Api.ApiModels
{
    public class PersonalGoalApiModel
    {
        public long GoalId { get; set; }
        
        public string GoalName { get; set; } = string.Empty;

        public string GoalDescription { get; set; } = string.Empty; 

        public DateTimeOffset GoalStartDate { get; set; }

        public DateTimeOffset GoalEndDate { get; set; }

        public long AmountToComplete { get; set; }

        public long AmountCompleted { get; set; }

        public long UserId { get; set; }

        public long ExerciseId { get; set; }
        
        public string MeasurementUnit { get; set; } = string.Empty;
    }
}
