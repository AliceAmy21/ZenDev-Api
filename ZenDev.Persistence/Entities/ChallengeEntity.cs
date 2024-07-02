using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZenDev.Persistence.Constants;

namespace ZenDev.Persistence.Entities
{
    public class ChallengeEntity
    {
        [Key]
        public long ChallengeId {get;set;}
        [MaxLength(100)]
        public string ChallengeName {get;set;}
        [MaxLength(500)]
        public string ChallengeDescription {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public Measurement Measurement {get;set;}
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