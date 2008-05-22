using System;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI
{
    class View : Form
    {
        protected IMainForm mainForm = null;

        public View()
        {
            this.FormClosing += this.View_FormClosing;
            this.VisibleChanged += this.View_VisibleChanged;
        }
        protected void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
        protected virtual void View_VisibleChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.ViewsVisibilityChanged();
        }
    }
}
