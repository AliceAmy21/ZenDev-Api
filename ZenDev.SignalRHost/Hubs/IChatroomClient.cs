using System.Threading.Tasks;

namespace ZenDev.SignalRHost.HubConfig
{
    public interface IChatroomHub
    {
        Task AddToGroup(long groupId);
        Task RemoveFromGroup(long groupId);
        Task SendMessage(long userId, string messageContent, long groupId, DateTime timeSent);
        Task AddReaction(long userId, long messageId, long reactionId, long groupId);
        Task RemoveReaction(long userId, long messageId, long reactionId, long groupId);
        Task AskServer(string someTextFromClient);
    }
}
