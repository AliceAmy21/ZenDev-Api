using ZenDev.Common.Helpers;
using ZenDev.Persistence.Constants;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class ChallengeListModel
    {
        public long ChallengeId {get;set;}
        public string ChallengeName {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountCompleted {get;set;}
        public long AmountToComplete {get;set;}
        public Measurement Measurement {get;set;}
        public long ExerciseId {get;set;}
        public required ExerciseEntity ExerciseEntity {get;set;}
        public long GroupId {get;set;}
        public required GroupEntity GroupEntity {get;set;}
        public long Admin {get;set;}
    }
}