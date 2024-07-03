using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence.Entities;

namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class MindfulnessController : ControllerBase
    {
        private readonly IMindfulnessService _mindfulnessService;
        private readonly IMapper _mapper;

        public MindfulnessController(
            IMindfulnessService mindfulnessService,
            IMapper mapper)
        {
            _mindfulnessService = mindfulnessService;
            _mapper = mapper;
        }

        [HttpPost(nameof(AddMindfulnessPoints))]
        public async Task<ActionResult<ResultApiModel>> AddMindfulnessPoints(MindfulnessApiModel mindfulnessApiModel)
        {
            var mindfulnessEntity = _mapper.Map<MindfulnessEntity>(mindfulnessApiModel);

            var result = await _mindfulnessService.AddMindfulnessPoints(mindfulnessEntity);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }

        [HttpGet(nameof(GetMindfulnessPoints))]
        public async Task<ActionResult<MindfulnessApiModel?>> GetMindfulnessPoints(long userId)
        {
            var result = await _mindfulnessService.GetMindfulnessPoints(userId);

            if (result == null) return Empty;

            return Ok(_mapper.Map<MindfulnessApiModel>(result));
        }
    }
}
