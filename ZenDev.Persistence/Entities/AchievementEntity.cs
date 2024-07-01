using System.ComponentModel.DataAnnotations;

namespace ZenDev.Persistence.Entities
{
    public class AchievementEntity
    {
        [Key]
        public long AchievementId { get; set; }

        [MaxLength(50)]
        public string AchievementName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string AchievementDescription { get; set; } = string.Empty;

        public string AchievementIcon { get; set; } = string.Empty;
    }
}