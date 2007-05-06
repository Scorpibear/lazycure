using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Interfaces;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    public class Driver : ILazyCureDriver
    {
        private Activity currentActivity,previousActivity=null;
        private ITimeSystem timeSystem;

        public const string FirstActivityName = "starting LazyCure";
        public string TimeLogsFolder;
        public string LastError;
     
        public Driver(ITimeSystem timeSystem)
        {
            this.timeSystem = timeSystem;
            currentActivity = new Activity(FirstActivityName, timeSystem);
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
            currentActivity.Stop();
            previousActivity = currentActivity;
            currentActivity = Activity.After(previousActivity,nextActivity);
            return currentActivity;
        }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            SwitchTo(nextActivity);
            previousActivity.Name = finishedActivity;
        }

        #endregion

        public bool SaveTimeLog()
        {
            StreamWriter stream = null;
            Boolean isSaved = false;
            try
            {
                Directory.CreateDirectory(TimeLogsFolder);
            }
            catch (Exception)
            {
                return isSaved;
            }
                stream = File.CreateText(TimeLogsFolder + @"\" + currentActivity.StartTime.ToString("yyyy-MM-dd") + ".timelog");
                stream.Write(currentActivity.ToString());
                isSaved = true;
                if(stream!=null)
                    stream.Close();
            return isSaved;
        }


    }
}
