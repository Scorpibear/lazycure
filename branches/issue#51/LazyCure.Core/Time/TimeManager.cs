using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Core.Time.TimeLogs;

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
        private IMidnightCorrector midnightCorrector;
        private RunningActivity previousActivity;
        private bool splitByComma;
        private bool switchAtMidnight;
        private ITimeLog timeLog;
        private ITimeLogsManager timeLogsManager;
        private string tweetingActivity;
        

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

        public IMidnightCorrector MidnightCorrector
        {
            set { midnightCorrector = value; }
        }

        public IActivity PreviousActivity
        {
            get { return previousActivity; }
        }

        public bool SplitByComma { set { splitByComma = value; } }

        public bool SwitchAtMidnight { set { switchAtMidnight = value; } }

        public ITimeLog TimeLog
        {
            get
            {
                
                if (timeLogsManager != null)
                    return timeLogsManager.ActiveTimeLog;
                return timeLog;
            }
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

        public string TweetingActivity
        {
            get { return tweetingActivity; }
            set { tweetingActivity = value; }
        }

        #endregion Properties

        public TimeManager(ITimeSystem timeSystem, ITimeLogsManager timeLogsManager)
        {
            currentActivity = new RunningActivity(FIRST_ACTIVITY, timeSystem);
            midnightCorrector = new MidnightSwitcher();
            if (timeLogsManager != null)
            {
                TimeLogsManager = timeLogsManager;
                timeLogsManager.ActivateTimeLog(currentActivity.Start.Date);
            }
        }

        public TimeManager(ITimeSystem timeSystem) : this(timeSystem, null) { }

        /// <summary>
        /// Finish activity and start the next
        /// </summary>
        /// <param name="finishedActivityName">finished activity name</param>
        /// <param name="nextActivityName">next activity name</param>
        /// <returns>finished activity object</returns>
        public List<IActivity> FinishActivity(string finishedActivityName, string nextActivityName)
        {
            IActivity finishedActivity = currentActivity;
            finishedActivity.Name = finishedActivityName;
            return SwitchTo(nextActivityName);
        }

        /// <summary>
        /// Switch to the next activity and returns list of finished activities
        /// </summary>
        /// <param name="nextActivityName">name of the next activity</param>
        /// <returns>list of finished activities</returns>
        public List<IActivity> SwitchTo(string nextActivityName)
        {
            Stop();
            List<IActivity> finishedActivities = CheckForComma();
            StartNext(nextActivityName);
            return finishedActivities;
        }
        
        /// <summary>
        /// Check current activity for comma and split if necessary
        /// <returns>list of additional activities created by split by comma or empty array</returns>
        /// </summary>
        private List<IActivity> CheckForComma()
        {
            RunningActivity[] additionalActivities = (splitByComma) ?
                currentActivity.SplitByComma() :
                new RunningActivity[]{currentActivity};
            foreach (RunningActivity additionalActivity in additionalActivities)
            {
                CheckForMidnight();
                currentActivity = additionalActivity;
                AddToTimeLog();
            }
            return new List<IActivity>(additionalActivities);
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

        public void CheckForMidnight()
        {
            if (switchAtMidnight && midnightCorrector!=null)
                midnightCorrector.PerformMidnightCorrection(currentActivity, timeLogsManager);
        }
    }
}