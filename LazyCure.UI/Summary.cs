using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Constants;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;
using LifeIdea.LazyCure.UI.Backend;

namespace LifeIdea.LazyCure.UI
{
    public partial class Summary : Backend.View,ISummaryView
    {
        #region Fields

        private ToolStripButton[] allDayButtons { get { return new ToolStripButton[] { lastMonthButton, prevWeekButton, thisWeekButton, yesterdayButton, todayButton }; } }
        private readonly ILazyCureDriver lazyCure;
        private readonly Timer timer = new Timer();

        #endregion Fields

        #region Public Methods

        public Summary(ILazyCureDriver lazyCure, IMainForm mainForm)
        {
            InitializeComponent();
            if (lazyCure != null)
            {
                this.lazyCure = lazyCure;
                if (lazyCure.HistoryDataProvider != null)
                {
                    activitiesSummary.DataSource = lazyCure.HistoryDataProvider.ActivitiesSummaryData;
                    tasksSummary.DataSource = lazyCure.HistoryDataProvider.TasksSummaryData;
                }
                workingTimeIntervalsGrid.DataSource = lazyCure.WorkingTimeIntervalsData;
                maxRestDurationTextBox.Text = Format.MaskedText(lazyCure.PossibleWorkInterruptionDuration);
            }
            this.mainForm = mainForm;
            timer.Interval = 300;
            timer.Start();
            timer.Tick += UpdateStatistics;
            UpdateSelectedRowsTime();
            LoadDateDropDownsValues();
            UpdateActiveDateDropDownsValue();
        }

        public void ClickDayButton(string text)
        {
            foreach (ToolStripButton button in allDayButtons)
                if (button.Text == text)
                    button.Checked = !button.Checked;
        }

        public string GetCheckedDayButtonsText()
        {
            string checkedButtonsText = "";
            foreach (ToolStripButton button in allDayButtons)
                checkedButtonsText += GetTextIfButtonIsChecked(button);
            return checkedButtonsText;
        }

        public string GetFromDate()
        {
            return this.fromDateDropDown.Text;
        }

        public string GetToDate()
        {
            return this.toDateDropDown.Text;
        }

        #endregion Public Methods

        #region Private Methods

        private void AlignCheckedButtonsWithDropDownsValues()
        {
            isDayButtonsCheckedChangedHandlerBlocked = true;
            foreach (var button in allDayButtons)
            {
                var dates = GetFromAndToDatesText(button.Text);
                if (dates.Item1 == this.fromDateDropDown.Text && dates.Item2 == this.toDateDropDown.Text)
                    button.Checked = true;
                else
                    button.Checked = false;
            }
            isDayButtonsCheckedChangedHandlerBlocked = false;
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

        private string GetTextIfButtonIsChecked(ToolStripButton button)
        {
            return (button.Checked) ? button.Text : string.Empty;
        }

        private bool IsTaskCell(DataGridViewCellEventArgs e)
        {
            return (e.ColumnIndex == activitiesSummary.Columns[taskColumnForActivitySummary.Name].Index)
                   && (e.RowIndex > -1);
        }

        private void LoadDateDropDownsValues()
        {
            if (lazyCure != null && lazyCure.HistoryDataProvider != null && lazyCure.HistoryDataProvider.TimeLogsManager != null)
            {
                var availableDays = lazyCure.HistoryDataProvider.TimeLogsManager.AvailableDays;
                foreach (DateTime day in availableDays)
                {
                    string formattedDate = Format.Date(day);
                    this.fromDateDropDown.DropDownItems.Add(formattedDate);
                    this.toDateDropDown.DropDownItems.Add(formattedDate);
                }
            }
        }

        private void UpdateActiveDateDropDownsValue()
        {
            string selectedPeriod = this.GetCheckedDayButtonsText();
            var dates = GetFromAndToDatesText(selectedPeriod);
            this.fromDateDropDown.Text = dates.Item1;
            this.toDateDropDown.Text = dates.Item2;
        }

        private Tuple<string, string> GetFromAndToDatesText(string selectedPeriod)
        {
            DateTime from = DateTime.Now, to = DateTime.Now;
            switch (selectedPeriod)
            {
                case "Yesterday":
                    from = to = from.AddDays(-1);
                    break;
                case "This Week":
                    from = DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek);
                    to = from.AddDays(6);
                    break;
                case "Prev Week":
                    from = DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek - 7);
                    to = from.AddDays(6);
                    break;
                case "Last Month":
                    from = to.AddDays(-30);
                    break;
                default: // will show Today
                    break;
            }
            string fromText = Format.Date(from);
            string toText = Format.Date(to);
            return Tuple.Create(fromText, toText);
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
            if (lazyCure != null && lazyCure.HistoryDataProvider != null)
            {
                allActivitiesTime.Text = Format.ShortDuration(lazyCure.HistoryDataProvider.AllActivitiesTime);
                UpdateWorkingActivitiesTime();
                UpdateSelectedRowsTime();
                efficiencyTextBox.Text = Format.Percent(lazyCure.Efficiency);
                timeOnWorkTextBox.Text = Format.ShortDuration(lazyCure.TimeOnWork);
            }
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

        private void activitiesSummary_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string after = activitiesSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            activitiesSummary.CancelEdit();
            string before = activitiesSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            lazyCure.RenameActivity(before, after);
        }

        private void automaticallyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            lazyCure.CalculateAutomaticallyWorkingIntervals = automaticallyRadioButton.Checked;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView focusedGrid = null;
            if (activitiesSummary.Focused)
                focusedGrid = activitiesSummary;
            if (tasksSummary.Focused)
                focusedGrid = tasksSummary;
            if(focusedGrid!=null)
                Clipboard.SetDataObject(focusedGrid.GetClipboardContent());
        }

        private void dateDropDown_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripDropDownButton dropDown = sender as ToolStripDropDownButton;
            dropDown.Text = e.ClickedItem.Text;
            AlignCheckedButtonsWithDropDownsValues();
        }

        bool isDayButtonsCheckedChangedHandlerBlocked = false;

        private void dayButton_CheckedChanged(object sender, EventArgs e)
        {
            if (isDayButtonsCheckedChangedHandlerBlocked)
                return;
            isDayButtonsCheckedChangedHandlerBlocked = true;
            ToolStripButton currentButton = sender as ToolStripButton;
            if (currentButton.Checked)
            {
                foreach (ToolStripButton button in allDayButtons)
                    if (button != currentButton)
                        button.Checked = false;
            }
            else
                currentButton.Checked = true;
            UpdateActiveDateDropDownsValue();
            isDayButtonsCheckedChangedHandlerBlocked = false;
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

        private void workingTimeIntervalsGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(this, Constants.InvalidTimeWarning, Constants.InvalidTimeValue);
        }

        #endregion Event Handlers
    }
}
