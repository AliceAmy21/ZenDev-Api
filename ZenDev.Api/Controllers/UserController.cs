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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetUserByEmail))]
        public async Task<ActionResult<UserApiModel>> GetUserByEmail(string email) 
        {
            var result = await _userService.GetUserByEmailAsync(email);

            if (result == null) return NotFound();
            
            return Ok(_mapper.Map<UserApiModel>(result));
        }

        [HttpGet(nameof(GetAllUsers))]
        public async Task<ActionResult<List<UserApiModel>>> GetAllUsers() 
        {
            var result = await _userService.GetAllUsers();

            if (result == null) return NotFound();
            
            return Ok(_mapper.Map<List<UserApiModel>>(result));
        }

        [HttpPost(nameof(CreateUser))]
        public async Task<ActionResult<ResultApiModel>> CreateUser(UserApiModel user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            var result = await _userService.CreateUserAsync(userEntity);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }

        [HttpPost(nameof(AddRefreshToken))]
        public async Task<ActionResult<ResultApiModel>> AddRefreshToken(string email, string refreshToken)
        {
            var result = await _userService.AddRefreshTokenAsync(email, refreshToken);

            return Ok(_mapper.Map<ResultApiModel>(result));
        }
    }
}