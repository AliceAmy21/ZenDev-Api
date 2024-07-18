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
            await _dbContext.SaveChangesAsync();
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
                await _dbContext.SaveChangesAsync();
                TournamentGroupBridgeEntity tournamentGroupBridgeEntity = new TournamentGroupBridgeEntity{
                    TGroupId = tournamentGroupEntity.TGroupId,
                    TournamentId = tournamentEntity.TournamentId,
                    Points = 0
                };
                await _dbContext.AddAsync(tournamentGroupBridgeEntity);
                await _dbContext.SaveChangesAsync();
                var users = group.UserEntities;
                foreach(var user in users){
                    TournamentGroupUserBridgeEntity tournamentGroupUserBridgeEntity = new TournamentGroupUserBridgeEntity{
                        UserId = user.UserId,
                        TGroupId = tournamentGroupEntity.TGroupId
                    };
                    await _dbContext.AddAsync(tournamentGroupUserBridgeEntity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            await _dbContext.SaveChangesAsync();
            return tournamentEntity;
        }

        public async Task<List<UserEntity>> GetAllUserForGroup(long groupId){
            var bridge = _dbContext.UserGroupBridge
            .Include(users=>users.UserEntity)
            .AsQueryable();
            return await bridge.Where(g => g.GroupId == groupId).Select(u=>u.UserEntity).ToListAsync();
        }

        public async Task<List<TournamentGroupModel>> GetAllGroups()
        {
            var groups = _dbContext.Groups
            .Include(exercise=>exercise.ExerciseTypeEntity)
            .AsQueryable();

            List<TournamentGroupModel> groupModels = [];
            foreach(var group in groups){
                TournamentGroupModel tournamentGroupModel = new TournamentGroupModel{
                    TGroupId = group.GroupId,
                    TGroupName = group.GroupName,
                    TGroupDescription= group.GroupDescription,
                    TGroupIconUrl= group.GroupIconUrl,
                    ExerciseName= group.ExerciseTypeEntity.ExerciseType,
                    MemberCount= group.MemberCount,
                };
                groupModels.Add(tournamentGroupModel);
            }
            foreach(var group in groupModels){
                group.UserEntities = await GetAllUserForGroup(group.TGroupId);
            }

            return groupModels;
        }

        public async Task<List<TournamentLeaderBoardModel>> GetAllGroupsForTournaments(long TournamentId)
        {
            var bridge = _dbContext.TournamentGroupBridge
            .Include(groups=>groups.TournamentGroupEntity)
            .AsQueryable();
            List<TournamentLeaderBoardModel> tournamentLeaderBoardModels = [];
            var groups = await bridge.Where(t=>t.TournamentId == TournamentId).ToListAsync();
            foreach(var group in groups){
                var leaderboardgroup = new TournamentLeaderBoardModel{
                    TGroupId = group.TGroupId,
                    TGroupDescription = group.TournamentGroupEntity.TGroupDescription,
                    TGroupIconUrl = group.TournamentGroupEntity.TGroupIconUrl,
                    TGroupName = group.TournamentGroupEntity.TGroupName,
                    MemberCount = group.TournamentGroupEntity.MemberCount,
                    Points = group.Points,
                    ExerciseName = group.TournamentGroupEntity.ExerciseName
                };
                tournamentLeaderBoardModels.Add(leaderboardgroup);
            }
            return tournamentLeaderBoardModels.OrderByDescending(p=>p.Points).ToList();
        }


        

        public async Task<List<TournamentGroupEntity>> GetAllGroupsForTournaments1(long TournamentId)
        {
            var bridge = _dbContext.TournamentGroupBridge
            .Include(groups=>groups.TournamentGroupEntity)
            .AsQueryable();
            return await bridge.Where(t=>t.TournamentId == TournamentId).OrderByDescending(points=>points.Points).Select(g=>g.TournamentGroupEntity).ToListAsync();
        }

        public async Task<List<TournamentEntity>> GetAllTournaments()
        {
            return await _dbContext.Tournaments.ToListAsync();
        }

        public async Task<TournamentModel> GetTournament(long TournamentId)
        {
            TournamentEntity tournamentEntity =  await _dbContext.Tournaments.FindAsync(TournamentId);
            List<TournamentGroupEntity> tournamentGroupEntities = await GetAllGroupsForTournaments1(tournamentEntity.TournamentId);
            List<TournamentGroupModel> tournamentGroupModels = [];
            foreach(TournamentGroupEntity groupEntity in tournamentGroupEntities){
                TournamentGroupModel tournamentGroupModel = new TournamentGroupModel{
                    TGroupId = groupEntity.TGroupId,
                    TGroupName= groupEntity.TGroupName,
                    TGroupDescription= groupEntity.TGroupDescription,
                    TGroupIconUrl= groupEntity.TGroupIconUrl,
                    MemberCount= groupEntity.MemberCount,
                    ExerciseName = groupEntity.ExerciseName,
                    UserEntities = await GetUsersForTournamentGroup(groupEntity.TGroupId)
                }; 
                tournamentGroupModels.Add(tournamentGroupModel);
            }
            TournamentModel tournamentModel = new TournamentModel{
                TournamentId = tournamentEntity.TournamentId,
                TournamentName = tournamentEntity.TournamentName,
                TournamentDescription = tournamentEntity.TournamentDescription,
                ExerciseName = tournamentEntity.ExerciseEntity.ExerciseName,
                StartDate = tournamentEntity.StartDate,
                EndDate = tournamentEntity.StartDate,
                tournamentGroupModels = tournamentGroupModels
            };
            return tournamentModel;
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