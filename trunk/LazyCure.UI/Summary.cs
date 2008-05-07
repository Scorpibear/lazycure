using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    partial class Summary : View,ISummaryView
    {
        private readonly ILazyCureDriver lazyCure;
        private readonly Timer timer = new Timer();

        public Summary(ILazyCureDriver lazyCure, IMainForm mainForm)
        {
            this.lazyCure = lazyCure;
            InitializeComponent();
            activitiesSummary.DataSource = lazyCure.ActivitiesSummaryData;
            tasksSummary.DataSource = lazyCure.TasksSummaryData;
            this.mainForm = mainForm;
            timer.Interval = 500;
            timer.Start();
            timer.Tick += UpdateStatistics;
            UpdateSelectedRowsTime();
        }

        private void UpdateStatistics(object sender, EventArgs e)
        {
            allActivitiesTime.Text = Format.ShortDuration(lazyCure.AllActivitiesTime);
            UpdateWorkingActivitiesTime();
            UpdateSelectedRowsTime();
        }

        private void activitiesSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsTaskCell(e))
            {
                Dialogs.TaskManager.SelectedTask = activitiesSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Dialogs.TaskManager.Location = Cursor.Position;
                Dialogs.TaskManager.ShowDialog(this);
                activitiesSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Dialogs.TaskManager.SelectedTask;
                UpdateWorkingActivitiesTime();
            }
        }

        private bool IsTaskCell(DataGridViewCellEventArgs e)
        {
            return (e.ColumnIndex == activitiesSummary.Columns[taskColumnForActivitySummary.Name].Index)
                   && (e.RowIndex > -1);
        }

        private void UpdateWorkingActivitiesTime()
        {
            workingActivitiesTime.Text = Format.ShortDuration(lazyCure.WorkingActivitiesTime);
        }

        private void UpdateSelectedRowsTime()
        {
            TimeSpan timeInSelectedRows;
            switch(tabControl.SelectedIndex)
            {
                case 0:
                    timeInSelectedRows = CalculateSelectedRowsTime(activitiesSummary,
                        spentColumnForActivitySummary.Name);
                    break;
                case 1:
                    timeInSelectedRows = CalculateSelectedRowsTime(tasksSummary,
                        spentColumnForTasksSummary.Name);
                    break;
                default:
                    timeInSelectedRows = TimeSpan.Zero;
                    break;
            }
            selectedRowsTime.Text = Format.ShortDuration(timeInSelectedRows);
        }

        private static TimeSpan CalculateSelectedRowsTime(DataGridView summaryTable, string durationColumnName)
        {
            TimeSpan timeInSelectedRows = new TimeSpan();
            List<int> selectedRows = new List<int>();
            foreach (DataGridViewRow row in summaryTable.SelectedRows)
            {
                if (!selectedRows.Contains(row.Index))
                    selectedRows.Add(row.Index);
            }
            foreach (DataGridViewCell cell in summaryTable.SelectedCells)
            {
                if (!selectedRows.Contains(cell.OwningRow.Index))
                    selectedRows.Add(cell.OwningRow.Index);
            }
            foreach (int rowIndex in selectedRows)
            {
                Object value = summaryTable.Rows[rowIndex].Cells[durationColumnName].Value;
                if (value != null)
                {
                    TimeSpan spent = (TimeSpan)value;
                    timeInSelectedRows += spent;
                }
            }
            return timeInSelectedRows;
        }
    }
}
