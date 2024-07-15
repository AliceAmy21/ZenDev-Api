namespace ZenDev.SignalRHost.Hubs
{
    public interface IChatroomClient
    { 
        Task AddToGroup(long groupId);
        Task RemoveFromGroup(long groupId);
        Task SendMessage(long userId, string messageContent, long groupId);
        Task AddReaction(long userId, long messageId, long reactionId, long groupId);
        Task RemoveReaction(long userId, long messageId, long reactionId, long groupId);

    }
}
