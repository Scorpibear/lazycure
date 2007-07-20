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
        private string timeLogsFolder;
        private readonly History history;

        public static string FirstActivityName = "starting LazyCure";
        public bool SaveAfterDone=true;
        public string TimeLogsFolder { get { return timeLogsFolder; } set { timeLogsFolder = value; } }

        public Driver(ITimeSystem timeSystem)
        {
            this.timeSystem = timeSystem;
            timeLog = new TimeLog(timeSystem, FirstActivityName);
            history = new History();
        }
        public Driver() : this(new RunTimeSystem()) { }
        public void LoadHistory(string filename)
        {
            history.Load(filename);
        }
        public void SaveHistory(string filename)
        {
            history.Save(filename);
        }
        #region ILazyCureDriver Members

        public IActivity CurrentActivity { get { return timeLog.CurrentActivity; } }
        public object ActivitiesSummaryData { get { return timeLog.ActivitiesSummary; } }
        public object TimeLogData { get { return timeLog.Data; } }
        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            timeLog.FinishActivity(finishedActivity, nextActivity);
            history.AddActivity(finishedActivity);
            if(SaveAfterDone)
                SaveTimeLog();
        }
        public string TimeLogDate { get { return timeLog.Day.ToString("yyyy-MM-dd"); } }
        public bool LoadTimeLog(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    timeLog.Load(filename);
                    return true;
                }
                else
                {
                    Log.Error("Specified file does not exist");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }
        public string[] LatestActivities
        {
            get { return history.LatestActivities; }
        }
        public bool SaveTimeLog()
        {
            if (timeLogsFolder == "")
            {
                Log.Error("TimeLogsFolder is not specified");
                return false;
            }
            return SaveTimeLog(GetTimeLogFileNameByDate(timeLog.Day));
        }
        public bool SaveTimeLog(string filename)
        {
            StreamWriter stream = null;
            try
            {
                FileInfo fileInfo = new FileInfo(filename);
                fileInfo.Directory.Create();
                stream = fileInfo.CreateText();
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
            return LoadTimeLog(filename);
        }

        #endregion

        private string GetTimeLogFileNameByDate(DateTime date)
        {
            return TimeLogsFolder + @"\" + date.ToString("yyyy-MM-dd") + ".timelog";
        }
    }
}
