namespace ZenDev.Api.ApiModels
{
    public class ChallengeApiModel
    {
        public long ChallengeId {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
        public required ExerciseApiModel ExerciseEntity {get;set;}
        public long GroupId {get;set;}
        public required GroupApiModel GroupEntity {get;set;}
        public long userId {get;set;}
        public required List<UserApiModel> UserEntities {get;set;} = [];

    }
}