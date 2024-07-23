using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class MessageEntity
    {
        [Key]
        public long MessageId {get;set;}
        public long UserId {get;set;}
        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity {get;set;}
        public string MessageContent {get;set;} = string.Empty;
        public DateTimeOffset DateSent {get;set;}
        public bool Shareable {get;set;} = false;
        public List<ChatMessageBridgeEntity> ChatMessageBridges {get;set;} = [];
        public List<MessageReactionBridgeEntity> messageReactionBridges {get;set;} = [];
    }
}