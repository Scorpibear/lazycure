using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    partial class TaskManager : View
    {
        private readonly ILazyCureDriver driver;
        const string NewTaskName = "New task...";

        public TaskManager(ILazyCureDriver driver, IMainForm mainForm)
        {
            InitializeComponent();
            this.driver = driver;
            this.mainForm = mainForm;
            treeView.Nodes.AddRange(driver.TasksNodes);
        }

        public string SelectedTask
        {
            get { return treeView.SelectedNode.Text; }
            set { treeView.SelectedNode = treeView.Nodes[value]; }
        }

        private void AddSibling()
        {
            TreeNode newNode;
            if (treeView.SelectedNode != null)
                newNode = treeView.Nodes.Insert(treeView.SelectedNode.Index+1, NewTaskName);
            else
                newNode = treeView.Nodes.Add(NewTaskName);
            newNode.BeginEdit();
        }

        private void DeleteTask()
        {
            DialogResult result = MessageBox.Show(this,
                "Do you really want to delete selected task '" + SelectedTask + "'?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                driver.RemoveTask(SelectedTask);
                treeView.SelectedNode.Remove();
            }
        }

        private void addSiblingButton_Click(object sender, EventArgs e)
        {
            AddSibling();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        private void isWorkingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            driver.UpdateIsWorkingTaskProperty(SelectedTask, isWorkingCheckBox.Checked);
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
                driver.UpdateTaskNodeText(e.Node, e.Label);
            treeView.SelectedNode = e.Node;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.isWorkingCheckBox.Checked = driver.IsWorkingTask(SelectedTask);
        }

        private void treeView_DoubleClick(object sender, EventArgs e)
        {
            Hide();
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    DeleteTask();
                    break;
                case Keys.Enter:
                    AddSibling();
                    break;
            }
        }
    }
}