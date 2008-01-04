using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    partial class Summary : View,ISummaryView
    {
        private ILazyCureDriver lazyCure;
        private readonly IMainForm mainForm;

        public Summary(ILazyCureDriver lazyCure, IMainForm mainForm)
        {
            this.lazyCure = lazyCure;
            InitializeComponent();
            activitiesSummary.DataSource = lazyCure.ActivitiesSummaryData;
            allActivitiesTime.Text = Format.ShortDuration(lazyCure.AllActivitiesTime);
            this.mainForm = mainForm;
        }

        private void Summary_VisibleChanged(object sender, System.EventArgs e)
        {
            if(mainForm!=null)
                mainForm.Summary_VisibleChanged();
        }

        private void timeUpdate_Click(object sender, EventArgs e)
        {
            allActivitiesTime.Text = Format.ShortDuration(lazyCure.AllActivitiesTime);
        }

        private void activitiesSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==activitiesSummary.Columns["Task"].Index)
            {
                Dialogs.Tasks.SelectedTask = activitiesSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Dialogs.Tasks.Location = Cursor.Position;
                Dialogs.Tasks.ShowDialog(this);
                activitiesSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Dialogs.Tasks.SelectedTask;
            }
        }
    }
}