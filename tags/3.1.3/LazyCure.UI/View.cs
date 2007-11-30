using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI
{
    class View : Form
    {
        public View()
        {
            this.FormClosing += this.View_FormClosing;
        }
        protected void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
    }
}
