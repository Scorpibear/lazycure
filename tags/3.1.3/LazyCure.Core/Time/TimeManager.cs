using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    /// <summary>
    /// Manages time
    /// </summary>
    public class TimeManager: ITimeManager
    {
        private RunningActivity currentActivity;
        private RunningActivity previousActivity = null;
        private ITimeLog timeLog = null;

        private const string FIRST_ACTIVITY = "Activity1";

        public TimeManager(ITimeSystem timeSystem)
        {
            currentActivity = new RunningActivity(FIRST_ACTIVITY, timeSystem);
        }

        public IActivity CurrentActivity
        {
            get { return currentActivity; }
        }

        public IActivity PreviousActivity
        {
            get { return previousActivity; }
        }

        public ITimeLog TimeLog
        {
            get{ return timeLog;}
            set{ timeLog = value;}
        }

        public ITimeSystem TimeSystem
        {
            get { return currentActivity.timeSystem; }
        }

        public IActivity SwitchTo(string nextActivityName)
        {
            currentActivity.Stop();
            if(TimeLog!=null)
                TimeLog.AddActivity(currentActivity);
            previousActivity = currentActivity;
            currentActivity = RunningActivity.After(previousActivity, nextActivityName);
            return currentActivity;
        }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            currentActivity.Name = finishedActivity;
            SwitchTo(nextActivity);
        }
    }
}
