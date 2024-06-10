using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Services
{
    public class GroupInvitationService : IGroupInvitationService
    {
        private readonly ZenDevDbContext _dbContext;
        private readonly ILogger<GroupService> _logger;

        public GroupInvitationService(
            ZenDevDbContext dbContext,
            ILogger<GroupService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<GroupInvitationEntity?>> getGroupInvitationsByUserIdAsync(long userId)
        {
            var result = await _dbContext.GroupInvitations
                .Where(invitation => invitation.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<GroupInvitationEntity> CreateGroupInvitationAsync(GroupInvitationEntity groupInvitation)
        {
            try
            {
                await _dbContext.AddAsync(groupInvitation);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create group invitation.");
                return new GroupInvitationEntity();
            }

            _logger.LogInformation("Group invitation created successfully");
            GroupInvitationEntity newInvitation = GetGroupInvitationById(groupInvitation.GroupInvitationId);
        
            return newInvitation;
        }

        public GroupInvitationEntity GetGroupInvitationById(long id)
        {
            var invitation = _dbContext.GroupInvitations
                .FirstOrDefault(invite => invite.GroupInvitationId == id);

            return invitation;
        }

        public async Task<List<UserInviteModel>> getNonGroupMembers(long groupId)
        {
            var groupMembers = await _dbContext.UserGroupBridge
                .Where(userGroup => userGroup.GroupId == groupId)
                .ToListAsync();

            List<UserEntity> users = GetAllUsers();

            List<UserInviteModel> nonGroupMembers = [];

            foreach (var member in groupMembers)
            {
                UserEntity user = users.Find(user => user.UserId == member.UserId);

                if (user != null)
                {
                    users.Remove(user);
                }
                
            }

            foreach (var user in users)
            {
                UserInviteModel userInviteModel = new()
                {
                    UserId = user.UserId,
                    UserName = user.UserName
                };

                nonGroupMembers.Add(userInviteModel);
            }

            return nonGroupMembers;
        }

        public UserEntity getUserById(long userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public List<UserEntity> GetAllUsers()
        {
            var result = _dbContext.Users.ToList();
            return result;
        }

    }
}