using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Persistence.Entities
{
    public class TournamentGroupBridgeEntity
    {
        [Key]
        public long TournamentGroupId {get;set;}
        public long TournamentId {get;set;}
        [ForeignKey(nameof(TournamentId))]
        public TournamentEntity TournamentEntity {get;set;}
        public long TGroupId {get;set;}
        [ForeignKey(nameof(TGroupId))]
        public TournamentGroupEntity TournamentGroupEntity {get;set;}
        public long Points {get;set;}
    }
}