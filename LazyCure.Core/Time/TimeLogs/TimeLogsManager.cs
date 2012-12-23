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

        public void ActivateTimeLog(ITimeLog timeLog)
        {
            ActiveTimeLog = timeLog;
        }

        public ITimeLog ActiveTimeLog { get; set; }

        public bool Save()
        {
            return fileManager.SaveTimeLog(ActiveTimeLog);
        }

        public List<DateTime> AvailableDays
        {
            get {
                List<DateTime> days;
                if (fileManager != null)
                {
                    days = fileManager.AllTimeLogDates;
                    days.Reverse();
                } else
                    days = new List<DateTime>();
                return days;
            }
        }
    }
}
