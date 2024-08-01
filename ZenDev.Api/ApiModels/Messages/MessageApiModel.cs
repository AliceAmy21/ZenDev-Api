
namespace ZenDev.Api.ApiModels
{
    public class MessageApiModel
    {
        public long MessageId {get;set;}
        public long UserId {get;set;}
        public UserInviteApiModel UserInviteApiModel {get;set;}
        public string MessageContent {get;set;}
        public DateTimeOffset DateSent {get;set;}
        public bool Shareable {get;set;}
        public List<ReactionApiModel> ReactionApiModels {get;set;}
    }
}