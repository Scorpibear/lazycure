using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    internal partial class TimeLogEditor : View,ITimeLogView
    {
        private ILazyCureDriver lazyCure;
        private IMainForm mainForm;

        public TimeLogEditor(ILazyCureDriver lazyCure, IMainForm mainForm)
        {
            InitializeComponent();
            this.lazyCure = lazyCure;
            timeLogView.DataSource = lazyCure.TimeLogData;
            this.mainForm = mainForm;
        }
        public void CancelEdit()
        {
            timeLogView.CancelEdit();
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
            if (e.ColumnIndex != timeLogView.Columns["Activity"].Index)
                ShowTimeNotValidMessage(timeLogView.Columns[e.ColumnIndex].Name);
        }
        private void ShowTimeNotValidMessage(string column)
        {
            MessageBox.Show(timeLogView,
                    "Please, enter correct time value between 0:00:00 and 23:59:59",
                    String.Format("Value in '{0}' column is not correct",column), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void TimeLogEditor_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == false)
            {
                lazyCure.SaveTimeLog();
            }
            if(mainForm!=null)
                mainForm.TimeLogEditor_VisibleChanged();
        }
   }
}