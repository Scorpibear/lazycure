using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    internal partial class TimeLogEditor : View,ITimeLogView
    {
        private ILazyCureDriver lazyCure;
        public TimeLogEditor(ILazyCureDriver lazyCure)
        {
            InitializeComponent();
            this.lazyCure = lazyCure;
            this.timeLogView.DataSource = lazyCure.TimeLogData;
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
            if (e.ColumnIndex!=timeLogView.Columns["Activity"].Index)
                ShowTimeNotValidMessage(timeLogView.Columns[e.ColumnIndex].Name);
        }
        private void ShowTimeNotValidMessage(string column)
        {
            MessageBox.Show(timeLogView,
                    "Please, enter correct time value between 0:00:00 and 23:59:59",
                    String.Format("Value in '{0}' column in not correct",column), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}