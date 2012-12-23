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
        /// Returns list of DateTime values for which time logs are available. Empty List is returned if not time logs at all
        /// </summary>
        List<DateTime> AvailableDays { get; }

        void ActivateTimeLog(ITimeLog timeLog);

        List<IActivity> GetActivities(DateTime day);

        bool Save();
    }
}
