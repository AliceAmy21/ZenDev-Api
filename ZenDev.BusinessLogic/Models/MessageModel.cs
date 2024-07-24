using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class MessageModel
    {
        public long MessageId {get;set;}
        public long UserId {get;set;}
        public UserEntity UserEntity {get;set;}
        public string MessageContent {get;set;}
        public DateTimeOffset DateSent {get;set;}
        public bool Shareable {get;set;}
        public List<ReactionEntity> ReactionEntities {get;set;}
    }
}