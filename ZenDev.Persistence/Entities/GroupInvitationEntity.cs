using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenDev.Persistence.Entities
{
    public class GroupInvitationEntity
    {
        [Key]
        public long GroupInvitationId { get; set; }

        public long GroupId { get; set; }

         public long UserId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public GroupEntity GroupEntity { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity { get; set; }
    }
}