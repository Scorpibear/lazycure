using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    public partial class Tasks : Form
    {
        private readonly ILazyCureDriver driver;        

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
    }
}