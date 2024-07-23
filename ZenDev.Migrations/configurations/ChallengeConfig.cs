using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenDev.Persistence.Constants;

namespace ZenDev.Migrations.configurations
{
    public class ChallengeConfig
    {
        public string ChallengeName {get;set;} = string.Empty;
        public string ChallengeDescription {get;set;} = string.Empty;
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public Measurement Measurement {get;set;}
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
        public long GroupId {get;set;}
        public long Admin {get;set;}
    }
}