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
        private bool splitByComma;
        private bool switchAtMidnight;
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

        public bool SplitByComma { set { splitByComma = value; } }

        public bool SwitchAtMidnight { set { switchAtMidnight = value; } }

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

        public TimeManager(ITimeSystem timeSystem, ITimeLogsManager timeLogsManager):this(timeSystem)
        {
            TimeLogsManager = timeLogsManager;
            TimeLog = new TimeLog(currentActivity.Start.Date);
        }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            currentActivity.Name = finishedActivity;
            SwitchTo(nextActivity);
        }

        public IActivity SwitchTo(string nextActivityName)
        {
            Stop();
            CheckForComma();
            CheckForMidnight();
            AddToTimeLog();
            StartNext(nextActivityName);
            return currentActivity;
        }

        private void CheckForComma()
        {
            if(splitByComma)
                foreach (RunningActivity additionalActivity in currentActivity.SplitByComma())
                {
                    CheckForMidnight();
                    AddToTimeLog();
                    currentActivity = additionalActivity;
                }
        }

        private void Stop()
        {
            currentActivity.Stop();
        }

        private void StartNext(string nextActivityName)
        {
            previousActivity = currentActivity;
            currentActivity = RunningActivity.After(previousActivity, nextActivityName);
        }

        private void AddToTimeLog()
        {
            if (TimeLog != null)
                TimeLog.AddActivity(currentActivity);
        }

        private void CheckForMidnight()
        {
            DateTime endTime = currentActivity.Start + currentActivity.Duration;
            DateTime endDate = endTime.Date;
            if ((currentActivity.Start.Date < endDate) && switchAtMidnight)
                PerformMidnightCorrection(endDate);
        }

        public void PerformMidnightCorrection(DateTime endDate)
        {
            TimeSpan oldDayActivityDuration = endDate - currentActivity.Start;
            TimeSpan newDayActivityDuration = currentActivity.Duration - oldDayActivityDuration;
            currentActivity.Duration = oldDayActivityDuration;
            AddToTimeLog();
            if (TimeLogsManager != null)
                TimeLogsManager.Save();
            TimeLog = new TimeLog(endDate);
            if (TimeLogsManager != null)
                TimeLogsManager.UpdateTimeLogReferencies(TimeLog);
            currentActivity.Start = endDate;
            currentActivity.Duration = newDayActivityDuration;
        }
    }
}
