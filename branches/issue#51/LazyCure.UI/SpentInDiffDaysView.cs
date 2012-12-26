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

        public ComboBox.ObjectCollection ComboBoxItems { get { return this.activityOrTaskValueComboBox.Items; } }

        public string SelectedValue { get { return this.activityOrTaskValueComboBox.SelectedItem as string; } }

        public SpentInDiffDaysView(IHistoryDataProvider historyDataProvider, IMainForm mainForm)
        {
            this.historyDataProvider = historyDataProvider;
            this.mainForm = mainForm;
            InitializeComponent();
            if(historyDataProvider!=null)
                this.daySpentDataGrid.DataSource = this.historyDataProvider.Data;
        }

        public void LoadActivitiesOrTasksList()
        {
            this.activityOrTaskValueComboBox.Items.Clear();
            if (historyDataProvider != null)
                FillActivityOrTaskComboBox();
        }

        private void FillActivityOrTaskComboBox()
        {
            string[] entities = null;
            switch (ActiveEntityType)
            {
                case EntityType.Activity:
                    entities = historyDataProvider.HistoryActivities;
                    break;
                case EntityType.Task:
                    entities = historyDataProvider.Tasks;
                    break;
            }
            if (entities != null)
            {
                foreach (string entity in entities)
                    this.activityOrTaskValueComboBox.Items.Add(entity);
                this.activityOrTaskValueComboBox.SelectedIndex = 0;
            }
        }

        private void SpentOnDiffDays_VisibleChanged(object sender, System.EventArgs e)
        {
            if (this.Visible)
                LoadActivitiesOrTasksList();
        }

        private void closeButton_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void activityComboBox_SelectedValueChanged(object sender, System.EventArgs e)
        {
            LoadData();
        }

        string entityValue;

        public enum EntityType {Activity,Task}

        public EntityType ActiveEntityType {get{return (this.activityRadioButton.Checked) ? EntityType.Activity : EntityType.Task;}}

        private void LoadData()
        {
            this.daySpentDataGrid.Hide();
            var entityType = ActiveEntityType;
            entityValue = this.SelectedValue;
            //update in separate thread in order to not hangup UI
            new Thread(new ThreadStart(new Action(() => {
                switch (entityType)
                {
                    case EntityType.Activity:
                        this.historyDataProvider.UpdateDataTableForActivity(entityValue);
                        break;
                    case EntityType.Task:
                        this.historyDataProvider.UpdateDataTableForTask(entityValue);
                        break;
                }
                //need to do via Invoke to avoid multithreading issues
                this.Invoke(new Action(() =>
                {
                    this.daySpentDataGrid.Show();
                }));
            }))).Start();
        }

        private void daySpentDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Log.Error("issue for entity '{0}' in (row,column)({1},{2})", SelectedValue, e.RowIndex, e.ColumnIndex);
            Log.Exception(e.Exception);
        }

        private void daySpentDataGrid_BindingContextChanged(object sender, EventArgs e)
        {
            
        }

        private void activityRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            LoadActivitiesOrTasksList();
        }
    }
}
