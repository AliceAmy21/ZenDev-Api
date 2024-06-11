using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class UserChallengeBridgeEntity
    {

        [Key]
        public long UserChallengeId {get;set;}
        public long UserId {get;set;}
        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity {get;set;}
        public long ChallengeId{get;set;}
        [ForeignKey(nameof(ChallengeId))]
        public ChallengeEntity ChallengeEntity {get;set;}

    }
}