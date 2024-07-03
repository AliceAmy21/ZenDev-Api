using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Helpers;
using ZenDev.Persistence.Entities;


namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(
            IGroupService groupService,
            IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetAllGroups))]
        public async Task<ActionResult<List<GroupListApiModel>>> GetAllGroups([FromQuery] GroupQueryObject query, long userId)
        {
            var groups = await _groupService.getAllGroupsAsync(query, userId);

            return Ok(_mapper.Map<List<GroupApiModel>>(groups));
        }

        [HttpPost(nameof(CreateGroup))]
        public async Task<ActionResult<GroupApiModel>> CreateGroup(GroupResultApiModel groupResult)
        {
            var groupResultModel = _mapper.Map<GroupResultModel>(groupResult);

            var result = await _groupService.CreateGroupAsync(groupResultModel);

            return Ok(_mapper.Map<GroupResultApiModel>(result));
        }

        [HttpPost(nameof(CreateUserGroupBridge))]
        public async Task<ActionResult<UserGroupBridgeApiModel>> CreateUserGroupBridge(UserGroupBridgeApiModel userGroupBridge)
        {
            var userGroupBridgeEntity = _mapper.Map<UserGroupBridgeEntity>(userGroupBridge);

            var result = await _groupService.CreateUserGroupBridgeAsync(userGroupBridgeEntity);

            return Ok(_mapper.Map<UserGroupBridgeEntity>(result));
        }

        [HttpDelete(nameof(DeleteGroup))]
        public async Task<ActionResult<ResultApiModel>> DeleteGroup(long groupId)
        {
            var result = await _groupService.DeleteGroupAsync(groupId);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }

        [HttpPost(nameof(UpdateGroup))]
        public async Task<ActionResult<GroupApiModel>> UpdateGroup(GroupApiModel group)
        {
            var groupEntity = _mapper.Map<GroupEntity>(group);

            var result = await _groupService.UpdateGroupAsync(groupEntity);

            return Ok(_mapper.Map<GroupApiModel>(result));
        }

        [HttpGet(nameof(GetAllGroupExercises))]
        public async Task<ActionResult<List<ExerciseTypeApiModel>>> GetAllGroupExercises()
        {
            var result = await _groupService.GetGroupExercisesAsync();

            if (result == null) return NotFound();

            return Ok(_mapper.Map<List<ExerciseTypeApiModel>>(result));
        }

        [HttpGet(nameof(GetGroupMembers))]
        public async Task<ActionResult<List<UserInviteApiModel>>> GetGroupMembers(long groupId)
        {
            var result = await _groupService.GetGroupMembers(groupId);

            if (result == null) return NotFound();

            return Ok(_mapper.Map<List<UserInviteApiModel>>(result));
        }

    }
}
