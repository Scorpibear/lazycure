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
            data.Columns.Add("Activity");
            data.Columns.Add("Start", Type.GetType("System.DateTime"));
            data.Columns.Add("Duration", Type.GetType("System.TimeSpan"));
            //data.Columns.Add("End", Type.GetType("System.DateTime"),"Start+Duration");
            activitiesSummary = new DataTable("ActivitiesSummary");
            activitiesSummary.Columns.Add("Activity");
            activitiesSummary.Columns.Add("Spent", Type.GetType("System.TimeSpan"));
            data.RowChanged += new DataRowChangeEventHandler(data_RowChanged);
        }

        public IActivity SwitchTo(string nextActivity)
        {
            currentActivity.Stop();

            data.Rows.Add(currentActivity.Name, currentActivity.StartTime, currentActivity.Duration);
            
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
                    if ((string)activitiesSummary.Rows[iRowIndex]["Activity"] == (string)theRow["Activity"])
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
    }
}
