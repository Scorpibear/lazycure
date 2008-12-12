using System;
using System.Drawing;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    partial class TaskManager : View
    {
        const string NewTaskName = "New task...";
        private readonly ILazyCureDriver driver;
        private int minimalHeight;

        public TaskManager(ILazyCureDriver driver, IMainForm mainForm)
        {
            InitializeComponent();
            this.driver = driver;
            this.mainForm = mainForm;
            treeView.Nodes.AddRange(driver.TasksNodes);
            minimalHeight = ClientSize.Height;
        }

        public string SelectedTask
        {
            get
            {
                TreeNode selected = treeView.SelectedNode;
                if (selected != null)
                    return selected.Text;
                else
                    return null;
            }
            set { treeView.SelectedNode = treeView.Nodes[value]; }
        }

        #region Private Methods

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

        private void EditTask()
        {
            treeView.SelectedNode.BeginEdit();
        }

        private void ResizeToShowAllTasks()
        {
            int treeViewBordersHeight = treeView.Size.Height - treeView.ClientSize.Height;
            int newFormHeight = treeView.ItemHeight * (treeView.Nodes.Count + 1) + treeViewBordersHeight;
            if (newFormHeight < minimalHeight)
                newFormHeight = minimalHeight;
            if (newFormHeight!=Height)
                ClientSize = new Size(Width, newFormHeight);
        }

        #endregion Private Methods

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

        private void miRename_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        private void TaskManager_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                ResizeToShowAllTasks();
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
                case Keys.F2:
                    EditTask();
                    break;
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView.SelectedNode = e.Node;
        }
    }
}