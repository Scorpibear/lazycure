using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Represent activities summary data
    /// </summary>
    public class ActivitiesSummary
    {
        private readonly ITimeLog timeLog;

        public DataTable Data;
        
        public ActivitiesSummary(ITimeLog timeLog)
        {
            Data = new DataTable("ActivitiesSummary");
            Data.Columns.Add("Activity");
            Data.Columns.Add("Spent", Type.GetType("System.TimeSpan"));
            this.timeLog = timeLog;
            timeLog.Data.RowChanged += TimeLogData_RowChanged;
        }
        public void Update()
        {
            Data.Clear();
            foreach (IActivity activity in timeLog.Activities)
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
                    Data.Rows.Add(activity.Name, activity.Duration);
                }
            }
        }

        private void TimeLogData_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            Update();
        }
    }
}
