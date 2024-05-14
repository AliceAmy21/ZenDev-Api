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
    [Authorize]
    public class PersonalGoalController : ControllerBase
    {
        private readonly IPersonalGoalService _personalGoalService;
        private readonly IMapper _mapper;

        public PersonalGoalController(
            IPersonalGoalService personalGoalService,
            IMapper mapper)
        {
            _personalGoalService = personalGoalService;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetGoalById))]
        public async Task<ActionResult<PersonalGoalApiModel>> GetGoalById(long id)
        {
            var result = await _personalGoalService.GetGoalByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(_mapper.Map<PersonalGoalApiModel>(result));
        }

        [HttpGet(nameof(GetAllGoals))]
        public async Task<ActionResult<List<PersonalGoalApiModel>>> GetAllGoals(long userId)
        {
            var result = await _personalGoalService.GetAllGoalsAsync(userId);

            if (result == null) return NotFound();

            return Ok(_mapper.Map<List<PersonalGoalApiModel>>(result));
        }

        [HttpPost(nameof(CreateGoal))]
        public async Task<ActionResult<ResultApiModel>> CreateGoal(PersonalGoalApiModel goal)
        {
            var personalGoalEntity = _mapper.Map<PersonalGoalEntity>(goal);

            var result = await _personalGoalService.CreateGoalAsync(personalGoalEntity);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }

        [HttpDelete(nameof(DeleteGoal))]
        public async Task<ActionResult<ResultApiModel>> DeleteGoal(long id)
        {
            var result = await _personalGoalService.DeleteGoalAsync(id);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }

        [HttpPost(nameof(UpdateGoal))]
        public async Task<ActionResult<ResultApiModel>> UpdateGoal(PersonalGoalApiModel goal)
        {
            var personalGoalEntity = _mapper.Map<PersonalGoalEntity>(goal);

            var result = await _personalGoalService.UpdateGoalAsync(personalGoalEntity);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }

    }
}