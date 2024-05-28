using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class GroupEntity
    {
        [Key]
        public long GroupId { get; set; }

        [MaxLength(50)]
        public string GroupName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string GroupDescription { get; set; } = string.Empty;

        public string GroupIconUrl { get; set; } = string.Empty;

        public long ExerciseTypeId { get; set; }

        [ForeignKey(nameof(ExerciseTypeId))]
        public ExerciseTypeEntity ExerciseTypeEntity { get; set; }

        public List<UserGroupBridgeEntity> UserGroupBridgeEntities { get; set; } = [];
    }
}