using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;
using LifeIdea.LazyCure.Core.Tasks;

namespace LifeIdea.LazyCure.Core.Reports
{
    public class HistoryDataProvider: IHistoryDataProvider
    {
        #region Fields
        
        DataTable table;

        #endregion Fields

        #region Properties

        public IActivitiesHistory ActivitiesHistory { get; set; }

        public DataTable Data
        {
            get
            {
                return table;
            }
        }

        public string[] HistoryActivities
        {
            get { return ActivitiesHistory.Activities; }
        }

        public string[] LatestActivities
        {
            get { return ActivitiesHistory.LatestActivities; }
        }

        public ITaskCollection TaskCollection { get; set; }

        /// <summary>
        /// Returns list of task names as string[] array. If no tasks, empty array is returned
        /// </summary>
        public string[] Tasks
        {
            get
            {
                if (this.TaskCollection != null)
                    return TaskCollection.GetAllTasksNames();
                return new string[] { };
            }
        }

        public ITimeLogsManager TimeLogsManager { get; set; }

        public string UniqueActivityName
        {
            get
            {
                return ActivitiesHistory.UniqueName;
            }
        }

        #endregion Properties

        public HistoryDataProvider(ITaskCollection taskCollection)
        {
            table = CreateTable();
            ActivitiesHistory = new ActivitiesHistory();
            TaskCollection = taskCollection;
        }

        private DataTable CreateTable()
        {
            var table = new DataTable();
            table.Columns.Add("Day");
            table.Columns.Add("Spent");
            return table;
        }

        public void UpdateDataTableForActivity(string activityName)
        {
            UpdateDataTable(new ActivityTimeSummarizer(activityName, table, TimeLogsManager));
        }

        public void UpdateDataTableForTask(string taskName)
        {
            UpdateDataTable(new TaskTimeSummarizer(taskName, table, TimeLogsManager, TaskCollection));
        }

        public void UpdateDataTable(TimeSummarizer timeSummarizer)
        {
            ResetRows();
            if (TimeLogsManager != null)
            {
                foreach (DateTime day in TimeLogsManager.AvailableDays)
                    timeSummarizer.AddSpentForDay(day);
            }
        }

        private void ResetRows()
        {
            this.table.Clear();
        }
        
        public void ApplySettings(ISettings settings)
        {
            this.ActivitiesHistory.LatestSize = settings.ActivitiesNumberInTray;
            this.ActivitiesHistory.Size = settings.MaxActivitiesInHistory;
        }
    }
}