using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Helpers;


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
            return (Ok(_mapper.Map<List<GroupApiModel>>(groups)));
        }
    }
}
