using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class TournamentCreationModel
    {
        public string TournamentName {get;set;} = string.Empty;
        public string TournamentDescription {get;set;} = string.Empty;
        public ExerciseEntity ExerciseEntity {get;set;}
        public DateTimeOffset StartDate {get;set;}
        public DateTimeOffset EndDate {get;set;}
        public List<TournamentGroupModel> TournamentGroupModels {get;set;} = [];
    }
}