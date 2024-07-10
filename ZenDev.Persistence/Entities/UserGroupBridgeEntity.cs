using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class UserGroupBridgeEntity
    {
        [Key]
        public long UserGroupId { get; set; }

        public bool GroupAdmin { get; set; }

        public long UserId { get; set; }

        public long GroupId { get; set; }

        public long Points{ get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity { get; set; }

        [ForeignKey(nameof(GroupId))]
        public GroupEntity GroupEntity { get; set; }
    }
}