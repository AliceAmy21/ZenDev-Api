using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class ChallengeEntity
    {
        [Key]
        public long ChallengeId {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
        [ForeignKey(nameof(ExerciseId))]
        public ExerciseEntity ExerciseEntity {get;set;}
        public long GroupId {get;set;}
        [ForeignKey(nameof(GroupId))]
        public GroupEntity GroupEntity {get;set;}
        public List<UserChallengeBridgeEntity> UserChallengeBridgeEntities {get;set;} = [];
        public long Admin {get;set;}

    }
}