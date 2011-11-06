using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI
{
    using Backend.HotKeys;
    public partial class HotKeysEditor : Form
    {
        public HotKeysEditor()
        {
            InitializeComponent();
            keysBox.Items.AddRange(HotKeys.GetAllNames());
        }

        public string Keys
        {
            set
            {
                HotKey key = HotKey.Parse(value);
                ctrlCheckBox.Checked = key.Ctrl;
                altCheckBox.Checked = key.Alt;
                shiftCheckBox.Checked = key.Shift;
                keysBox.Text = key.JustKeyString;
            }
            get { return keysLabel.Text; }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            HotKey key = HotKey.Parse(keysBox.Text);
            key.Ctrl = ctrlCheckBox.Checked;
            key.Alt = altCheckBox.Checked;
            key.Shift = shiftCheckBox.Checked;
            keysLabel.Text = key.ToString();
        }
    }
}