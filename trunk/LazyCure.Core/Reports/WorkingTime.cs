using System;
using System.Data;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Reports
{
    /// <summary>
    /// Calculate efficiency,
    ///  calculates time on work,
    ///  calculates working tasks time,
    ///  calculates working time intervals,
    ///  define is activity is working
    /// </summary>
    public class WorkingTime : IWorkingTimeManager
    {
        private bool calculateAutomatically = true;
        private DataTable table = null;
        private ITimeLog timeLog;
        private ITaskCollection taskCollection;
        private TimeSpan possibleWorkInterruption = TimeSpan.Parse("0:25");
        private TimeSpan previousWorkingTasksTime;

        public WorkingTime(ITimeLog timeLog, ITaskCollection taskCollection)
        {
            TimeLog = timeLog;
            TaskCollection = taskCollection;
            CreateTable();
            FillTable();
        }

        public bool CalculateAutomatically
        {
            set
            {
                calculateAutomatically = value;
                FillTable();
            }
        }

        public TimeSpan PossibleWorkInterruption
        {
            get { return possibleWorkInterruption; }
            set
            {
                possibleWorkInterruption = value;
                FillTable();
            }
        }

        public ITaskCollection TaskCollection
        {
            set { taskCollection = value; }
        }

        public ITimeLog TimeLog
        {
            set
            {
                timeLog = value;
                if (timeLog.Data != null)
                {
                    timeLog.Data.RowDeleted += TimeLogData_RowChanged;
                    timeLog.Data.RowChanged += TimeLogData_RowChanged;
                }
                FillTable();
            }
        }

        public TimeSpan TimeOnWork
        {
            get
            {
                TimeSpan result = TimeSpan.Zero;
                foreach (DataRow row in table.Rows)
                {
                    result += (DateTime)row["End"] - (DateTime)row["Start"];
                }
                return result;
            }
        }

        public TimeSpan WorkingTasksTime
        {
            get
            {
                TimeSpan result = TimeSpan.Zero;
                foreach (IActivity activity in timeLog.Activities)
                {
                    if (IsWorking(activity))
                        result += activity.Duration;
                }
                UpdateTable(result);
                return result;
            }
        }

        public DataTable Intervals
        {
            get { return table; }
        }

        public bool IsWorking(IActivity activity)
        {
            return taskCollection.IsWorkingActivity(activity.Name);
        }

        private void CreateTable()
        {
            table = null;
            table = new DataTable();
            table.Columns.Add("Start", DateTime.MinValue.GetType());
            table.Columns.Add("End", DateTime.MinValue.GetType());
        }

        private void FillTable()
        {
            if (table != null && calculateAutomatically)
            {
                table.Rows.Clear();
                DateTime start = DateTime.MinValue;
                DateTime end = start;
                foreach (IActivity activity in timeLog.Activities)
                {
                    if (IsWorking(activity))
                    {
                        if (start == DateTime.MinValue)
                            start = activity.StartTime;
                        end = activity.StartTime + activity.Duration;
                    }
                    else
                    {
                        if (activity.Duration > PossibleWorkInterruption)
                            AddInterval(ref start, end);
                    }
                }
                AddInterval(ref start, end);
            }
        }

        private void UpdateTable(TimeSpan newWorkingTasksTime)
        {
            if (newWorkingTasksTime != previousWorkingTasksTime)
            {
                previousWorkingTasksTime = newWorkingTasksTime;
                FillTable();
            }
        }

        private void AddInterval(ref DateTime start, DateTime end)
        {
            if (start != DateTime.MinValue)
            {
                table.Rows.Add(start, end);
                start = DateTime.MinValue;
            }
        }

        private void TimeLogData_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            FillTable();
        }
    }
}
