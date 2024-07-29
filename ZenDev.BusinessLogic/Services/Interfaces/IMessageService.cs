using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<List<MessageModel>> GetAllMessagesForChat(long groupId);
        public Task<ReactionModel> AddReactionToMessage(ReactionModel reaction);
        public Task<long> RemoveReactionFromMessage(long reactionId);
        public Task<ChatroomEntity> GetChatroom(long groupId);
        public Task<List<ChatroomModel>> GetAllChatsByUserId(long userId);
        public Task<List<ChatMessageBridgeEntity>> GetLastGroupMessage(long groupId);
        public Task<MessageModel> SaveMessage(SaveMessageModel messageModel);
    }
}