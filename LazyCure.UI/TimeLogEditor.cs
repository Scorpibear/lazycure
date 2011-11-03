using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Constants;
using LifeIdea.LazyCure.Shared.Interfaces;

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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(timeLogView.GetClipboardContent());
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
            string columnName = this.timeLogView.Columns[e.ColumnIndex].HeaderText;
            if(e.ColumnIndex == 1)
                ShowErrorMessage(Constants.PleaseEnterNotEmptyActivityName,
                                String.Format(Constants.IncorrectValueInColumn, columnName));
            else
                ShowTimeNotValidMessage(columnName);
            e.Cancel = true;
        }

        private void ShowTimeNotValidMessage(string column)
        {
            ShowErrorMessage(Constants.InvalidTimeWarning,
                    String.Format(Constants.IncorrectValueInColumn, column));
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

        private void timeLogView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Hide();
        }
    }
}