using System;
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
        }

        private void UpdateStatistics(object sender, EventArgs e)
        {
            allActivitiesTime.Text = Format.ShortDuration(lazyCure.AllActivitiesTime);
            UpdateWorkingActivitiesTime();
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
    }
}
