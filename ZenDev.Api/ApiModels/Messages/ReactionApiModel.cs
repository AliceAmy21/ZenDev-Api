
namespace ZenDev.Api.ApiModels
{
    public class ReactionApiModel
    {
        public long ReactionId {get;set;}
        public long UserId {get;set;}
        public UserInviteApiModel UserInviteApiModel {get;set;}
        public string Reaction {get;set;}
    }
}