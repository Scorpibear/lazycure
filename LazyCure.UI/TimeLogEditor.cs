using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    internal partial class TimeLogEditor : View,ITimeLogView
    {
        private readonly ILazyCureDriver lazyCure;

        public TimeLogEditor(ILazyCureDriver lazyCure, IMainForm mainForm)
        {
            InitializeComponent();
            this.lazyCure = lazyCure;
            this.Data = lazyCure.TimeLogData;
            this.mainForm = mainForm;
        }
        
        public void CancelEdit()
        {
            timeLogView.CancelEdit();
        }

        public object Data
        {
            set { timeLogView.DataSource = value; }
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
                if (e.ColumnIndex != timeLogView.Columns["Activity"].Index)
                    ShowTimeNotValidMessage(timeLogView.Columns[e.ColumnIndex].Name);
        }
        
        private void ShowTimeNotValidMessage(string column)
        {
            ShowErrorMessage("Please, enter correct time value between 0:00:00 and 23:59:59",
                    String.Format("Value in '{0}' column is not correct",column));
        }
        
        private void ShowErrorMessage(string message, string header)
        {
            MessageBox.Show(timeLogView, message, header, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void View_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == false)
            {
                lazyCure.Save();
            }
            base.View_VisibleChanged(sender,e);
        }
   }
}