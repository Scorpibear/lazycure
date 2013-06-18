using System;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    /// <summary>
    /// Splitting activity at midnight to 2 different timelogs
    /// </summary>
    public class MidnightSwitcher:IMidnightCorrector
    {
        public void PerformMidnightCorrection(IActivity currentActivity, ITimeLogsManager timeLogsManager)
        {
            DateTime endTime = currentActivity.End;
            DateTime midnightTime = endTime.Date;
            DateTime startTime = currentActivity.Start;
            if (startTime < midnightTime)
            {
                TimeSpan oldDayActivityDuration = midnightTime - startTime;
                TimeSpan newDayActivityDuration = endTime - midnightTime;
                currentActivity.Duration = oldDayActivityDuration;
                if (timeLogsManager != null)
                {
                    ITimeLog timeLog = timeLogsManager.ActiveTimeLog;
                    if (timeLog != null)
                        timeLog.AddActivity(currentActivity);
                    timeLogsManager.ActivateTimeLog(midnightTime);
                }
                currentActivity.Start = midnightTime;
                currentActivity.Duration = newDayActivityDuration;
            }
        }
    }
}
