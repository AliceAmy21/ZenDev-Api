using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            ReactionEntity reactionEntity = new ReactionEntity
            {
                Reaction = reaction.Reaction,
                UserId = reaction.UserId,
            };
            await _dbContext.Reactions.AddAsync(reactionEntity);

            MessageReactionBridgeEntity messageReactionBridgeEntity = new MessageReactionBridgeEntity
            {
                MessageId = reaction.messageId,
                ReactionId = reactionEntity.ReactionId
            };
            await _dbContext.MessageReactionBridge.AddAsync(messageReactionBridgeEntity);
        }

        public Task<List<MessageModel>> GetAllMessagesForChat(long groupId)
        {
            var chatMessageBridge = _dbContext.ChatMessageBridge
                .Include(message => message.MessageEntity)
                .Include(chat => chat.ChatroomEntity)
                .Include(reaction => reaction.MessageEntity.messageReactionBridges)
                .AsQueryable();

            List<MessageModel> messageModels = [];
            var messages = chatMessageBridge.Where(group => group.ChatroomEntity.GroupId == groupId)
                .Select(message => message.MessageEntity);

            foreach (var message in messages)
            {
                var messageComplete = new MessageModel
                {
                    MessageId = message.MessageId,
                    MessageContent = message.MessageContent,
                    UserId = message.UserId,
                    UserEntity = message.UserEntity,
                    DateSent = message.DateSent,
                    Shareable = message.Shareable,
                    ReactionEntities = message.messageReactionBridges.Where(reaction => reaction.MessageId == message.MessageId)
                    .Select(reaction => reaction.ReactionEntity).ToList(),
                };
                messageModels.Add(messageComplete);
            }
            return Task.FromResult(messageModels.OrderByDescending(time => time.DateSent).ToList());
        }

        public async Task RemoveReactionFromMessage(long reactionId)
        {
            ReactionEntity reactionEntity = await _dbContext.Reactions.FindAsync(reactionId);
            if (reactionEntity != null)
                _dbContext.Reactions.Remove(reactionEntity);
        }

        public async Task<ChatroomEntity> GetChatroom(long groupId)
        {
            return await _dbContext.Chatrooms.FirstOrDefaultAsync(c => c.GroupId == groupId);
        }

        public async Task<List<ChatroomModel>> GetAllChatsByUserId(long userId)
        {
            var groups = _dbContext.UserGroupBridge.Where(g => g.UserId == userId)
                .Include(group => group.GroupEntity)
                .Select(groups => groups.GroupEntity)
                .ToList();

            var allChats = new List<ChatroomModel>();

            foreach (var chat in groups)
            {
                var lastMessage = GetLastMessage(chat.GroupId);
                var chatroom = await _dbContext.Chatrooms.SingleOrDefaultAsync(c => c.GroupId == chat.GroupId);
                allChats.Add(new ChatroomModel
                {
                    ChatId = chatroom?.ChatId ?? 0,
                    GroupId = chat.GroupId,
                    GroupEntity = chat,
                    LastMessage = lastMessage.MessageContent,
                });
            }

            return allChats;
        }

        public async Task<ResultModel> SaveMessage(SaveMessageModel messageModel)
        {
            var result = new ResultModel()
            {
                Success = false,
            };

            try
            {
                var message = new MessageEntity()
                {
                    UserId = messageModel.UserId,
                    MessageContent = messageModel.MessageContent,
                    DateSent = messageModel.DateSent,
                    Shareable = messageModel.Shareable,
                };

                var saveMessage = await _dbContext.AddAsync(message);
                await _dbContext.SaveChangesAsync();

                var chatMessageBridge = new ChatMessageBridgeEntity()
                {
                    MessageId = saveMessage.Entity.MessageId,
                    ChatId = messageModel.ChatId,
                };

                await _dbContext.AddAsync(chatMessageBridge);
                await _dbContext.SaveChangesAsync();
                result.Success = true;
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessages = new List<string> { ex.Message };
            }

            return result;
        }

        public LastMessageModel GetLastMessage(long groupId)
        {
            var chatMessageBridge = _dbContext.ChatMessageBridge
                .Include(message => message.MessageEntity)
                .Include(chat => chat.ChatroomEntity)
                .Where(group => group.ChatroomEntity.GroupId == groupId)
                .OrderByDescending(message => message.MessageEntity.DateSent).ToList();


            if (chatMessageBridge.Count() > 0)
            {
                var firstMessage = chatMessageBridge.First();
                var lastMessage = new LastMessageModel()
                {
                    MessageId = firstMessage.MessageId,
                    MessageContent = firstMessage.MessageEntity.MessageContent,
                    DateSent = firstMessage.MessageEntity.DateSent,
                    ChatId = firstMessage.ChatId,
                    GroupId = firstMessage.ChatroomEntity.GroupId,
                };

                return lastMessage;
            }
            else
            {
                return new LastMessageModel();
            }

        }

        public async Task<List<ChatMessageBridgeEntity>> GetLastGroupMessage(long groupId)
        {
            var chatMessageBridge = _dbContext.ChatMessageBridge
                 .Include(chat => chat.ChatroomEntity)
                 .Where(group => group.ChatroomEntity.GroupId == groupId)
                 .ToList();

            var temp = new List<ChatMessageBridgeEntity>();

            foreach(var c in chatMessageBridge)
            {
                var cbm = new ChatMessageBridgeEntity();
                cbm.ChatId = c.ChatId;
                cbm.MessageEntity = c.MessageEntity;
                cbm.ChatroomEntity = c.ChatroomEntity;
            }
            return temp;
        }

    }
}