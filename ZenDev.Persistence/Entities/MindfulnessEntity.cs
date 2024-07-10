using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class MindfulnessEntity
    {
        [Key]
        public long MindfulnessId { get; set; }

        public long TotalPoints { get; set; }

        public long TodaysPoints { get; set; }

        public Double TotalMinutes { get; set; }

        public Double TodaysMinutes { get; set; }

        public DateTimeOffset? LastUpdate { get; set; }

        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity { get; set; }

    }
}
