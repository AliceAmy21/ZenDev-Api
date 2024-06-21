using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenDev.Api.ApiModels
{
    public class ChallengeListApiModel
    {
        public long ChallengeId {get;set;}
        public DateTimeOffset ChallengeStartDate {get;set;}   
        public DateTimeOffset ChallengeEndDate {get;set;}
        public long AmountToComplete {get;set;}
        public required ExerciseApiModel ExerciseApiModel {get;set;}
        public required GroupApiModel GroupApiModel {get;set;}
        public long Admin {get;set;}
    }
}