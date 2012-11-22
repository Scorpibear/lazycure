using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Interfaces;
using System;
using System.Threading;
using LifeIdea.LazyCure.UI.Backend;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.UI
{
    public partial class SpentInDiffDaysView : Backend.View, ISpentOnDiffDaysView
    {
        private IHistoryDataProvider historyDataProvider;

        public ComboBox.ObjectCollection ComboBoxItems { get { return this.activityComboBox.Items; } }

        public string SelectedActivity { get { return this.activityComboBox.SelectedItem as string; } }

        public SpentInDiffDaysView(IHistoryDataProvider historyDataProvider, IMainForm mainForm)
        {
            this.historyDataProvider = historyDataProvider;
            this.mainForm = mainForm;
            InitializeComponent();
            if(historyDataProvider!=null)
                this.daySpentDataGrid.DataSource = this.historyDataProvider.SpentOnDiffDaysDataTable;
        }

        public void LoadActivitiesList()
        {
            this.activityComboBox.Items.Clear();
            if (historyDataProvider != null)
                FillActivityComboBox();
        }

        private void FillActivityComboBox()
        {
            string[] activities = historyDataProvider.HistoryActivities;
            if (activities != null)
            {
                foreach (string activity in activities)
                    this.activityComboBox.Items.Add(activity);
                this.activityComboBox.SelectedIndex = 0;
            }
        }

        private void SpentOnDiffDays_VisibleChanged(object sender, System.EventArgs e)
        {
            if (this.Visible)
                LoadActivitiesList();
        }

        private void closeButton_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void activityComboBox_SelectedValueChanged(object sender, System.EventArgs e)
        {
            LoadData();
        }

        string activityName;

        private void LoadData()
        {
            this.daySpentDataGrid.Hide();
            activityName = this.SelectedActivity;
            //update in separate thread in order to not hangup UI
            new Thread(new ThreadStart(new Action(() => {
                this.historyDataProvider.UpdateDataTableForActivity(activityName);
                //need to do via Invoke to avoid multithreading issues
                this.Invoke(new Action(() =>
                {
                    this.daySpentDataGrid.Show();
                }));
            }))).Start();
        }

        private void daySpentDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Log.Error("issue for activity '{0}' in (row,column)({1},{2})", SelectedActivity, e.RowIndex, e.ColumnIndex);
            Log.Exception(e.Exception);
        }

        private void daySpentDataGrid_BindingContextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
