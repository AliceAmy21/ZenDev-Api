namespace ZenDev.Api.ApiModels
{
    public class LastMessageApiModel
    {
        public long MessageId { get; set; }
        public string MessageContent { get; set; }
        public DateTimeOffset DateSent { get; set; }
        public long ChatId { get; set; }
        public long GroupId { get; set; }
    }
}
