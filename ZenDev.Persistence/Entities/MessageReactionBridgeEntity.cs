using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Persistence.Entities
{
    public class MessageReactionBridgeEntity
    {
        [Key]
        public long MessageReactionId {get;set;}
        public long ReactionId {get;set;}
        [ForeignKey(nameof(ReactionId))]
        public ReactionEntity ReactionEntity {get;set;}
        public long MessageId {get;set;}
        [ForeignKey(nameof(MessageId))]
        public MessageEntity MessageEntity {get;set;}
    }
}