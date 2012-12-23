using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Reports
{
    /// <summary>
    /// help to summarize time for specific activity in all history
    /// </summary>
    public class ActivityTimeSummarizer: TimeSummarizer
    {
        public ActivityTimeSummarizer(string activityName, DataTable table, ITimeLogsManager timeLogsManager)
        {
            this.entityName = activityName;
            this.table = table;
            this.timeLogsManager = timeLogsManager;
        }

        public override TimeSpan SummarizeSpent(List<IActivity> activities)
        {
            TimeSpan totallySpent = TimeSpan.Zero;
            foreach (IActivity activity in activities)
            {
                if (activity.Name == this.entityName)
                    totallySpent += activity.Duration;
            }
            return totallySpent;
        }
    }
}
