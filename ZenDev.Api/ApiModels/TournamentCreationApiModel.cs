using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Api.ApiModels
{
    public class TournamentCreationApiModel
    {
        public string TournamentName {get;set;} = string.Empty;
        public string TournamentDescription {get;set;} = string.Empty;
        public ExerciseApiModel exerciseApiModel {get;set;}
        public DateTimeOffset StartDate {get;set;}
        public DateTimeOffset EndDate {get;set;}
        public List<TournamentGroupApiModel> tournamentGroupApiModels {get;set;} = [];
    }
}