using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Structures;
using LifeIdea.LazyCure.UI.Backend;

namespace LifeIdea.LazyCure.UI
{
    public partial class OathAuthorize : Dialog
    {
        ILazyCureDriver lazyCureDriver = null;
        TokensPair pair;

        public OathAuthorize(ILazyCureDriver lazyCureDriver)
        {
            this.lazyCureDriver = lazyCureDriver;
            InitializeComponent();
        }

        public TokensPair TokensPair { get { return pair; } }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var preCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            pair = lazyCureDriver.SetExternalPosterAuthorizationPin(this.textBoxPIN.Text);
            this.Cursor = preCursor;
        }


        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.dialog_buttonCancel_Click(sender, e);
        }


        private void textBoxPIN_TextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = !string.IsNullOrWhiteSpace(this.textBoxPIN.Text);
        }

        private void OathAuthorize_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.textBoxPIN.Clear();
                lazyCureDriver.AuthorizeInExternalPoster();
            }
        }
    }
}
