using ZenDev.Api.ApiModels;

namespace ZenDev.SignalRHost.Hubs
{
    public interface IChatroomClient
    {
        public Task ConnectionSuccessful(string message);
        public Task JoinChatSuccessful(string message);
        public Task JoinChatUnsuccessful(string message);
        public Task SendMessageSuccessful(string message);
        public Task SendMessageUnsuccessful(string message);
        public Task NewMessage(MessageApiModel message);
    }
}
