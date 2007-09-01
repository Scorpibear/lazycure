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
            this.activitiesSummary.DataSource = lazyCure.ActivitiesSummaryData;
            this.mainForm = mainForm;
        }

        private void Summary_VisibleChanged(object sender, System.EventArgs e)
        {
            if(mainForm!=null)
                mainForm.Summary_VisibleChanged();
        }
    }
}