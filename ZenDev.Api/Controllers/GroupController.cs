using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Services;
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
        public async Task<ActionResult<List<GroupApiModel>>> GetAllGroups([FromQuery] GroupQueryObject query, long userId)
        {
            var groups = await _groupService.getAllGroupsAsync(query, userId);

            return Ok(_mapper.Map<List<GroupApiModel>>(groups));
        }

        [HttpPost(nameof(CreateGroup))]
        public async Task<ActionResult<GroupApiModel>> CreateGroup(GroupApiModel group)
        {
            var groupEntity = _mapper.Map<GroupEntity>(group);

            var result = await _groupService.CreateGroupAsync(groupEntity);

            return Ok(_mapper.Map<GroupApiModel>(result));
        }

        [HttpGet(nameof(GetAllGroupExercises))]
        public async Task<ActionResult<List<ExerciseTypeApiModel>>> GetAllGroupExercises()
        {
            var result = await _groupService.GetGroupExercisesAsync();

            if (result == null) return NotFound();

            return Ok(_mapper.Map<List<ExerciseTypeApiModel>>(result));
        }

    }
}
