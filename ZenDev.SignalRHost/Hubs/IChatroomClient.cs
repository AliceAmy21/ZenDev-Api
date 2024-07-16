namespace ZenDev.SignalRHost.Hubs
{
    public interface IChatroomClient
    { 
        public Task AddToGroup(long groupId);
        public Task RemoveFromGroup(long groupId);
        public Task SendMessage(long userId, string messageContent, long groupId);
        public Task AddReaction(long userId, long messageId, long reactionId, long groupId);
        public Task RemoveReaction(long userId, long messageId, long reactionId, long groupId);

    }
}
