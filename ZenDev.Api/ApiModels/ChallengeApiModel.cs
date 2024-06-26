using ZenDev.Common.Helpers;
using ZenDev.Persistence.Constants;

namespace ZenDev.Api.ApiModels
{
    public class ChallengeApiModel
    {
        public long ChallengeId {get;set;}
        public string ChallengeName {get;set;}
        public string ChallengeDescription {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountCompleted {get;set;} = 0;
        public Measurement Measurement {get;set;}
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
        public ExerciseApiModel ExerciseApiModel {get;set;}
        public long GroupId {get;set;}
        public GroupApiModel GroupApiModel {get;set;}
        public long UserId {get;set;}
        public long Admin {get;set;}
    }
}