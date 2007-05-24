using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Interfaces;
using System.IO;
using System.Data;

namespace LifeIdea.LazyCure.Core
{
    public class Driver : ILazyCureDriver
    {
        private ITimeSystem timeSystem;
        private TimeLog timeLog;

        public static string FirstActivityName = "starting LazyCure";
        public string TimeLogsFolder;

        public Driver(ITimeSystem timeSystem)
        {
            this.timeSystem = timeSystem;
            timeLog = new TimeLog(timeSystem, FirstActivityName);
        }
        public Driver() : this(new RunTimeSystem()) { }

        #region ILazyCureDriver Members
        public IActivity CurrentActivity { get { return timeLog.CurrentActivity; } }
        public IActivity PreviousActivity { get { return timeLog.PreviousActivity; } }
        public object ActivitiesSummaryData { get { return timeLog.ActivitiesSummary; } }
        public object TimeLogData { get { return timeLog.Data; } }
        /// <summary>
        /// switch from one activity to another
        /// </summary>
        /// <param name="nextActivity">name of next activity</param>
        /// <returns>activity after switching</returns>
        public IActivity SwitchTo(string nextActivityName)
        {
            IActivity nextActivity = timeLog.SwitchTo(nextActivityName);
            return nextActivity;
        }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            timeLog.FinishActivity(finishedActivity, nextActivity);
        }
        #endregion

        public bool SaveTimeLog()
        {
            StreamWriter stream = null;
            try
            {
                Directory.CreateDirectory(TimeLogsFolder);
                stream = File.CreateText(GetTimeLogFileNameByDate(CurrentActivity.StartTime));
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
            timeLog.Save(stream);
            stream.Close();
            return true;
        }
        public bool LoadTimeLog(DateTime date)
        {
            string filename = GetTimeLogFileNameByDate(date);
            try
            {
                if (File.Exists(filename))
                {
                    timeLog.Load(filename);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        private string GetTimeLogFileNameByDate(DateTime date)
        {
            return TimeLogsFolder + @"\" + date.ToString("yyyy-MM-dd") + ".timelog";
        }
    }
}
