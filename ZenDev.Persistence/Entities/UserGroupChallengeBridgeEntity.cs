using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class UserGroupChallengeBridgeEntity
    {

        [Key]
        public long UserGroupChallengeId {get;set;}
        public long UserGroupId {get;set;}
        [ForeignKey(nameof(UserGroupId))]
        public required UserGroupBridgeEntity UserGroupBridgeEntity {get;set;}
        public long ChallengeId{get;set;}
        [ForeignKey(nameof(ChallengeId))]
        public required ChallengeEntity ChallengeEntity {get;set;}

    }
}