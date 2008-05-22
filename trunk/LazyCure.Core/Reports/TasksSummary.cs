using System;
using System.Data;
using LifeIdea.LazyCure.Core.Tasks;

namespace LifeIdea.LazyCure.Core.Reports
{
    public class TasksSummary: ITasksSummary
    {
        private readonly DataTable dataTable;
        private DataTable sourceTable;
        private ITaskCollection taskCollection;

        public DataTable ActivitiesSummaryTable
        {
            set
            {
                sourceTable = value;
                sourceTable.RowChanged += sourceTable_RowChanged;
                sourceTable.RowDeleted += sourceTable_RowChanged;
            }
            get
            {
                return sourceTable;
            }
        }

        public DataTable Data
        {
            get { return dataTable; }
        }

        public ITaskCollection TaskCollection
        {
            set { taskCollection = value; }
        }

        public TasksSummary(DataTable activitiesSummaryTable,ITaskCollection taskCollection)
        {
            dataTable = new DataTable("TasksSummary");
            dataTable.Columns.Add("Task");
            dataTable.Columns.Add("Spent", TimeSpan.Zero.GetType());
            ActivitiesSummaryTable = activitiesSummaryTable;
            this.taskCollection = taskCollection;
            Calculate();
        }

        public void Calculate()
        {
            dataTable.Clear();
            foreach(DataRow foreignRow in ActivitiesSummaryTable.Rows)
            {
                bool isUpdated = false;
                foreach (DataRow nativeRow in dataTable.Rows)
                {
                    if(nativeRow["Task"].Equals(foreignRow["Task"]))
                    {
                        nativeRow["Spent"] = (TimeSpan)nativeRow["Spent"] + (TimeSpan)foreignRow["Spent"];
                        isUpdated = true;
                    }
                    
                }
                if (!isUpdated)
                    dataTable.Rows.Add(foreignRow["Task"], foreignRow["Spent"]);
            }
        }

        private void sourceTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            Calculate();
        }
    }
}
