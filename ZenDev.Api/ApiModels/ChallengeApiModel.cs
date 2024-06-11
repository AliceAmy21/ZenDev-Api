namespace ZenDev.Api.ApiModels
{
    public class ChallengeApiModel
    {
        public long ChallengeId {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
        public ExerciseApiModel ExerciseEntity {get;set;}
        public long GroupId {get;set;}
        public GroupApiModel GroupEntity {get;set;}
        public long UserId {get;set;}
    }
}