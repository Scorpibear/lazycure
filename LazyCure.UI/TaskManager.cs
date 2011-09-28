using System;
using System.Drawing;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;
using LifeIdea.LazyCure.UI.Backend;

namespace LifeIdea.LazyCure.UI
{
    partial class TaskManager : View
    {
        readonly string NewTaskName = Constants.NewTask;

        private readonly ILazyCureDriver driver;
        private int minimalHeight;
        private TreeBinder treeBinder;

        public TaskManager(ILazyCureDriver driver, IMainForm mainForm)
        {
            InitializeComponent();
            this.driver = driver;
            this.mainForm = mainForm;
            treeBinder = new TreeBinder(driver.TaskViewDataSource);
            treeBinder.BindNodes(treeView);
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
            set { treeView.SelectedNode = treeBinder.GetTask(value); }
        }

        #region Private Methods

        private void AddSibling()
        {
            TreeNode newNode = treeBinder.AddSibling(treeView.SelectedNode);
            newNode.BeginEdit();
        }

        private void AddSubtask()
        {
            TreeNode newNode = treeBinder.AddSubtask(treeView.SelectedNode);
            newNode.BeginEdit();
        }

        private void DeleteTask()
        {
            DialogResult result = MessageBox.Show(this, String.Format(Constants.DeleteTaskConfirmationMessage, SelectedTask),
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                treeBinder.Remove(treeView.SelectedNode);
            treeView.Focus();
        }

        private void EditTask()
        {
            treeView.SelectedNode.BeginEdit();
        }

        private void ResizeToShowAllTasks()
        {
            int treeViewBordersHeight = treeView.Size.Height - treeView.ClientSize.Height;
            int newFormHeight = treeView.ItemHeight * NodesCountToShow() + treeViewBordersHeight;
            if (newFormHeight < minimalHeight)
                newFormHeight = minimalHeight;
            if (newFormHeight!=Height)
                ClientSize = new Size(Width, newFormHeight);
        }

        private int NodesCountToShow()
        {
            int count = 1 + GetChildrenNodesCount(treeView.Nodes);
            return count;
        }

        private static int GetChildrenNodesCount(TreeNodeCollection nodes)
        {
            int count = nodes.Count;
            foreach (TreeNode subNode in nodes)
                count += GetChildrenNodesCount(subNode.Nodes);
            return count;
        }

        #endregion Private Methods

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            AddSibling();
        }

        private void addSubtaskButton_Click(object sender, EventArgs e)
        {
            AddSubtask();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        private void isWorkingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            treeBinder.UpdateIsWorking(treeView.SelectedNode, isWorkingCheckBox.Checked);
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        private void TaskManager_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ResizeToShowAllTasks();
                treeView.ExpandAll();
            }
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
                treeBinder.Rename(e.Node, e.Label);
            treeView.SelectedNode = e.Node;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.isWorkingCheckBox.Checked = treeBinder.IsWorking(treeView.SelectedNode);
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
                case Keys.Insert:
                    AddSubtask();
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
