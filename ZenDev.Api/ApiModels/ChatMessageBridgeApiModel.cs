using System.ComponentModel.DataAnnotations.Schema;
using ZenDev.Persistence.Entities;

namespace ZenDev.Api.ApiModels
{
    public class ChatMessageBridgeApiModel
    {
        public long ChatMessageId { get; set; }
        public long ChatId { get; set; }
        public ChatroomApiModel ChatroomApiModel { get; set; }
        public long MessageId { get; set; }
        public MessageApiModel MessageApiModel { get; set; }
    }
}
