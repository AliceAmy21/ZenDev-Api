namespace ZenDev.Api.ApiModels
{
    public class ChatroomApiModel
    {
        public long ChatId { get; set; }
        public long GroupId { get; set; }
        public LastMessageApiModel LastMessage { get; set; }
        public GroupApiModel GroupApiModel { get; set; }
    }
}