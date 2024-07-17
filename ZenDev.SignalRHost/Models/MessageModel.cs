using System.Globalization;

namespace ZenDev.SignalRHost.Models
{
    public class MessageModel
    {
        public long MessageId { get; set; } 
        public string MessageContent { get; set; }
        public DateTime TimeSent { get; set; }

    }
}
