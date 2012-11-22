using System;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI.Backend
{
    public class View : Dialog
    {
        protected IMainForm mainForm = null;

        public View()
        {
            this.VisibleChanged += this.View_VisibleChanged;
        }

        protected virtual void View_VisibleChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.ViewsVisibilityChanged();
        }
    }
}
