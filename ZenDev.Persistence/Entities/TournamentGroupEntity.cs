using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Persistence.Entities
{
    public class TournamentGroupEntity
    {
        [Key]
        public long TGroupId {get;set;}
        public string TGroupName {get;set;}  = string.Empty;
        public string TGroupDescription {get;set;}  = string.Empty;
        public string TGroupIconUrl {get;set;} = string.Empty;
        public long MemberCount {get;set;}
        public string ExerciseName {get;set;} = string.Empty; 
        public List<TournamentGroupUserBridgeEntity> TournamentGroupUserBridgeEntities {get;set;} = [];
        public List<TournamentGroupBridgeEntity> TournamentGroupBridgeEntities {get;set;} = [];
    }
}