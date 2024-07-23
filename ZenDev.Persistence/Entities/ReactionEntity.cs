

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ZenDev.Persistence.Entities
{
    public class ReactionEntity
    {
        [Key]
        public long ReactionId {get;set;}
        public long UserId {get;set;}
        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity {get;set;}
        public string Reaction {get;set;}
        public MessageReactionBridgeEntity MessageReactionBridgeEntity {get;set;}
    }
}