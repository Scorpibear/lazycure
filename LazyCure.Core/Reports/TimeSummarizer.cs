using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.Reports
{
    /// <summary>
    /// abstract class for time summarizers
    /// </summary>
    public abstract class TimeSummarizer
    {
        protected string entityName;

        protected DataTable table;

        protected ITimeLogsManager timeLogsManager;

        public void AddSpentForDay(DateTime day)
        {
            TimeSpan spent = this.GetSpentOnDay(day);
            if (spent != TimeSpan.Zero)
                AddRow(day, spent);
        }

        private TimeSpan GetSpentOnDay(DateTime day)
        {
            if (timeLogsManager != null)
            {
                List<IActivity> activities = timeLogsManager.GetActivities(day);
                return this.SummarizeSpent(activities);
            }
            return TimeSpan.Zero;
        }

        public abstract TimeSpan SummarizeSpent(List<IActivity> activities);

        private void AddRow(DateTime day, TimeSpan spent)
        {
            string dayString = Format.Date(day);
            string spentString = Format.ShortDuration(spent);
            AddRow(dayString, spentString);
        }

        private void AddRow(string day, string spent)
        {
            DataRow row = table.NewRow();
            row["Day"] = day;
            row["Spent"] = spent;
            table.Rows.Add(row);
        }
    }
}
