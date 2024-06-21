using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class ChallengeListModel
    {
        public long ChallengeId {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
        public required ExerciseEntity ExerciseEntity {get;set;}
        public long GroupId {get;set;}
        public required GroupEntity GroupEntity {get;set;}
        public long Admin {get;set;}
    }
}