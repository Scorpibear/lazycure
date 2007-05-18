using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LifeIdea.LazyCure.Interfaces;
using System.Data;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Store information about activities
    /// </summary>
    public class TimeLog
    {
        private Activity currentActivity, previousActivity = null;
        private List<IActivity> activitiesList = new List<IActivity>();
        private DataTable activitiesSummary;
        private DataTable data;

        public IActivity CurrentActivity { get { return currentActivity; } }
        public IActivity PreviousActivity { get { return previousActivity; } }
        public DataTable ActivitiesSummary { get { return activitiesSummary; } }
        public DataTable Data { get { return data; } }

        public TimeLog(ITimeSystem timeSystem, string firstActivityName)
        {
            currentActivity = new Activity(firstActivityName, timeSystem);
            activitiesList.Add(currentActivity);
            data = new DataTable("TimeLog");

            DataColumn startCol = data.Columns.Add("Start", Type.GetType("System.DateTime"));
            DataColumn activityCol = data.Columns.Add("Activity");
            DataColumn durationCol = data.Columns.Add("Duration", Type.GetType("System.TimeSpan"));
            DataColumn endCol = data.Columns.Add("End", Type.GetType("System.DateTime"));
            DataView dataView = new DataView(data);
            activitiesSummary = new DataTable("ActivitiesSummary");
            activitiesSummary.Columns.Add("Activity");
            activitiesSummary.Columns.Add("Spent", Type.GetType("System.TimeSpan"));
            data.RowChanged += new DataRowChangeEventHandler(data_RowChanged);
            data.ColumnChanging += new DataColumnChangeEventHandler(data_ColumnChanging);
        }
        public IActivity SwitchTo(string nextActivity)
        {
            currentActivity.Stop();
            DataRow newRow = data.NewRow();
            newRow["Activity"] = currentActivity.Name;
            newRow["Start"] = currentActivity.StartTime;
            newRow["Duration"] = currentActivity.Duration;
            data.Rows.Add(newRow);
            
            previousActivity = currentActivity;
            currentActivity = Activity.After(previousActivity, nextActivity);
            activitiesList.Add(currentActivity);

            return currentActivity;
        }
        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            currentActivity.Name = finishedActivity;
            SwitchTo(nextActivity);
        }
        public void Save(TextWriter writer)
        {
            writer.WriteLine("<?xml version=\"1.0\" standalone=\"yes\"?>");
            writer.WriteLine("<LazyCureData>");
            foreach (IActivity activity in activitiesList)
                writer.WriteLine(activity.ToString());
            writer.WriteLine("</LazyCureData>");
        }
        private void data_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            activitiesSummary.Clear();
            foreach(DataRow theRow in data.Rows)
            {
                bool isRowAdded = false;
                for (int iRowIndex = 0; iRowIndex < activitiesSummary.Rows.Count; iRowIndex++)
                {
                    if (((string)activitiesSummary.Rows[iRowIndex]["Activity"] == (string)theRow["Activity"]) &&
                        activitiesSummary.Rows[iRowIndex]["Spent"].GetType()!= Type.GetType("System.DBNull")
                        )
                    {
                        TimeSpan currentDuration = (TimeSpan)activitiesSummary.Rows[iRowIndex]["Spent"];
                        activitiesSummary.Rows[iRowIndex]["Spent"] = currentDuration + (TimeSpan)theRow["Duration"];
                        isRowAdded = true;
                        break;
                    }
                }
                if (!isRowAdded)
                {
                    activitiesSummary.Rows.Add(theRow["Activity"], theRow["Duration"]);
                }
            }
        }
        private void data_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            if ((e.Column.ColumnName == "Start" || e.Column.ColumnName == "Duration") &&
                e.Row["Duration"].GetType() != Type.GetType("System.DBNull") &&
                e.Row["Start"].GetType() != Type.GetType("System.DBNull")
                )
            {
                e.Row["End"] = ((DateTime)(e.Row["Start"])) + ((TimeSpan)(e.Row["Duration"]));
                return;
            }
            if ((e.Column.ColumnName == "Start" || e.Column.ColumnName == "End") &&
                e.Row["Start"].GetType() != Type.GetType("System.DBNull") &&
                e.Row["End"].GetType() != Type.GetType("System.DBNull")
                )
            {
                e.Row["Duration"] = (DateTime)e.Row["End"] - (DateTime)e.Row["Start"];
                return;
            }
        }
    }
}
