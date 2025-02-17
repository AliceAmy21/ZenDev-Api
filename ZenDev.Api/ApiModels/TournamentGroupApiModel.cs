using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Api.ApiModels
{
    public class TournamentGroupApiModel
    {
        public long TGroupId {get;set;}
        public string TGroupName {get;set;} = string.Empty;
        public string TGroupDescription {get;set;} =string.Empty;
        public string TGroupIconUrl {get;set;} = string.Empty;
        public long MemberCount {get;set;}
        public string ExerciseName {get;set;} = string.Empty; 
        public List<UserInviteApiModel> userInviteApiModels {get;set;} = [];
    }
}