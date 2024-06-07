namespace ZenDev.Api.ApiModels
{
    public class ChallengeApiModel
    {
        public long ChallengeId {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountToComplete {get;set;}
        public required ExerciseApiModel ExerciseEntity {get;set;}
        public required GroupApiModel GroupEntity {get;set;}
        public required List<UserGroupChallengeBridgeApiModel> UserGroupChallengeBridgeEntities {get;set;} = [];

    }
}