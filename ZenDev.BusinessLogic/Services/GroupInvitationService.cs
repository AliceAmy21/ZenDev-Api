using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    }
}