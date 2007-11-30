using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    public partial class Tasks : Form
    {
        private readonly ILazyCureDriver driver;
        const string NewTaskName = "New task...";

        public Tasks(ILazyCureDriver driver)
        {
            InitializeComponent();
            this.driver = driver;
            driver.FillTaskNodes(treeView.Nodes);
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
    }
}