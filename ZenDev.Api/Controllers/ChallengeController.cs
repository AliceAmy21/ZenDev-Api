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
    public class ChallengeController(
        IChallengeService challengeService,
        IMapper mapper
        ) : ControllerBase
    {
        private readonly IChallengeService _challengeService = challengeService;
        private readonly IMapper _mapper = mapper;

        [HttpPut(nameof(AddUserToChallenge))]
        public async Task<ActionResult<ChallengeApiModel>> AddUserToChallenge(long challengeId, long userId){
                var challenge = await _challengeService.AddUserToChallengeAsync(challengeId,userId);
                return Ok(_mapper.Map<ChallengeApiModel>(challenge));
        }

        [HttpPost(nameof(CreateChallenge))]
        public async Task<ActionResult<ChallengeApiModel>> CreateChallenge(ChallengeCreationApiModel challenge){
            long UserId = challenge.UserId;
            var challengeModel = _mapper.Map<ChallengeCreationModel>(challenge);
            var challengeNew = await _challengeService.CreateChallengeAsync(challengeModel,UserId);
            return Ok(_mapper.Map<ChallengeApiModel>(challengeNew));

        }

        [HttpGet(nameof(GetChallengeById))]
        public async Task<ActionResult<ChallengeApiModel>> GetChallengeById(long ChallengeId){
            var challenge = await _challengeService.GetChallengeByIdAsync(ChallengeId);
            return Ok(_mapper.Map<ChallengeApiModel>(challenge));
        }

        [HttpGet(nameof(GetChallengesForGroup))]
        public ActionResult<List<List<ChallengeListApiModel>>> GetChallengesForGroup(long groupId,long userId){
            var listChallenges = _challengeService.GetChallengesForGroupAsync(groupId,userId);
            return Ok(_mapper.Map<List<List<ChallengeListApiModel>>>(listChallenges));
        }

        [HttpGet(nameof(GetChallengesForUser))]
        public ActionResult<List<ChallengeListApiModel>> GetChallengesForUser(long userId){
            var listChallenges = _challengeService.GetChallengesForUserAsync(userId);
            return Ok(_mapper.Map<List<ChallengeListApiModel>>(listChallenges));
        }

        [HttpGet(nameof(GetUsersForChallenge))]
        public ActionResult<List<UserChallengeBridgeApiModel>> GetUsersForChallenge(long challengeId){
            var listUsers = _challengeService.GetUsersForChallengeAsync(challengeId);
            return Ok(_mapper.Map<List<UserChallengeBridgeApiModel>>(listUsers));
        }

        [HttpGet(nameof(GetUsersToInviteChallenge))]
        public ActionResult<List<UserApiModel>> GetUsersToInviteChallenge(long challengeId, long userId){
            return Ok(new List<UserApiModel>());
        }

        [HttpDelete(nameof(RemoveUserFromChallenge))]
        public async Task<ActionResult<ChallengeApiModel>> RemoveUserFromChallenge(long challengeId, long userId){
            var challenge = await _challengeService.RemoveUserFromChallengeAsync(challengeId,userId);
            return Ok(_mapper.Map<ChallengeApiModel>(challenge));
        }

        [HttpPut(nameof(UpdateChallenge))]
        public async Task<ActionResult<ChallengeApiModel>> UpdateChallenge(ChallengeUpdateApiModel challenge){
            var challengeModel = _mapper.Map<ChallengeUpdateModel>(challenge);
            var challengeNew = await _challengeService.UpdateChallengeAsync(challengeModel);
            return Ok(_mapper.Map<ChallengeEntity>(challengeNew));
        }
    }
}