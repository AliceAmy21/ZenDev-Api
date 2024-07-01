using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class UserAchievementBridgeEntity
    {
        [Key]
        public long UserAchievementId { get; set; }

        public long UserId { get; set; }

        public long AchievementId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity { get; set; }

        [ForeignKey(nameof(AchievementId))]
        public AchievementEntity AchievementEntity { get; set; }
    }
}