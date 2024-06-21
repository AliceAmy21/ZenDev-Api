using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenDev.Common.Helpers;
using ZenDev.Persistence.Constants;

namespace ZenDev.Api.ApiModels
{
    public class ChallengeUpdateApiModel
    {
        public long ChallengeId {get;set;}   
        public string ChallengeDescription {get;set;}
        public DateTimeOffset ChallengeEndDate {get;set;}
        public Measurement Measurement {get;set;}
        public long AmountCompleted {get;set;} = 0;
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
        public long Admin {get;set;}
    }
}