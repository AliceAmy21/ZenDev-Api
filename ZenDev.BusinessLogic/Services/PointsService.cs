using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenDev.BusinessLogic.Services.Interfaces;
using ZenDev.Common.Enums;
using ZenDev.Common.Models;

namespace ZenDev.BusinessLogic.Services
{
    public class PointsService: IPointsService
    {
        public int CalculatePoints(List<ActivityPointsApiModel> activities)
        {
            int totalPoints = 0;


            foreach (var activity in activities)
            {
                int movingTimeInMinutes = GetMinutes(activity.MovingTime);
                totalPoints += GetPointsForCategory(movingTimeInMinutes, activity.AverageHeartrate, activity.MaxHeartrate);
            }
            return totalPoints;
        }

        private static int GetMinutes(int movingTime)
        {
            return (movingTime / 60);
        }

        private static int GetPointsForCategory(int time, double? avgHeartRate, double? maxHeartRate)
        {
            if (time >= 30 && !avgHeartRate.HasValue)
            {
                return (int)PointsCategory.Minutes30PlusWorkout;
            }
            else if (avgHeartRate.HasValue && maxHeartRate.HasValue)
            {
                double percentage = avgHeartRate.Value / maxHeartRate.Value;

                if (time >= 30 && time < 60 && percentage >= 0.60 && percentage <= 0.69)
                {
                    return (int)PointsCategory.Minutes30To59HeartRate60To69Percent;
                }
                else if (time >= 60 && time < 90 && percentage >= 0.60 && percentage <= 0.69)
                {
                    return (int)PointsCategory.Minutes60To89HeartRate60To69Percent;
                }
                else if (time >= 90 && time < 120 && percentage >= 0.60 && percentage <= 0.69)
                {
                    return (int)PointsCategory.Minutes90To119HeartRate60To69Percent;
                }
                else if (time >= 120 && time < 180 && percentage >= 0.60 && percentage <= 0.69)
                {
                    return (int)PointsCategory.Minutes120To179HeartRate60To69Percent;
                }
                else if (time >= 180 && percentage >= 0.60)
                {
                    return (int)PointsCategory.Minutes180PlusHeartRate60PlusPercent;
                }
                else if (time >= 90 && time < 120 && percentage >= 0.70)
                {
                    return (int)PointsCategory.Minutes90To119HeartRate70PlusPercent;
                }
                else if (time >= 120 && percentage >= 0.70)
                {
                    return (int)PointsCategory.Minutes120PlusHeartRate70PlusPercent;
                }
                else if (time >= 15 && time < 30 && percentage >= 0.70)
                {
                    return (int)PointsCategory.Minutes15To29HeartRate70PlusPercent;
                }
                else if (time >= 30 && time < 60 && percentage >= 0.70 && percentage <= 0.79)
                {
                    return (int)PointsCategory.Minutes30To59HeartRate70To79Percent;
                }
                else if (time >= 60 && time < 90 && percentage >= 0.70)
                {
                    return (int)PointsCategory.Minutes60To89HeartRate70PlusPercent;
                }
                else if (time >= 90 && time < 120 && percentage >= 0.70)
                {
                    return (int)PointsCategory.Minutes90To119HeartRate70PlusPercent;
                }
                else if (time >= 120 && percentage >= 0.70)
                {
                    return (int)PointsCategory.Minutes120PlusHeartRate70PlusPercent;
                }
                else if (time >= 30 && percentage >= 0.80)
                {
                    return (int)PointsCategory.Minutes30PlusHeartRate80PlusPercent;
                }
            }

            return 0;
        }
    }
}
