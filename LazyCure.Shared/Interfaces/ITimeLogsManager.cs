using System;
using System.Collections.Generic;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    /// <summary>
    /// Manage time logs
    /// </summary>
    public interface ITimeLogsManager
    {
        /// <summary>
        /// Returns active time log
        /// </summary>
        ITimeLog ActiveTimeLog { get; }
        
        /// <summary>
        /// Returns list of DateTime values for which time logs are available. Empty List is returned if not time logs at all
        /// </summary>
        List<DateTime> AvailableDays { get; }

        /// <summary>
        /// File manager used for storing timelogs
        /// </summary>
        ITimeLogsFileManager FileManager { get; set; }

        /// <summary>
        /// Activates time log for specific date
        /// </summary>
        /// <param name="dateTime">date</param>
        /// <returns>active time log</returns>
        ITimeLog ActivateTimeLog(DateTime dateTime);

        /// <summary>
        /// Get activities for specific day
        /// </summary>
        /// <param name="day">day</param>
        /// <returns>list of activities</returns>
        List<IActivity> GetActivities(DateTime day);

        /// <summary>
        /// Get timelogs for specific period
        /// </summary>
        /// <param name="from">date to start from</param>
        /// <param name="to">end date</param>
        /// <returns>list of timelogs</returns>
        List<ITimeLog> GetTimeLogs(DateTime from, DateTime to);

        /// <summary>
        /// Save active timelog
        /// <returns>true or false, indicating saved or not</returns>
        /// </summary>
        bool SaveActiveTimeLog();
    }
}
