using Microsoft.EntityFrameworkCore;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class TournamentService:ITournamentService
    {
        private readonly ZenDevDbContext _dbContext;
        public TournamentService(ZenDevDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<TournamentEntity> CreateTournament(TournamentCreationModel tournamentCreation)
        {
            TournamentEntity tournamentEntity = new TournamentEntity{
                TournamentName=tournamentCreation.TournamentName,
                TournamentDescription=tournamentCreation.TournamentDescription,
                StartDate=tournamentCreation.StartDate,
                EndDate=tournamentCreation.EndDate,
                ExerciseId=tournamentCreation.ExerciseEntity.ExerciseId
            };
            await _dbContext.AddAsync(tournamentEntity);
            var groups = tournamentCreation.TournamentGroupModels;
            foreach(var group in groups){
                TournamentGroupEntity tournamentGroupEntity = new TournamentGroupEntity{
                    TGroupName = group.TGroupName,
                    TGroupDescription = group.TGroupDescription,
                    TGroupIconUrl = group.TGroupIconUrl,
                    MemberCount = group.MemberCount,
                    ExerciseName = tournamentCreation.ExerciseEntity.ExerciseName,
                };
                await _dbContext.AddAsync(tournamentGroupEntity);
                TournamentGroupBridgeEntity tournamentGroupBridgeEntity = new TournamentGroupBridgeEntity{
                    TGroupId = tournamentGroupEntity.TGroupId,
                    TournamentId = tournamentEntity.TournamentId,
                    Points = 0
                };
                await _dbContext.AddAsync(tournamentGroupBridgeEntity);
                var users = group.UserEntities;
                foreach(var user in users){
                    TournamentGroupUserBridgeEntity tournamentGroupUserBridgeEntity = new TournamentGroupUserBridgeEntity{
                        UserId = user.UserId,
                        TGroupId = tournamentGroupEntity.TGroupId
                    };
                    await _dbContext.AddAsync(tournamentGroupUserBridgeEntity);
                }
            }
            await _dbContext.SaveChangesAsync();
            return tournamentEntity;
        }

        public async Task<List<TournamentGroupModel>> GetAllGroups()
        {
            var groups = _dbContext.Groups
            .Include(exercise=>exercise.ExerciseTypeEntity)
            .AsQueryable();
            var bridge = _dbContext.UserGroupBridge
            .Include(users=>users.UserEntity)
            .AsQueryable();

            List<TournamentGroupModel> groupModels = [];
            foreach(var group in groups){
                TournamentGroupModel tournamentGroupModel = new TournamentGroupModel{
                    TGroupName = group.GroupName,
                    TGroupDescription= group.GroupDescription,
                    TGroupIconUrl= group.GroupIconUrl,
                    ExerciseName= group.ExerciseTypeEntity.ExerciseType,
                    MemberCount= group.MemberCount,
                    UserEntities= bridge.Where(g => g.GroupId == group.GroupId).Select(u=>u.UserEntity).ToList()
                };
                groupModels.Add(tournamentGroupModel);
            }

            return groupModels;
        }

        public async Task<List<TournamentGroupEntity>> GetAllGroupsForTournaments(long TournamentId)
        {
            var bridge = _dbContext.TournamentGroupBridge
            .Include(groups=>groups.TournamentGroupEntity)
            .AsQueryable();
            return await bridge.Where(t=>t.TournamentId == TournamentId).Select(g=>g.TournamentGroupEntity).ToListAsync();
        }

        public async Task<List<TournamentEntity>> GetAllTournaments()
        {
            return await _dbContext.Tournaments.ToListAsync();
        }

        public async Task<TournamentEntity> GetTournament(long TournamentId)
        {
            return await _dbContext.Tournaments.FindAsync(TournamentId);
        }

        public async Task<List<UserEntity>> GetUsersForTournamentGroup(long TGroupId)
        {
            var bridge = _dbContext.TournamentGroupUserBridge
            .Include(user=>user.UserEntity)
            .AsQueryable();
            return await bridge.Where(g=>g.TGroupId == TGroupId).Select(u=>u.UserEntity).ToListAsync();
        }
    }
}