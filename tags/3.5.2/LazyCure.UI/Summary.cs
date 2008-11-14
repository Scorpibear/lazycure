using System;
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
            workingTimeIntervalsGrid.DataSource = lazyCure.WorkingTimeIntervalsData;
            maxRestDurationTextBox.Text = Format.ShortDuration(lazyCure.PossibleWorkInterruptionDuration);
            this.mainForm = mainForm;
            timer.Interval = 300;
            timer.Start();
            timer.Tick += UpdateStatistics;
            UpdateSelectedRowsTime();
        }

        #region Private Methods

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

        private bool IsTaskCell(DataGridViewCellEventArgs e)
        {
            return (e.ColumnIndex == activitiesSummary.Columns[taskColumnForActivitySummary.Name].Index)
                   && (e.RowIndex > -1);
        }

        private void UpdateSelectedRowsTime()
        {
            TimeSpan timeInSelectedRows;
            switch (tabControl.SelectedIndex)
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

        private void UpdateWorkingActivitiesTime()
        {
            workingActivitiesTime.Text = Format.ShortDuration(lazyCure.WorkingActivitiesTime);
        }

        #endregion Private Methods

        #region Event Handlers

        private void UpdateStatistics(object sender, EventArgs e)
        {
            allActivitiesTime.Text = Format.ShortDuration(lazyCure.AllActivitiesTime);
            UpdateWorkingActivitiesTime();
            UpdateSelectedRowsTime();
            efficiencyTextBox.Text = Format.Percent(lazyCure.Efficiency);
            timeOnWorkTextBox.Text = Format.ShortDuration(lazyCure.TimeOnWork);
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

        private void automaticallyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            lazyCure.CalculateAutomaticallyWorkingIntervals = automaticallyRadioButton.Checked;
        }

        private void efficiencyTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void maxRestDurationTextBox_TextChanged(object sender, EventArgs e)
        {
            lazyCure.PossibleWorkInterruptionDuration = Format.Duration(maxRestDurationTextBox.Text);
            automaticallyRadioButton.Checked = true;
        }

        private void showTimeLogButton_Click(object sender, EventArgs e)
        {
            Dialogs.TimeLog.Show();
        }

        private void workingTimeIntervalsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            manuallyRadioButton.Checked = true;
        }
        #endregion Event Handlers
    }
}
