using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Core.Tasks;

namespace LifeIdea.LazyCure.Core.Reports
{
    /// <summary>
    /// Help to summarize time for task in all history
    /// </summary>
    public class TaskTimeSummarizer: TimeSummarizer
    {
        ITaskCollection taskCollection;

        public TaskTimeSummarizer(string taskName, DataTable table, ITimeLogsManager timeLogsManager, ITaskCollection taskCollection)
        {
            this.entityName = taskName;
            this.table = table;
            this.timeLogsManager = timeLogsManager;
            this.taskCollection = taskCollection;
        }

        public override TimeSpan SummarizeSpent(List<IActivity> activities)
        {
            TimeSpan totallySpent = TimeSpan.Zero;
            foreach (IActivity activity in activities)
            {
                Task task = taskCollection.GetTask(this.entityName);
                if (task.RelatedActivities.Contains(activity.Name))
                    totallySpent += activity.Duration;
            }
            return totallySpent;
        }
    }
}
