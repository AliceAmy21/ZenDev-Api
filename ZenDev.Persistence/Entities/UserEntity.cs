using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZenDev.Persistence.Entities
{
    public class UserEntity
    {
        [Key]
        public long UserId { get; set; }

        [MaxLength(50)]
        public string UserEmail { get; set; } = string.Empty;

        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        public string StravaRefreshToken { get; set; } = string.Empty;
        
        public long Streak { get; set; }

        public string AvatarIconUrl { get; set; } = string.Empty;

        public DateTimeOffset LastActive { get; set; }

        public DateTimeOffset? LastSynced { get; set; }

        [DefaultValue(0)]
        public long TotalPoints { get; set; }

        public DateTimeOffset ActiveWeek { get; set; }

        public long WeekPoints { get; set; }

        public List<UserGroupBridgeEntity> UserGroupBridgeEntities { get; set; } = [];

        public List<UserChallengeBridgeEntity> UserChallengeBridgeEntities {get;set;} = [];

        public List<TournamentGroupUserBridgeEntity> TournamentGroupUserBridgeEntities {get;set;} = [];
    }
}