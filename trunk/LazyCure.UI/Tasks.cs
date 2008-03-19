using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    partial class Tasks : View
    {
        private readonly ILazyCureDriver driver;
        const string NewTaskName = "New task...";

        public Tasks(ILazyCureDriver driver,IMainForm mainForm)
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

        private void treeView_DoubleClick(object sender, System.EventArgs e)
        {
            Hide();
        }

        private void addSibling_Click(object sender, System.EventArgs e)
        {
            TreeNode newNode = treeView.Nodes.Add(NewTaskName);
            newNode.BeginEdit();
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            driver.UpdateTaskNodeText(e.Node, e.Label);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.isWorkingCheckBox.Checked = driver.IsWorkingTask(SelectedTask);
        }

        private void isWorkingCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            driver.UpdateIsWorkingTaskProperty(SelectedTask, isWorkingCheckBox.Checked);
        }

    }
}