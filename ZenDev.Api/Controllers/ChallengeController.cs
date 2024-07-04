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
        public async Task<ActionResult<int>> AddUserToChallenge(long challengeId, long userId){
                await _challengeService.AddUserToChallengeAsync(challengeId,userId);
                return Ok(userId);
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
        public ActionResult<List<List<ChallengeListApiModel>>> GetChallengesForUser(long userId){
            var listChallenges = _challengeService.GetChallengesForUserAsync(userId);
            return Ok(_mapper.Map<List<List<ChallengeListApiModel>>>(listChallenges));
        }

        [HttpGet(nameof(GetUsersForChallenge))]
        public ActionResult<List<UserInviteApiModel>> GetUsersForChallenge(long challengeId){
            var listUsers = _challengeService.GetUsersForChallengeAsync(challengeId);
            return Ok(_mapper.Map<List<UserInviteApiModel>>(listUsers));
        }

        [HttpGet(nameof(GetUsersToInviteChallenge))]
        public ActionResult<List<UserInviteApiModel>> GetUsersToInviteChallenge(long challengeId, long groupId){
            var listUsers = _challengeService.GetUsersToInviteChallengeAsync(challengeId,groupId);
            return Ok(_mapper.Map<List<UserInviteApiModel>>(listUsers));
        }

        [HttpDelete(nameof(RemoveUserFromChallenge))]
        public async Task<ActionResult<int>> RemoveUserFromChallenge(long challengeId, long userId){
            await _challengeService.RemoveUserFromChallengeAsync(challengeId,userId);
            return Ok(userId);
        }

        [HttpPut(nameof(UpdateChallenge))]
        public async Task<ActionResult<ChallengeApiModel>> UpdateChallenge(ChallengeUpdateApiModel challenge){
            var challengeModel = _mapper.Map<ChallengeUpdateModel>(challenge);
            var challengeNew = await _challengeService.UpdateChallengeAsync(challengeModel);
            return Ok(_mapper.Map<ChallengeApiModel>(challengeNew));
        }

        [HttpGet(nameof(GetAllExercises))]
        public async Task<ActionResult<List<ExerciseApiModel>>> GetAllExercises() 
        {
            var result = await _challengeService.GetAllExercisesAsync();
            if (result == null) return NotFound();
            return Ok(_mapper.Map<List<ExerciseApiModel>>(result));
        }

        [HttpDelete(nameof(DeleteChallenge))]
        public async Task<long> DeleteChallenge(long challengeId){
            return _challengeService.DeleteChallengeAsync(challengeId).Result;
        }

    }
}