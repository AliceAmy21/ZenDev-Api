using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class ChatMessageBridge
    {
        [Key]
        public long ChatMessageId {get;set;}
        public long ChatId {get;set;}
        [ForeignKey(nameof(ChatId))]
        public ChatroomEntity ChatroomEntity {get;set;}
        public long MessageId {get;set;}
        [ForeignKey(nameof(MessageId))]
        public MessageEntity MessageEntity {get;set;}
    }
}