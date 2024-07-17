using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;

namespace ZenDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TournamentController(
        ITournamentService tournamentService,
        IMapper mapper
    ):ControllerBase
    {
        private readonly ITournamentService _tournamentService = tournamentService;
        private readonly IMapper _mapper = mapper;

        [HttpGet(nameof(GetUsersForTournamentGroup))]
        public async Task<ActionResult<List<UserInviteApiModel>>> GetUsersForTournamentGroup(long TGroupId){
            var tournament = await _tournamentService.GetUsersForTournamentGroup(TGroupId);
            return Ok(_mapper.Map<List<UserInviteApiModel>>(tournament));
        }

        [HttpPost(nameof(CreateTournament))]
        public async Task<ActionResult<TournamentApiModel>> CreateTournament(TournamentCreationApiModel tournamentCreation){
            var tournamentCreationModel = _mapper.Map<TournamentCreationModel>(tournamentCreation);
            var tournament = await _tournamentService.CreateTournament(tournamentCreationModel);
            return Ok(_mapper.Map<TournamentApiModel>(tournament));
        }

        [HttpGet(nameof(GetAllGroupsForTournaments))]
        public async Task<ActionResult<List<TournamentLeaderBoardApiModel>>> GetAllGroupsForTournaments(long TournamentId){
            var tournament = await _tournamentService.GetAllGroupsForTournaments(TournamentId);
            return Ok(_mapper.Map<List<TournamentLeaderBoardApiModel>>(tournament));
        }

        [HttpGet(nameof(GetTournament))]
        public async Task<ActionResult<TournamentApiModel>> GetTournament(long TournamentId){
            var tournaments =await _tournamentService.GetTournament(TournamentId);
            return Ok(_mapper.Map<TournamentApiModel>(tournaments));
        }

        [HttpGet(nameof(GetAllTournaments))]
        public async Task<ActionResult<List<TournamentListApiModel>>> GetAllTournaments(){
            var tournaments = await _tournamentService.GetAllTournaments();
            return Ok(_mapper.Map<List<TournamentListApiModel>>(tournaments));
        }

        [HttpGet(nameof(GetAllGroups))]
        public async Task<ActionResult<List<TournamentGroupApiModel>>> GetAllGroups(){
            var tournament = await _tournamentService.GetAllGroups();
            return Ok(_mapper.Map<List<TournamentGroupApiModel>>(tournament));
        }
    }
}