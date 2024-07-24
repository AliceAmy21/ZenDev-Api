using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;

namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderBoardController(
        ILeaderBoardService leaderBoardServiceService,
        IMapper mapper
        ) : ControllerBase
    {
    private readonly ILeaderBoardService _leaderBoardService = leaderBoardServiceService;
    private readonly IMapper _mapper = mapper;

    [HttpGet(nameof(GetGroupLeaderBoard))]
    public async Task<ActionResult<List<LeaderBoardListApiModel>>> GetGroupLeaderBoard(long groupId){
        List<LeaderBoardListModel> leaderBoards = _leaderBoardService.GetAllLeaderBoardDataForGroups(groupId);
        return Ok(_mapper.Map<List<LeaderBoardListApiModel>>(leaderBoards));
    }

    [HttpGet(nameof(GetChallengeLeaderBoard))]
    public async Task<ActionResult<List<LeaderBoardListApiModel>>> GetChallengeLeaderBoard(long challengeId){
        List<LeaderBoardListModel> leaderBoards = _leaderBoardService.GetAllLeaderBoardDataForChallenge(challengeId);
        return Ok(_mapper.Map<List<LeaderBoardListApiModel>>(leaderBoards));
    }
    }
}