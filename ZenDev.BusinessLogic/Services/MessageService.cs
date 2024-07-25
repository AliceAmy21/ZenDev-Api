using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<MessageService> _logger;
        
        public MessageService(
            ZenDevDbContext dbContext,
            ILogger<MessageService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddReactionToMessage(ReactionModel reaction)
        {
            ReactionEntity reactionEntity = new ReactionEntity{
                Reaction = reaction.Reaction,
                UserId = reaction.UserId,
            };
            await _dbContext.Reactions.AddAsync(reactionEntity);

            MessageReactionBridgeEntity messageReactionBridgeEntity = new MessageReactionBridgeEntity{
                MessageId = reaction.messageId,
                ReactionId = reactionEntity.ReactionId
            };
            await _dbContext.MessageReactionBridge.AddAsync(messageReactionBridgeEntity);
        }

        public Task<List<MessageModel>> GetAllMessagesForChat(long groupId)
        {
            var chatMessageBridge = _dbContext.ChatMessageBridge
                .Include(message=> message.MessageEntity)
                .Include(chat=>chat.ChatroomEntity)
                .Include(reaction=>reaction.MessageEntity.messageReactionBridges)
                .AsQueryable();

            List<MessageModel> messageModels = [];  
            var messages = chatMessageBridge.Where(group => group.ChatroomEntity.GroupId == groupId)
                .Select(message=>message.MessageEntity);

            foreach(var message in messages){
                var messageComplete = new MessageModel{
                    MessageId = message.MessageId,
                    MessageContent = message.MessageContent,
                    UserId = message.UserId,
                    UserEntity =message.UserEntity,
                    DateSent =message.DateSent,
                    Shareable =message.Shareable,
                    ReactionEntities = message.messageReactionBridges.Where(reaction=>reaction.MessageId == message.MessageId)
                    .Select(reaction=>reaction.ReactionEntity).ToList(),
                };  
                messageModels.Add(messageComplete);         
                }
            return Task.FromResult(messageModels.OrderByDescending(time=>time.DateSent).ToList());
        }

        public async Task RemoveReactionFromMessage(long reactionId)
        {
            ReactionEntity reactionEntity = await _dbContext.Reactions.FindAsync(reactionId);
            if(reactionEntity!=null)
                _dbContext.Reactions.Remove(reactionEntity);
        }

        public async Task<ChatroomEntity> GetChatroom(long groupId)
        {
            return await _dbContext.Chatrooms.FirstOrDefaultAsync(c => c.GroupId == groupId);
        }

        //public async Task<ChatroomModel> GetGroupInfo(long groupId)
        //{
        //    var group = _dbContext.Groups
        //        .Include(group )
        //}
    }
}