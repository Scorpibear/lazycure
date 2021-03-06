using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time.TimeLogs
{
    /// <summary>
    /// Represent time log for one day
    /// </summary>
    public class TimeLog : ITimeLog
    {
        private readonly DataTable data;
        private DateTime date;
        private string filename = null;

        public TimeLog(DateTime date)
        {
            this.date = date;

            data = new DataTable("TimeLog");

            DataColumn startCol = data.Columns.Add("Start", Type.GetType("System.DateTime"));
            startCol.AllowDBNull = false;
            DataColumn activityCol = data.Columns.Add("Activity");
            activityCol.AllowDBNull = false;
            data.Columns.Add("Duration", Type.GetType("System.TimeSpan"));
            data.Columns.Add("End", Type.GetType("System.DateTime"));

            data.ColumnChanging += data_ColumnChanging;
        }

        #region ITimeLog Members

        public List<IActivity> Activities
        {
            get
            {
                List<IActivity> activities = new List<IActivity>();
                foreach (DataRow row in data.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (row["Duration"] != DBNull.Value)
                        {
                            IActivity activity = new Activity(
                                (string)row["Activity"],
                                (DateTime)row["Start"],
                                (TimeSpan)row["Duration"]
                                );
                            activities.Add(activity);
                        }
                    }
                }
                return activities;
            }
            set
            {
                data.Clear();
                foreach (IActivity activity in value)
                {
                    AddActivity(activity.Name, activity.Start, activity.Duration);
                }
            }
        }

        public void AddActivity(IActivity activity)
        {
            AddActivity(activity.Name,activity.Start,activity.Duration);
        }

        public DataTable Data
        {
            get { return data; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value.Date; }
        }

        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }

        public void RenameActivities(string before, string after)
        {
            foreach (DataRow row in Data.Rows)
                if ((string)row["Activity"] == before)
                    row["Activity"] = after;
        }

        #endregion
        
        public override string ToString()
        {
            return date.ToShortDateString();
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        private void AddActivity(string name, DateTime start, TimeSpan duration)
        {
            DataRow row = data.NewRow();
            row["Activity"] = name;
            row["Start"] = start;
            row["Duration"] = duration;
            data.Rows.Add(row);
        }

        private void data_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            data.ColumnChanging -= data_ColumnChanging;
            switch (e.Column.ColumnName)
            {
                case "Start":
                    if (HasValues(e.Row["End"], e.ProposedValue))
                        e.Row["Duration"] = (DateTime)e.Row["End"] - (DateTime)e.ProposedValue;
                    else if (HasValues(e.ProposedValue, e.Row["Duration"]))
                        e.Row["End"] = (DateTime) e.ProposedValue + (TimeSpan) e.Row["Duration"];
                    if (HasValues(e.Row["Activity"], e.ProposedValue))
                        ReviewEndTime(e.Row, (DateTime)e.ProposedValue);
                    break;
                case "Duration":
                    if (HasValues(e.Row["Start"], e.ProposedValue))
                        e.Row["End"] = (DateTime) e.Row["Start"] + (TimeSpan) e.ProposedValue;
                    else if (HasValues(e.ProposedValue, e.Row["End"]))
                        e.Row["Start"] = (DateTime) e.Row["End"] - (TimeSpan) e.ProposedValue;
                    break;
                case "End":
                    if (HasValues(e.Row["Start"], e.ProposedValue))
                    {
                        DateTime end = (DateTime) e.ProposedValue;
                        DateTime start = (DateTime) e.Row["Start"];
                        if (start > end)
                            end = end + TimeSpan.FromDays(1);
                        e.Row["Duration"] = end - start;
                    }
                    else if (HasValues(e.Row["Duration"], e.ProposedValue))
                        e.Row["Start"] = (DateTime) e.ProposedValue - (TimeSpan) e.Row["Duration"];
                    break;
                case "Activity":
                    if (HasValues(e.Row["Start"], e.ProposedValue))
                        ReviewEndTime(e.Row, (DateTime)e.Row["Start"]);
                    break;
            }
            data.ColumnChanging += data_ColumnChanging;
        }

        /// <summary>
        /// Review end time of all rows comparing with new specified start time and correct it
        /// </summary>
        /// <param name="newRow">new row that is being added</param>
        /// <param name="newStartTime">new start time of recently added activity</param>
        private void ReviewEndTime(DataRow newRow, DateTime newStartTime)
        {
            DataRow next = null;
            DateTime nextStartTime = DateTime.MaxValue;
            for(int i=0;i<data.Rows.Count;i++)
            {
                DataRow row = data.Rows[i];
                if (row == newRow)
                    continue;
                DateTime startTime = (DateTime)row["Start"];
                //correct end time of activity which goes just before that
                if(startTime < newStartTime){
                    if((row["End"]==DBNull.Value) || ((DateTime)row["End"]>newStartTime)){
                        row["Duration"] = newStartTime - startTime;
                        row["End"] = newStartTime;
                    }
                }
                //finding the next row
                if ((startTime > newStartTime) && (startTime < nextStartTime))
                {
                    next = row;
                    nextStartTime = (DateTime)next["Start"];
                }
            }
            //autofill duration and end if the next activity after it has been found
            if (next != null)
            {
                newRow["Duration"] = nextStartTime - newStartTime;
                newRow["End"] = nextStartTime;
            }
        }

        private static bool HasValues(object a, object b)
        {
            return !(Convert.IsDBNull(a) || Convert.IsDBNull(b));
        }
    }
}
