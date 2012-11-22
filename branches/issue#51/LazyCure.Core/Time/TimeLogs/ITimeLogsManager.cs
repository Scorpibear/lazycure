using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time.TimeLogs
{
    public interface ITimeLogsManager
    {
        bool Save();
        void ActivateTimeLog(ITimeLog timeLog);

        List<IActivity> GetActivities(DateTime day);

        /// <summary>
        /// Returns list of DateTime values for which time logs are available. Empty List is returned if not time logs at all
        /// </summary>
        List<DateTime> AvailableDays { get; }
    }
}
