using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Helpers;
using ZenDev.Persistence.Entities;

namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class GroupInvitationController : ControllerBase
    {
        private readonly IGroupInvitationService _groupInvitationService;
        private readonly IMapper _mapper;

        public GroupInvitationController(
            IGroupInvitationService groupInvitationService,
            IMapper mapper)
        {
            _groupInvitationService = groupInvitationService;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetInvitationsByUserId))]
        public async Task<ActionResult<List<NotificationApiModel>>> GetInvitationsByUserId(long userId)
        {
            var result = await _groupInvitationService.GetGroupInvitationsByUserIdAsync(userId);

            if (result == null) return NotFound();

            return Ok(_mapper.Map<List<NotificationApiModel>>(result));
        }

        [HttpPost(nameof(CreateGroupInvitations))]
        public async Task<ActionResult<List<GroupInvitationApiModel>>> CreateGroupInvitations(List<GroupInvitationApiModel> groupInvitations)
        {
            var groupInvitationsEntity = _mapper.Map<List<GroupInvitationEntity>>(groupInvitations);

            var result = await _groupInvitationService.CreateGroupInvitationsAsync(groupInvitationsEntity);

            var groupInvitationsApiModelResult = _mapper.Map<List<GroupInvitationApiModel>>(result);

            return Ok(groupInvitationsApiModelResult);
        }

        [HttpGet(nameof(GetNonGroupMembers))]
        public async Task<ActionResult<List<UserInviteApiModel>>> GetNonGroupMembers(long groupId)
        {
            var result = await _groupInvitationService.GetNonGroupMembers(groupId);

            if (result == null) return NotFound();

            return Ok(_mapper.Map<List<UserInviteApiModel>>(result));
        }

        [HttpGet(nameof(GetAllUsers))]
        public async Task<ActionResult<List<UserInviteApiModel>>> GetAllUsers([FromQuery] GroupInvitationQueryObject query)
        {
            var result = await _groupInvitationService.GetAllUsersAsync(query);

            return Ok(_mapper.Map<List<UserInviteApiModel>>(result));
        }

        [HttpDelete(nameof(DeleteGroupInvitation))]
        public async Task<ActionResult<ResultApiModel>> DeleteGroupInvitation(GroupInvitationApiModel groupInvitation)
        {
            var groupInvitationEntity = _mapper.Map<GroupInvitationEntity>(groupInvitation);

            var result = await _groupInvitationService.DeleteGroupInvitationAsync(groupInvitationEntity);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }

        [HttpPost(nameof(AcceptGroupInvitationAsync))]
        public async Task<ActionResult<ResultApiModel>> AcceptGroupInvitationAsync(UserGroupBridgeApiModel userGroupBridgeApiModel)
        {
            var userGroupBridgeEntity = _mapper.Map<UserGroupBridgeEntity>(userGroupBridgeApiModel);

            var result = await _groupInvitationService.AcceptGroupInvitationAsync(userGroupBridgeEntity);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }
    }
}