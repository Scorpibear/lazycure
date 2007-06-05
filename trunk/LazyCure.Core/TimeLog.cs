using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LifeIdea.LazyCure.Interfaces;
using System.Data;
using System.Xml;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Store information about activities
    /// </summary>
    public class TimeLog
    {
        private LiveActivity currentActivity, previousActivity = null;
        private DataTable activitiesSummary;
        private DataTable data;
        private DateTime day;

        public IActivity CurrentActivity { get { return currentActivity; } }
        public DataTable ActivitiesSummary { get { return activitiesSummary; } }
        public DataTable Data { get { return data; } }
        public DateTime Day { get { return day; } }
        public List<IActivity> Activities
        {
            get
            {
                List<IActivity> activities = new List<IActivity>();
                foreach (DataRow row in data.Rows)
                {
                    IActivity activity = new Activity(
                        (string)row["Activity"],
                        (DateTime)row["Start"],
                        (TimeSpan)row["Duration"]
                        );
                    activities.Add(activity);
                }
                return activities;
            }
            set
            {
                data.Clear();
                foreach (IActivity activity in value)
                {
                    AddNewActivity(activity.Name, activity.StartTime, activity.Duration);
                }
            }
        }
        public TimeLog(ITimeSystem timeSystem, string firstActivityName)
        {
            currentActivity = new LiveActivity(firstActivityName, timeSystem);
            day = currentActivity.StartTime;

            data = new DataTable("TimeLog");

            DataColumn startCol = data.Columns.Add("Start", Type.GetType("System.DateTime"));
            DataColumn activityCol = data.Columns.Add("Activity");
            DataColumn durationCol = data.Columns.Add("Duration", Type.GetType("System.TimeSpan"));
            DataColumn endCol = data.Columns.Add("End", Type.GetType("System.DateTime"));

            activitiesSummary = new DataTable("ActivitiesSummary");
            activitiesSummary.Columns.Add("Activity");
            activitiesSummary.Columns.Add("Spent", Type.GetType("System.TimeSpan"));
            data.RowChanged += new DataRowChangeEventHandler(data_RowChanged);
            data.ColumnChanging += new DataColumnChangeEventHandler(data_ColumnChanging);
        }
        public IActivity SwitchTo(string nextActivity)
        {
            currentActivity.Stop();

            AddNewActivity(currentActivity.Name, currentActivity.StartTime, currentActivity.Duration);
            
            previousActivity = currentActivity;
            currentActivity = LiveActivity.After(previousActivity, nextActivity);
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
            foreach (DataRow row in data.Rows)
            {
                Activity activity = new Activity(
                    (string)row["Activity"],
                    (DateTime)row["Start"],
                    (TimeSpan)row["Duration"]
                    );
                writer.WriteLine(activity.ToString());
            }
            writer.WriteLine("</LazyCureData>");

        }

        public void Load(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            Data.Clear();
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                DateTime start = new DateTime();
                TimeSpan duration = new TimeSpan();
                string name = null;
                foreach (XmlNode parameter in node.ChildNodes)
                {
                    switch (parameter.Name)
                    {
                        case "Begin":
                            start = DateTime.Parse(parameter.InnerText);
                            break;
                        case "Duration":
                            duration = TimeSpan.Parse(parameter.InnerText);
                            break;
                        case "Activity":
                            name = parameter.InnerText;
                            break;
                    }
                }
                AddNewActivity(name, start, duration);
            }
            this.day = DateTime.Parse(new FileInfo(filename).Name.Split('.')[0]);;
        }

        private void AddNewActivity(string name, DateTime start, TimeSpan duration)
        {
            DataRow row = data.NewRow();
            row["Activity"] = name;
            row["Start"] = start;
            row["Duration"] = duration;
            data.Rows.Add(row);
        }
        private void data_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            CalculateActivitiesSummary();
            //CalculateTimeValues(e);
        }
        private bool isChanging = false;
        private void data_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            if (isChanging)
                return;
            isChanging = true;
            switch (e.Column.ColumnName)
            {
                case "Start":
                    if (HasValues(e.ProposedValue, e.Row["Duration"]))
                        e.Row["End"] = (DateTime)e.ProposedValue + (TimeSpan)e.Row["Duration"];
                    else if (HasValues(e.Row["End"], e.ProposedValue))
                        e.Row["Duration"] = (DateTime)e.Row["End"] - (DateTime)e.ProposedValue;
                    break;
                case "Duration":
                    if (HasValues(e.Row["Start"], e.ProposedValue))
                        e.Row["End"] = (DateTime)e.Row["Start"] + (TimeSpan)e.ProposedValue;
                    else if (HasValues(e.ProposedValue, e.Row["End"]))
                        e.Row["Start"] = (DateTime)e.Row["End"] - (TimeSpan)e.ProposedValue;
                    break;
                case "End":
                    if (HasValues(e.Row["Start"], e.ProposedValue))
                        e.Row["Duration"] = (DateTime)e.ProposedValue - (DateTime)e.Row["Start"];
                    else if (HasValues(e.Row["Duration"], e.ProposedValue))
                        e.Row["Start"] = (DateTime)e.ProposedValue - (TimeSpan)e.Row["Duration"];
                    break;
            }
            isChanging = false;

        }
        private void CalculateActivitiesSummary()
        {
            activitiesSummary.Clear();
            foreach (DataRow theRow in data.Rows)
            {
                bool isRowAdded = false;
                for (int iRowIndex = 0; iRowIndex < activitiesSummary.Rows.Count; iRowIndex++)
                {
                    if (((string)activitiesSummary.Rows[iRowIndex]["Activity"] == (string)theRow["Activity"]) &&
                        activitiesSummary.Rows[iRowIndex]["Spent"].GetType() != Type.GetType("System.DBNull")
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
        /*
        private static void CalculateTimeValues(DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                if (HasValues(e.Row["Duration"], e.Row["Start"]))
                {
                    DateTime endTime = (DateTime)e.Row["Start"] + (TimeSpan)e.Row["Duration"];
                    e.Row["End"] = endTime;
                    return;
                }
                if (HasValues(e.Row["Start"], e.Row["End"]))
                {
                    e.Row["Duration"] = (DateTime)e.Row["End"] - (DateTime)e.Row["Start"];
                    return;
                }
                if (HasValues(e.Row["Duration"], e.Row["End"]))
                {
                    e.Row["Start"] = (DateTime)e.Row["End"] - (TimeSpan)e.Row["Duration"];
                    return;
                }
            }
        }
         * */
        private static bool HasValues(object a, object b)
        {
            return !(Convert.IsDBNull(a) || Convert.IsDBNull(b));
        }

    }
}
