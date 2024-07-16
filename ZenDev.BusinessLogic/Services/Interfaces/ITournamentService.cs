using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services.Interfaces
{
    public interface ITournamentService
    {
        public Task<List<UserEntity>> GetUsersForTournamentGroup(long TGroupId);
        public Task<TournamentEntity> CreateTournament(TournamentCreationModel tournamentCreation);
        public Task<List<TournamentLeaderBoardModel>> GetAllGroupsForTournaments(long TournamentId);
        public Task<TournamentModel> GetTournament(long TournamentId);
        public Task<List<TournamentEntity>> GetAllTournaments();
        public Task<List<TournamentGroupModel>> GetAllGroups();
    }
}