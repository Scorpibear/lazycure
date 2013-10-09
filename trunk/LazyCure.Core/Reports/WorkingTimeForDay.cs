using System;
using System.Data;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;
using System.Collections.Generic;

namespace LifeIdea.LazyCure.Core.Reports
{
    /// <summary>
    /// Calculate efficiency,
    ///  calculates time on work,
    ///  calculates working tasks time,
    ///  calculates working time intervals
    /// </summary>
    public class WorkingTimeForDay : IWorkingTimeManager
    {
        private bool calculateAutomatically = true;
        private DataTable table = null;
        private ITimeLog timeLog;
        private IWorkDefiner workDefiner;
        private TimeSpan possibleWorkInterruption = TimeSpan.Parse("0:25");
        private TimeSpan previousWorkingTasksTime;

        public WorkingTimeForDay(ITimeLog timeLog, IWorkDefiner workDefiner)
        {
            TimeLog = timeLog;
            this.workDefiner = workDefiner;
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

        public IWorkDefiner WorkDefiner
        {
            set { workDefiner = value; }
        }

        public ITimeLog TimeLog
        {
            set
            {
                timeLog = value;
                if (timeLog!=null && timeLog.Data != null)
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
                var enumerator = GetActivityEnumerator();
                while(enumerator.MoveNext())
                {
                    IActivity activity = enumerator.Current;
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
            return workDefiner.IsWorkingActivity(activity.Name);
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
                if (timeLog != null)
                {
                    var enumerator = GetActivityEnumerator();
                    while (enumerator.MoveNext())
                    {
                        IActivity activity = enumerator.Current;
                        if (IsWorking(activity))
                        {
                            if (start == DateTime.MinValue)
                                start = activity.Start;
                            end = activity.Start + activity.Duration;
                        }
                        else
                        {
                            if (activity.Duration > PossibleWorkInterruption)
                                AddInterval(ref start, end);
                        }    
                    }
                }
                AddInterval(ref start, end);
            }
        }

        private List<IActivity>.Enumerator GetActivityEnumerator()
        {
            return timeLog.Activities.GetEnumerator();
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
