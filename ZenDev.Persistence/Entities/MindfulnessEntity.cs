using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenDev.Persistence.Entities
{
    public class MindfulnessEntity
    {
        [Key]
        public long MindfulnessId { get; set; }

        public long TotalPoints { get; set; }

        public long TotalMinutes { get; set; }

        public DateTimeOffset? LastUpdate { get; set; }

        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity { get; set; }

    }
}
