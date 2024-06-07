
namespace ZenDev.Api.ApiModels
{
    public class UserGroupChallengeBridgeApiModel
    {
        public long UserGroupChallengeId {get;set;}
        public required UserGroupBridgeApiModel UserGroupBridgeEntity;
        public long ChallengeId;
    }
}