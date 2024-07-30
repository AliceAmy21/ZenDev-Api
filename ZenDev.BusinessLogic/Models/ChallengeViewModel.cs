using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenDev.Common.Helpers;
using ZenDev.Persistence.Constants;
using ZenDev.Persistence.Entities;

namespace ZenDev.BusinessLogic.Models
{
    public class ChallengeViewModel
    {
        public long ChallengeId {get;set;}
        public string ChallengeName {get;set;}
        public string ChallengeDescription {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountCompleted {get;set;} = 0;
        public Measurement Measurement {get;set;}
        public long AmountToComplete {get;set;}
        public long ExerciseId {get;set;}
        public ExerciseEntity ExerciseEntity {get;set;}
        public long GroupId {get;set;}
        public GroupEntity GroupEntity {get;set;}
        public long UserId {get;set;}
        public long Admin {get;set;}
    }
}