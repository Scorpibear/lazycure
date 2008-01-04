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
        private RunningActivity previousActivity = null;
        private ITimeLog timeLog = null;

        #region Properties

        public IActivity CurrentActivity
        {
            get { return currentActivity; }
        }

        public TimeSpan MaxDuration
        {
            get{ return maxDuration;}
            set{ maxDuration = value;}
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

        public ITimeSystem TimeSystem
        {
            get { return currentActivity.timeSystem; }
        }

        #endregion Properties

        public TimeManager(ITimeSystem timeSystem)
        {
            currentActivity = new RunningActivity(FIRST_ACTIVITY, timeSystem);
        }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            currentActivity.Name = finishedActivity;
            SwitchTo(nextActivity);
        }

        public IActivity SwitchTo(string nextActivityName)
        {
            currentActivity.Stop();
            if (TimeLog != null)
                TimeLog.AddActivity(currentActivity);
            previousActivity = currentActivity;
            currentActivity = RunningActivity.After(previousActivity, nextActivityName);
            return currentActivity;
        }
    }
}
