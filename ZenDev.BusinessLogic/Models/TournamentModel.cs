using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.BusinessLogic.Models
{
    public class TournamentModel
    {
        public long TournamentId {get;set;}
        public string TournamentName {get;set;} = string.Empty;
        public string TournamentDescription {get;set;} = string.Empty;
        public string ExerciseName {get;set;} = string.Empty;
        public DateTimeOffset StartDate {get;set;}
        public DateTimeOffset EndDate {get;set;}
        public List<TournamentGroupModel> tournamentGroupModels {get;set;} = [];
    }
}