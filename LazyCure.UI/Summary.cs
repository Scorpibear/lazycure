using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Constants;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;
using LifeIdea.LazyCure.UI.Backend;

namespace LifeIdea.LazyCure.UI
{
    public partial class Summary : Backend.View,ISummaryView
    {
        public enum Period{
            Today,
            Yesterday,
            ThisWeek,
            PrevWeek,
            LastMonth,
            Custom
        }

        #region Fields

        private Dictionary<Period, ToolStripButton> periodButtonMap;
        private Dictionary<ToolStripButton, Period> buttonPeriodMap;
        private readonly ILazyCureDriver lazyCure;
        private readonly Timer timer = new Timer();

        #endregion Fields

        #region Public Methods

        public Summary(ILazyCureDriver lazyCure, IMainForm mainForm)
        {
            InitializeComponent();
            InitializeDictionary();
            if (lazyCure != null)
            {
                this.lazyCure = lazyCure;
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

        public void ClickDayButton(Period period)
        {
            ToolStripButton button = periodButtonMap[period];
            button.Checked = !button.Checked;
        }

        public Period GetCheckedDayButtonPeriod()
        {
            foreach (var pair in buttonPeriodMap)
                if (pair.Key.Checked)
                    return pair.Value;
            return Period.Custom;
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
            foreach (var pair in buttonPeriodMap)
            {
                var button = pair.Key;
                var dates = GetFromAndToDatesText(pair.Value);
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

        private Tuple<string, string> GetFromAndToDatesText(Period period)
        {
            DateTime from = DateTime.Now, to = DateTime.Now;
            switch (period)
            {
                case Period.Yesterday:
                    from = to = from.AddDays(-1);
                    break;
                case Period.ThisWeek:
                    from = DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek);
                    to = from.AddDays(6);
                    break;
                case Period.PrevWeek:
                    from = DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek - 7);
                    to = from.AddDays(6);
                    break;
                case Period.LastMonth:
                    from = to.AddDays(-30);
                    break;
                default: // will show Today
                    break;
            }
            string fromText = Format.Date(from);
            string toText = Format.Date(to);
            return Tuple.Create(fromText, toText);
        }

        private void InitializeDictionary()
        {
            periodButtonMap = new Dictionary<Period, ToolStripButton>() {
                {Period.Today, todayButton},
                {Period.Yesterday, yesterdayButton},
                {Period.ThisWeek, thisWeekButton},
                {Period.PrevWeek, prevWeekButton},
                {Period.LastMonth, lastMonthButton}
            };
            buttonPeriodMap = periodButtonMap.ToDictionary(
                    kp => kp.Value, kp => kp.Key);
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
            Period period = this.GetCheckedDayButtonPeriod();
            var dates = this.GetFromAndToDatesText(period);
            
            //turn off handler in order to call it once
            this.fromDateDropDown.TextChanged -= dateDropDown_TextChanged;
            this.toDateDropDown.TextChanged -= dateDropDown_TextChanged;

            this.fromDateDropDown.Text = dates.Item1;
            this.toDateDropDown.Text = dates.Item2;

            //turn on handler back and call it once manually
            this.fromDateDropDown.TextChanged += dateDropDown_TextChanged;
            this.toDateDropDown.TextChanged += dateDropDown_TextChanged;
            dateDropDown_TextChanged(this, EventArgs.Empty);
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

        private void UpdateSummaryDataSources(IHistoryDataProvider dataProvider)
        {
            if (dataProvider != null)
            {
                activitiesSummary.DataSource = dataProvider.ActivitiesSummaryData;
                tasksSummary.DataSource = dataProvider.TasksSummaryData;
            }
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
                UpdateSummaryDataSources(lazyCure.HistoryDataProvider);
                this.activitiesSummary.Invalidate();
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

        private void dateDropDown_TextChanged(object sender, EventArgs e)
        {
            if (sender == this.fromDateDropDown && (DateTime.Parse(this.toDateDropDown.Text) < DateTime.Parse(this.fromDateDropDown.Text)))
                this.toDateDropDown.Text = this.fromDateDropDown.Text;
            if (sender == this.toDateDropDown && (DateTime.Parse(this.fromDateDropDown.Text) > DateTime.Parse(this.toDateDropDown.Text)))
                this.fromDateDropDown.Text = this.toDateDropDown.Text;
            if (lazyCure != null && lazyCure.HistoryDataProvider != null)
            {
                DateTime from = DateTime.Parse(this.fromDateDropDown.Text);
                DateTime to = DateTime.Parse(this.toDateDropDown.Text);
                lazyCure.HistoryDataProvider.SetSummaryPeriod(from, to);
                UpdateSummaryDataSources(lazyCure.HistoryDataProvider);
            }
        }

        private void dayButton_CheckedChanged(object sender, EventArgs e)
        {
            if (isDayButtonsCheckedChangedHandlerBlocked)
                return;
            isDayButtonsCheckedChangedHandlerBlocked = true;
            ToolStripButton currentButton = sender as ToolStripButton;
            if (currentButton.Checked)
            {
                foreach (ToolStripButton button in buttonPeriodMap.Keys)
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
