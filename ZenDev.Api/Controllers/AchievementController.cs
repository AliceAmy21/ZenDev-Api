using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence.Entities;

namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementService _achievementService;
        private readonly IMapper _mapper;

        public AchievementController(
            IAchievementService achievementService,
            IMapper mapper)
        {
            _achievementService = achievementService;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetAllAchievements))]
        public async Task<ActionResult<List<AchievementApiModel>>> GetAllAchievements() 
        {
            var result = await _achievementService.GetAllAchievements();

            if (result == null) return NotFound();
            
            return Ok(_mapper.Map<List<AchievementApiModel>>(result));
        }
    }
}
