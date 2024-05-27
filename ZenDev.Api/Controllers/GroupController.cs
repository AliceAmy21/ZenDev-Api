using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenDev.BusinessLogic.Services.Interfaces;


namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
    }
}
