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
    /// <summary>
    /// Provide History Data
    /// </summary>
    public class HistoryDataProvider: IHistoryDataProvider
    {
        #region Fields

        private IActivitiesSummary activitiesSummary;
        private DataTable table;
        private ITaskCollection taskCollection;
        private ITasksSummary tasksSummary;

        #endregion Fields

        #region Properties

        public IActivitiesHistory ActivitiesHistory { get; set; }

        public IActivitiesSummary ActivitiesSummary { get { return activitiesSummary; } set { activitiesSummary = value; } }

        public DataTable ActivitiesSummaryData { get { if (activitiesSummary != null) return activitiesSummary.Data; else return null; } }

        public TimeSpan AllActivitiesTime { get { if (activitiesSummary != null)return activitiesSummary.AllActivitiesTime; else return TimeSpan.Zero; } }

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

        public ITaskCollection TaskCollection
        {
            get
            {
                return taskCollection;
            }
            set
            {
                this.taskCollection = value;
                if (this.activitiesSummary != null)
                    activitiesSummary.Linker = taskCollection;
                if (this.TasksSummary != null)
                    this.TasksSummary.TaskCollection = taskCollection;
            }
        }

        public ITasksSummary TasksSummary
        {
            get { return tasksSummary; }
            set { tasksSummary = value; }
        }

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

        public object TasksSummaryData
        {
            get
            {
                if(TasksSummary!=null)
                    return TasksSummary.Data;
                return null;
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

        public HistoryDataProvider(ITimeLogsManager timeLogsManager, ITaskCollection taskCollection)
        {
            TimeLogsManager = timeLogsManager;
            TaskCollection = taskCollection;
            table = CreateTable();
            ActivitiesHistory = new ActivitiesHistory();
        }

        #region Public Methods

        public void ApplySettings(ISettings settings)
        {
            this.ActivitiesHistory.LatestSize = settings.ActivitiesNumberInTray;
            this.ActivitiesHistory.Size = settings.MaxActivitiesInHistory;
        }

        public void CreateSummaries(ITimeLog timeLog)
        {
            this.activitiesSummary = new ActivitiesSummary(timeLog, TaskCollection);
            this.tasksSummary = new TasksSummary(ActivitiesSummaryData, TaskCollection);
        }

        public void SetSummaryPeriod(DateTime from, DateTime to)
        {
            activitiesSummary.TimeLogs = TimeLogsManager.GetTimeLogs(from, to);
            TasksSummary goodTasksSummary = tasksSummary as TasksSummary;
            if (goodTasksSummary != null)
                goodTasksSummary.Calculate();
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

        public void UpdateTimeLog(ITimeLog timeLog)
        {
            this.activitiesSummary.TimeLog = timeLog;
            this.activitiesSummary.Update();
        }

        #endregion Public Methods

        #region Private Methods

        private DataTable CreateTable()
        {
            var table = new DataTable();
            table.Columns.Add("Day");
            table.Columns.Add("Spent");
            return table;
        }

        private void ResetRows()
        {
            this.table.Clear();
        }

        #endregion Private Methods
    }
}