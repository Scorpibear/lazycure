using System;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI.Backend
{
    public class Dialog : Form
    {
        public Dialog()
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

        protected void dialog_buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}