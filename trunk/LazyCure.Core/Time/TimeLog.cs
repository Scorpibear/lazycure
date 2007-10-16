using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    /// <summary>
    /// Store information about activities
    /// </summary>
    public class TimeLog : ITimeLog
    {
        private readonly DataTable data;
        private LiveActivity currentActivity;
        private DateTime day;
        private bool isChanging = false;
        private LiveActivity previousActivity = null;

        public TimeLog(ITimeSystem timeSystem, string firstActivityName)
        {
            currentActivity = new LiveActivity(firstActivityName, timeSystem);
            day = currentActivity.StartTime;

            data = new DataTable("TimeLog");

            DataColumn startCol = data.Columns.Add("Start", Type.GetType("System.DateTime"));
            startCol.AllowDBNull = false;
            DataColumn activityCol = data.Columns.Add("Activity");
            activityCol.AllowDBNull = false;
            data.Columns.Add("Duration", Type.GetType("System.TimeSpan"));
            data.Columns.Add("End", Type.GetType("System.DateTime"));

            data.ColumnChanging += data_ColumnChanging;
        }

        public IActivity CurrentActivity
        {
            get { return currentActivity; }
        }

        public DateTime Day
        {
            get { return day; }
        }

        #region ITimeLog Members

        public DataTable Data
        {
            get { return data; }
        }

        public List<IActivity> Activities
        {
            get
            {
                List<IActivity> activities = new List<IActivity>();
                foreach (DataRow row in data.Rows)
                {
                    if (row["Duration"] != DBNull.Value)
                    {
                        IActivity activity = new Activity(
                            (string) row["Activity"],
                            (DateTime) row["Start"],
                            (TimeSpan) row["Duration"]
                            );
                        activities.Add(activity);
                    }
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

        #endregion

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
            writer.WriteLine(Serializer.TimeLogToString(this));
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
                        case "Start":
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
            day = DateTime.Parse(new FileInfo(filename).Name.Split('.')[0]);
            ;
        }

        private void AddNewActivity(string name, DateTime start, TimeSpan duration)
        {
            DataRow row = data.NewRow();
            row["Activity"] = name;
            row["Start"] = start;
            row["Duration"] = duration;
            data.Rows.Add(row);
        }

        private void data_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            if (isChanging)
                return;
            isChanging = true;
            switch (e.Column.ColumnName)
            {
                case "Start":
                    if (HasValues(e.ProposedValue, e.Row["Duration"]))
                        e.Row["End"] = (DateTime) e.ProposedValue + (TimeSpan) e.Row["Duration"];
                    else if (HasValues(e.Row["End"], e.ProposedValue))
                        e.Row["Duration"] = (DateTime) e.Row["End"] - (DateTime) e.ProposedValue;
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
            }
            isChanging = false;
        }

        private static bool HasValues(object a, object b)
        {
            return !(Convert.IsDBNull(a) || Convert.IsDBNull(b));
        }
    }
}