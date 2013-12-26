using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time.TimeLogs
{
    /// <summary>
    /// Manage Time Logs
    /// </summary>
    public class TimeLogsManager: ITimeLogsManager
    {
        private ITimeLogsFileManager fileManager;

        public TimeLogsManager(ITimeLogsFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public ITimeLog ActivateTimeLog(DateTime dateTime)
        {
            if (fileManager != null)
            {
                SaveActiveTimeLog();
                var timeLog = fileManager.GetTimeLog(dateTime);
                if (timeLog == null)
                    timeLog = new TimeLog(dateTime);
                ActiveTimeLog = timeLog;
            }
            return ActiveTimeLog;
        }

        public ITimeLog ActiveTimeLog { get; set; }

        public ITimeLogsFileManager FileManager { get { return this.fileManager; } set { this.fileManager = value; } }

        public List<DateTime> AvailableDays
        {
            get
            {
                List<DateTime> days;
                if (fileManager != null)
                {
                    days = fileManager.AllTimeLogDates;
                    days.Reverse();
                }
                else
                    days = new List<DateTime>();
                if (ActiveTimeLog != null && !days.Contains(ActiveDay))
                    days.Add(ActiveDay);
                return days;
            }
        }
        
        public List<IActivity> GetActivities(DateTime day)
        {
            if (fileManager != null)
            {
                ITimeLog timeLog = GetTimeLog(day);
                if (timeLog != null)
                {
                    return timeLog.Activities;
                }
            }
            return null;
        }
        
        public List<ITimeLog> GetTimeLogs(DateTime from, DateTime to)
        {
            var timeLogs = new List<ITimeLog>();
            var listOfDays = this.AvailableDays;
            listOfDays = listOfDays.FindAll(
                delegate(DateTime day){
                    return from <= day && day <= to;
                }
            );
            foreach (DateTime day in listOfDays)
            {
                ITimeLog timeLog = GetTimeLog(day);
                timeLogs.Add(timeLog);
            }
            return timeLogs;
        }

        private ITimeLog GetTimeLog(DateTime day)
        {
            ITimeLog timeLog;
            if (day == ActiveDay)
                timeLog = ActiveTimeLog;
            else
                timeLog = fileManager.GetTimeLog(day);
            return timeLog;
        }

        public DateTime ActiveDay
        {
            get
            {
                if(ActiveTimeLog!=null)
                    return ActiveTimeLog.Date;
                return DateTime.MinValue;
            }
        }

        public bool SaveActiveTimeLog()
        {
            if (fileManager != null && ActiveTimeLog != null)
                return fileManager.SaveTimeLog(ActiveTimeLog);
            return false;
        }
    }
}
