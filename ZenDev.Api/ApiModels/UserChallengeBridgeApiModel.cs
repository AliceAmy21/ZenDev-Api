
namespace ZenDev.Api.ApiModels
{
    public class UserChallengeBridgeApiModel
    {
        public long UserChallengeId {get;set;}
        public UserApiModel UserEntity {get;set;}
        public long ChallengeId {get;set;}
        public ChallengeApiModel ChallengeEntity {get;set;}
    }
}