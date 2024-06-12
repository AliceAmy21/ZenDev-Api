using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.BusinessLogic.Models
{
    public class ChallengeUpdateModel
    {
        public long ChallengeId {get;set;} 
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
    }
}