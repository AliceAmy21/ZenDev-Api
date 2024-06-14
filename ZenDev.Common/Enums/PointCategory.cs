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

    public enum PointCategory
    {
        Minutes30PlusWorkout,
        Minutes30To59HeartRate60To69Percent,
        Minutes60To89HeartRate60To69Percent,
        Minutes90To119HeartRate60To69Percent,
        Minutes120To179HeartRate60To69Percent,
        Minutes180PlusHeartRate60PlusPercent,
        Minutes15To29HeartRate70PlusPercent,
        Minutes30To59HeartRate70To79Percent,
        Minutes60To89HeartRate70PlusPercent,
        Minutes90To119HeartRate70PlusPercent,
        Minutes120PlusHeartRate70PlusPercent,
        Minutes30PlusHeartRate80PlusPercent
    }
}
