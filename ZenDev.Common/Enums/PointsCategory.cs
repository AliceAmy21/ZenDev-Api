using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenDev.Common.Enums
{
    /* Different point categories based on workout duration and average heart rate compared to the maximum heart rate.
     * Example: If John completes a workout of 30 - 59 minutes at 60% - 69% of his max heart rate,
     * he would earn 100 points for that workout.
     */

    public enum PointsCategory
    {
        Minutes30PlusWorkout = 100,
        Minutes30To59HeartRate60To69Percent = 100,
        Minutes60To89HeartRate60To69Percent = 200,
        Minutes90To119HeartRate60To69Percent = 300,
        Minutes120To179HeartRate60To69Percent = 450,
        Minutes180PlusHeartRate60PlusPercent = 600,
        Minutes15To29HeartRate70PlusPercent = 100,
        Minutes30To59HeartRate70To79Percent = 200,
        Minutes60To89HeartRate70PlusPercent = 300,
        Minutes90To119HeartRate70PlusPercent = 450,
        Minutes120PlusHeartRate70PlusPercent = 600,
        Minutes30PlusHeartRate80PlusPercent = 300
    }
}
