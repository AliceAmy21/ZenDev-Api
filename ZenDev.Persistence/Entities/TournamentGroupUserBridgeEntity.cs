using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Persistence.Entities
{
    public class TournamentGroupUserBridgeEntity
    {
        [Key]
        public long TournamentGroupUserId {get;set;}
        public long TGroupId {get;set;}
        [ForeignKey(nameof(TGroupId))]
        public TournamentGroupEntity TournamentGroupEntity {get;set;}
        public long UserId {get;set;}
        [ForeignKey(nameof(UserId))]
        public UserEntity UserEntity {get;set;}
    }
}