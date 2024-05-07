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

        public long Streak { get; set; }

        public string AvatarIconUrl { get; set; } = string.Empty;

        public DateTimeOffset LastActive { get; set; }
    }
}