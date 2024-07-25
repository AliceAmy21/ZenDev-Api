using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence.Entities;

namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class MessageController(
        IMessageService messageService,
        IMapper mapper
    ): ControllerBase
    {
        private readonly IMessageService _messageService = messageService;
        private readonly IMapper _mapper = mapper; 

        [HttpGet(nameof(GetAllMessagesForChat))]
        public async Task<ActionResult<List<MessageApiModel>>> GetAllMessagesForChat(long groupId){
            return Ok(_mapper.Map<List<MessageApiModel>>(_messageService.GetAllMessagesForChat(groupId)));
        }

        [HttpGet(nameof(GetChatroom))]
        public async Task<ActionResult<ChatroomApiModel>> GetChatroom(long groupId) {

            var result = await _messageService.GetChatroom(groupId);

            if (result == null) return NotFound();

            return Ok(_mapper.Map<ChatroomApiModel>(result));
        }
        

        [HttpPost(nameof(AddReactionToMessage))]
        public async Task AddReactionToMessage(ReactionApiModel reactionApi){
            await _messageService.AddReactionToMessage(_mapper.Map<ReactionModel>(reactionApi));
        }

        [HttpDelete(nameof(RemoveReactionFromMessage))]
        public async Task RemoveReactionFromMessage(long reactionId){
            await _messageService.RemoveReactionFromMessage(reactionId);
        }
    }
}