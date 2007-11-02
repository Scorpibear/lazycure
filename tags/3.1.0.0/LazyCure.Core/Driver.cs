using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    public class Driver : ILazyCureDriver
    {
        private Activity currentActivity,previousActivity;
        private ITimeSystem timeSystem;

        public string FirstActivityName = "starting LazyCure";
     
        public Driver(ITimeSystem timeSystem)
        {
            this.timeSystem = timeSystem;
            SwitchTo(FirstActivityName);
        }
        public Driver() : this(new RunTimeSystem()) { }

        #region ILazyCureDriver Members
        public IActivity CurrentActivity { get { return currentActivity; } }
        public IActivity PreviousActivity { get { return previousActivity; } }
        /// <summary>
        /// switch from one activity to another
        /// </summary>
        /// <param name="nextActivity">name of next activity</param>
        /// <returns>activity after switching</returns>
        public IActivity SwitchTo(string nextActivity)
        {
            if (currentActivity != null)
                currentActivity.Stop();
            previousActivity = currentActivity;
            currentActivity = new Activity(nextActivity, timeSystem);
            return currentActivity;
        }
        
        #endregion
    }
}