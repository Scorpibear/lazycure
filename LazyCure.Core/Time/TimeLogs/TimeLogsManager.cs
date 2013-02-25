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
        private IFileManager fileManager;

        public TimeLogsManager(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public void ActivateTimeLog(ITimeLog timeLog)
        {
            ActiveTimeLog = timeLog;
        }

        public ITimeLog ActiveTimeLog { get; set; }
        
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
                return days;
            }
        }
        
        public List<IActivity> GetActivities(DateTime day)
        {
            if (fileManager != null)
            {
                ITimeLog timeLog = fileManager.GetTimeLog(day);
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
                timeLogs.Add(fileManager.GetTimeLog(day));
            return timeLogs;
        }
        
        public bool Save()
        {
            return fileManager.SaveTimeLog(ActiveTimeLog);
        }
    }
}
