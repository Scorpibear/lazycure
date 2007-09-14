using System;
using LifeIdea.LazyCure.Interfaces;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    public class Driver : ILazyCureDriver
    {
        private readonly TimeLog timeLog;
        private readonly ActivitiesSummary activitiesSummary;
        private string timeLogsFolder;
        private readonly History history;

        public static string FirstActivityName = "starting LazyCure";
        public bool SaveAfterDone=true;
        public string TimeLogsFolder { get { return timeLogsFolder; } set { timeLogsFolder = value; } }

        public Driver(ITimeSystem timeSystem)
        {
            timeLog = new TimeLog(timeSystem, FirstActivityName);
            activitiesSummary = new ActivitiesSummary(timeLog);
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
        public TimeSpan AllActivitiesTime { get { return activitiesSummary.AllActivitiesTime; } }
        public IActivity CurrentActivity { get { return timeLog.CurrentActivity; } }
        public object ActivitiesSummaryData { get { return activitiesSummary.Data; } }
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
            if (File.Exists(filename))
            {
                timeLog.Load(filename);
                return true;
            }
            else
            {
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
