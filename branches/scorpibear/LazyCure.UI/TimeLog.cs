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
    internal partial class TimeLog : View,ITimeLogView
    {
        private ILazyCureDriver lazyCure;
        public TimeLog(ILazyCureDriver lazyCure)
        {
            InitializeComponent();
            this.lazyCure = lazyCure;
        }

        public void UpdateData()
        {
            IActivity activity = lazyCure.PreviousActivity;
            timeLogView.Rows.Add(Format.Time(activity.StartTime), activity.Name, Format.Duration(activity.Duration), Format.Time(activity.StartTime + activity.Duration));
        }

        private void timeLogView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (((e.ColumnIndex == 0) || (e.ColumnIndex == 3)) && timeLogView.IsCurrentCellInEditMode)
            {
                string str = e.FormattedValue.ToString();
                if (str != String.Empty)
                {
                    try
                    {
                        DateTime.Parse(str);
                    }
                    catch (FormatException)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    MessageBox.Show(timeLogView, "'Start Time' column could not be empty. Please, enter correct time value between 0:00:00 and 23:59:59.", "Value in 'Start Time' column in not correct");
                    e.Cancel = true;
                    return;
                }
                e.Cancel = false;
            }
       }

        private void timeLogView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (timeLogView.Rows[e.RowIndex].Cells["Duration"].Value != null)
                    {
                        DateTime startTime = Format.Time(timeLogView.Rows[e.RowIndex].Cells["Start"].Value);
                        TimeSpan duration = Format.Duration(timeLogView.Rows[e.RowIndex].Cells["Duration"].Value);
                        DateTime endTime = startTime + duration;
                        timeLogView.Rows[e.RowIndex].Cells["End"].Value = Format.Time(endTime);
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    DateTime startTime;
                    if (DateTime.TryParse(timeLogView.Rows[e.RowIndex].Cells["Start"].Value.ToString(), out startTime))
                    {
                        TimeSpan duration = Format.Duration(timeLogView.Rows[e.RowIndex].Cells["Duration"].Value);
                        DateTime endTime = startTime + duration;
                        timeLogView.Rows[e.RowIndex].Cells["End"].Value = Format.Time(endTime);
                    }
                    timeLogView.CurrentCell.Value = Format.Duration(Format.Duration(timeLogView.CurrentCell.Value));
                }
                if (e.ColumnIndex == 3)
                {
                    if (timeLogView.Rows[e.RowIndex].Cells[0].Value.ToString() != String.Empty)
                    {
                        DateTime startTime = Format.Time(timeLogView.Rows[e.RowIndex].Cells[0].Value);
                        DateTime endTime = Format.Time(timeLogView.Rows[e.RowIndex].Cells[3].Value);
                        TimeSpan duration = endTime - startTime;
                        timeLogView.Rows[e.RowIndex].Cells[2].Value = Format.Duration(duration);
                    }
                }
                if ((e.ColumnIndex == 0) || (e.ColumnIndex == 3))
                {
                    timeLogView.CurrentCell.Value = Format.Time(Format.Time(timeLogView.CurrentCell.Value));
                }
            }
        }

        private void timeLogView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timeLogView.CurrentCell = timeLogView.CurrentRow.Cells[0];
            }
        }
    }
}