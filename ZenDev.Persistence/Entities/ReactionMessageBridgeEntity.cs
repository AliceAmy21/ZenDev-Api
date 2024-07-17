

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ZenDev.Persistence.Entities
{
    public class ReactionMessageBridgeEntity
    {
        [Key]
        public long ReactionMessageBridgeId {get;set;}
        public long UserId {get;set;}
        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity {get;set;}
        public long MessageId {get;set;}
        [ForeignKey(nameof(MessageId))]
        public MessageEntity MessageEntity {get;set;}
        public long ReactionIconId {get;set;}
        [ForeignKey(nameof(ReactionIconId))]
        public ReactionIconEntity ReactionIconEntity {get;set;}
    }
}