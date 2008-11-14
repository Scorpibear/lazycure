using System;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    /// <summary>
    /// Manages time
    /// </summary>
    public class TimeManager : ITimeManager
    {
        private const string FIRST_ACTIVITY = "Activity1";
        private RunningActivity currentActivity;
        private TimeSpan maxDuration = TimeSpan.Parse("1:00");
        private RunningActivity previousActivity;
        private ITimeLog timeLog;
        private ITimeLogsManager timeLogsManager;

        #region Properties

        public IActivity CurrentActivity
        {
            get { return currentActivity; }
        }

        public TimeSpan MaxDuration
        {
            get { return maxDuration; }
            set { maxDuration = value; }
        }

        public bool CurrentActivityIsLastingTooLong
        {
            get { return currentActivity.Duration >= maxDuration; }
        }

        public IActivity PreviousActivity
        {
            get { return previousActivity; }
        }

        public ITimeLog TimeLog
        {
            get { return timeLog; }
            set { timeLog = value; }
        }

        public ITimeLogsManager TimeLogsManager
        {
            get { return timeLogsManager; }
            set { timeLogsManager = value; }
        }

        public ITimeSystem TimeSystem
        {
            get { return currentActivity.timeSystem; }
        }

        #endregion Properties

        public TimeManager(ITimeSystem timeSystem)
        {
            currentActivity = new RunningActivity(FIRST_ACTIVITY, timeSystem);
        }

        public TimeManager(ITimeSystem timeSystem, ITimeLogsManager timeLogsManager, ITimeLog timeLog)
            : this(timeSystem)
        {
            TimeLogsManager = timeLogsManager;
            TimeLog = timeLog;
        }

        public TimeManager(ITimeSystem timeSystem, ITimeLogsManager timeLogsManager)
            : this(timeSystem, timeLogsManager, new TimeLog(timeSystem.Now.Date)) { }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            currentActivity.Name = finishedActivity;
            SwitchTo(nextActivity);
        }

        public IActivity SwitchTo(string nextActivityName)
        {
            currentActivity.Stop();

            DateTime endTime = currentActivity.StartTime + currentActivity.Duration;
            DateTime endDate = endTime.Date;
            if (currentActivity.StartTime.Date < endDate)
                PerformMidnightCorrection(endDate);
            if (TimeLog != null)
                TimeLog.AddActivity(currentActivity);
            previousActivity = currentActivity;
            currentActivity = RunningActivity.After(previousActivity, nextActivityName);
            return currentActivity;
        }

        public void PerformMidnightCorrection(DateTime endDate)
        {
            TimeSpan oldDayActivityDuration = endDate - currentActivity.StartTime;
            TimeSpan newDayActivityDuration = currentActivity.Duration - oldDayActivityDuration;
            currentActivity.Duration = oldDayActivityDuration;
            if (TimeLog != null)
            {
                TimeLog.AddActivity(currentActivity);
                if (TimeLogsManager != null)
                {
                    TimeLogsManager.Save();
                }
            }
            TimeLog = new TimeLog(endDate);
            if (TimeLogsManager != null)
                TimeLogsManager.UpdateTimeLogReferencies(TimeLog);
            currentActivity.StartTime = endDate;
            currentActivity.Duration = newDayActivityDuration;
        }
    }
}
