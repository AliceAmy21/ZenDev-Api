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
            return Ok(_mapper.Map<List<UserInviteApiModel>>(_tournamentService.GetUsersForTournamentGroup(TGroupId)));
        }

        [HttpPost(nameof(CreateTournament))]
        public async Task<ActionResult<TournamentApiModel>> CreateTournament(TournamentCreationApiModel tournamentCreation){
            var tournamentCreationModel = _mapper.Map<TournamentCreationModel>(tournamentCreation);
            return Ok(_mapper.Map<TournamentApiModel>(_tournamentService.CreateTournament(tournamentCreationModel)));
        }

        [HttpGet(nameof(GetAllGroupsForTournaments))]
        public async Task<ActionResult<List<TournamentGroupApiModel>>> GetAllGroupsForTournaments(long TournamentId){
            return Ok(_mapper.Map<List<GroupApiModel>>(_tournamentService.GetAllGroupsForTournaments(TournamentId)));
        }

        [HttpGet(nameof(GetTournament))]
        public async Task<ActionResult<TournamentApiModel>> GetTournament(long TournamentId){
            return Ok(_mapper.Map<TournamentApiModel>(_tournamentService.GetTournament(TournamentId)));
        }

        [HttpGet(nameof(GetAllTournaments))]
        public async Task<ActionResult<List<TournamentListApiModel>>> GetAllTournaments(){
            return Ok(_mapper.Map<List<TournamentListApiModel>>(_tournamentService.GetAllTournaments()));
        }

        [HttpGet(nameof(GetAllGroups))]
        public async Task<ActionResult<List<TournamentGroupApiModel>>> GetAllGroups(){
            return Ok(_mapper.Map<List<TournamentGroupApiModel>>(_tournamentService.GetAllGroups()));
        }
    }
}