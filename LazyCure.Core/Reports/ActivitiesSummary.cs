using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.Reports
{
    /// <summary>
    /// Represent activities summary data for current day
    /// </summary>
    public class ActivitiesSummary: IActivitiesSummary
    {
        private List<ITimeLog> timeLogs;
        private TimeSpan allActivitiesTime=new TimeSpan();

        public DataTable Data { get; set; }

        public ITaskActivityLinker Linker { get; set; }

        public TimeSpan AllActivitiesTime{get{ return allActivitiesTime;}}

        public ITimeLog TimeLog
        {
            get
            {
                if (timeLogs != null && timeLogs.Count > 0)
                    return timeLogs[timeLogs.Count-1];
                return null;
            }
            set
            {
                timeLogs = new List<ITimeLog>();
                timeLogs.Add(value);
                value.Data.RowDeleted += TimeLogData_RowChanged;
                value.Data.RowChanged += TimeLogData_RowChanged;
            }
        }

        public List<ITimeLog> TimeLogs
        {
            get { return timeLogs; }
            set
            {
                timeLogs = value;
                this.Update();
            }
        }
        
        public ActivitiesSummary(ITimeLog timeLog, ITaskActivityLinker linker)
        {
            Data = new DataTable("ActivitiesSummary");
            Data.Columns.Add("Activity");
            Data.Columns.Add("Spent", Type.GetType("System.TimeSpan"));
            Data.Columns.Add("Task");
            TimeLog = timeLog;            
            this.Linker = linker;
            Data.ColumnChanged += Data_ColumnChanged;
        }

        public void Update()
        {
            Data.Clear();
            allActivitiesTime = new TimeSpan(0);
            foreach (ITimeLog timeLog in TimeLogs)
                foreach (IActivity activity in timeLog.Activities)
                    AddActivityToSummaryData(activity);
        }

        private void AddActivityToSummaryData(IActivity activity)
        {
            bool existentRowUpdated = false;
            for (int iRowIndex = 0; iRowIndex < Data.Rows.Count; iRowIndex++)
            {
                if (((string)Data.Rows[iRowIndex]["Activity"] == activity.Name) &&
                    (Data.Rows[iRowIndex]["Spent"] != DBNull.Value))
                {
                    TimeSpan currentDuration = (TimeSpan)Data.Rows[iRowIndex]["Spent"];
                    Data.Rows[iRowIndex]["Spent"] = currentDuration + activity.Duration;
                    existentRowUpdated = true;
                    break;
                }
            }
            if (!existentRowUpdated)
            {
                string relatedTask = Linker.GetRelatedTaskName(activity.Name);
                Data.Rows.Add(activity.Name, activity.Duration, relatedTask);
            }

            allActivitiesTime += activity.Duration;
        }

        private void TimeLogData_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            Update();
        }

        private void Data_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if(e.Column==Data.Columns["Task"])
            {
                string activity = e.Row["Activity"] as string;
                string task = e.ProposedValue as string;
                bool isLinked = Linker.LinkActivityAndTask(activity, task);
                if(!isLinked)
                    Log.Error(String.Format("Could not link activity '{0}' and task '{1}'",activity,task));
            }
        }
    }
}