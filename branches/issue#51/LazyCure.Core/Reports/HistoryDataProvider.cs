using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.Reports
{
    public class HistoryDataProvider
    {
        DataTable table;

        public HistoryDataProvider()
        {
            table = CreateTable();
        }

        private DataTable CreateTable()
        {
            var table = new DataTable();
            table.Columns.Add("Day");
            table.Columns.Add("Spent");
            return table;
        }

        public DataTable DataTable
        {
            get
            {
                return table;
            }
        }

        public void UpdateDataTableForActivity(string activityName)
        {
            ResetRows();
            //DateTime day = DateTime.Now;
            if(TimeLogsManager!=null)
            {
                foreach (DateTime day in TimeLogsManager.AvailableDays)
                    AddSpentForActivity(activityName, day);
            }
        }

        private void AddSpentForActivity(string activityName, DateTime day)
        {
            TimeSpan spent = this.GetSpentOnActivity(day, activityName);
            if (spent != TimeSpan.Zero)
                AddRow(day, spent);
        }

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

        private void ResetRows()
        {
            this.table.Clear();
        }
        private TimeSpan GetSpentOnActivity(DateTime day, string activityName)
        {
            if (TimeLogsManager != null)
            {
                List<IActivity> activities = TimeLogsManager.GetActivities(day);
                return this.SummarizeSpentForActivity(activities, activityName);
            }
            return TimeSpan.Zero;
        }

        public TimeSpan SummarizeSpentForActivity(List<IActivity> activities, string activityName)
        {
            TimeSpan totallySpent = TimeSpan.Zero;
            foreach (IActivity activity in activities)
            {
                if (activity.Name == activityName)
                    totallySpent += activity.Duration;
            }
            return totallySpent;
        }
        public ITimeLogsManager TimeLogsManager { get; set; }
    }
}