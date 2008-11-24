using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;
using System.Collections.Generic;

namespace LifeIdea.LazyCure.UI
{
    internal partial class TimeLogEditor : View, ITimeLogView
    {
        private readonly ILazyCureDriver lazyCure;
        private List<int> timeColumnsIndeces = new List<int>();

        public TimeLogEditor(ILazyCureDriver lazyCure, IMainForm mainForm)
        {
            InitializeComponent();
            this.lazyCure = lazyCure;
            this.mainForm = mainForm;
            string[] timeColumnsNames = new string[] { "Start", "Duration", "End" };
            foreach (string columnName in timeColumnsNames)
                timeColumnsIndeces.Add(timeLogView.Columns[columnName].Index);
            Update();
        }

        public void CancelEdit()
        {
            timeLogView.CancelEdit();
        }

        public new void Update()
        {
            timeLogView.DataSource = lazyCure.TimeLogData;
            base.Update();
        }

        private void timeLogView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timeLogView.CurrentCell = timeLogView.CurrentRow.Cells["Start"];
            }
        }

        private void timeLogView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "Column 'Activity' does not allow nulls.")
            {
                ShowErrorMessage("Please, enter not empty activity name.",
                                "Value in 'Activity' column is not correct");
                e.Cancel = true;
            }
            else
                if (timeColumnsIndeces.Contains(e.ColumnIndex))
                    ShowTimeNotValidMessage(timeLogView.Columns[e.ColumnIndex].Name);
        }

        private void ShowTimeNotValidMessage(string column)
        {
            ShowErrorMessage("Please, enter correct time value between 0:00:00 and 23:59:59",
                    String.Format("Value in '{0}' column is not correct", column));
        }

        private void ShowErrorMessage(string message, string header)
        {
            MessageBox.Show(timeLogView, message, header, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void View_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == false)
                lazyCure.Save();
            else
                Update();
            base.View_VisibleChanged(sender, e);
        }

        private void timeLogView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            foreach (int columnIndex in timeColumnsIndeces)
                timeLogView.UpdateCellValue(columnIndex, e.RowIndex);
        }
    }
}