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
    public class ExampleController : ControllerBase
    {
        private readonly IExampleService _exampleService;
        private readonly IMapper _mapper;

        public ExampleController(
            IExampleService exampleService,
            IMapper mapper)
        {
            _exampleService = exampleService;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<ExampleApiModel>> GetById(long id) // 
        {
            var result = await _exampleService.GetExampleByIdAsync(id);

            if (result == null) return NotFound();
            
            return Ok(_mapper.Map<ExampleApiModel>(result));
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<IEnumerable<ExampleApiModel>>> GetAll()
        {
            var user = User;
            var results = await _exampleService.GetAllExamplesAsync();

            return Ok(results.Select(result => _mapper.Map<ExampleApiModel>(result)));
        }

        [HttpPost(nameof(Create))]
        public async Task<ActionResult<ResultApiModel>> Create(ExampleApiModel example)
        {
            var entity = _mapper.Map<ExampleEntity>(example);

            var result = await _exampleService.CreateExampleAsync(entity);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }
    }
}
