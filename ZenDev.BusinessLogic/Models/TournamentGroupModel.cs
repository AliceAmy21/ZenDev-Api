using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class TournamentGroupModel
    {
        public string TGroupName {get;set;} = string.Empty;
        public string TGroupDescription {get;set;} =string.Empty;
        public string TGroupIconUrl {get;set;} = string.Empty;
        public long MemberCount {get;set;}
        public string ExerciseName {get;set;} = string.Empty; 
        public List<UserEntity> UserEntities {get;set;} = [];
    }
}