using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI
{
    using Backend;
    public partial class HotKeysEditor : Form
    {
        public HotKeysEditor()
        {
            InitializeComponent();
            keysBox.Items.AddRange(GetKeysArray());
        }

        private static string[] GetKeysArray()
        {
            List<String> keys = new List<string>();
            for (int i = 1; i <= 12; i++)
                keys.Add("F" + i.ToString());
            for (int i = 0; i <= 9; i++)
                keys.Add(i.ToString());
            for (char ch = 'A'; ch <= 'Z'; ch++)
                keys.Add(ch.ToString());
            return keys.ToArray();
        }

        public string Keys { set {
            HotKey key = HotKey.Parse(value);
            ctrlCheckBox.Checked = key.Ctrl;
            altCheckBox.Checked = key.Alt;
            shiftCheckBox.Checked = key.Shift;
            keysBox.Text = key.JustKeyString;
        } get { return keysLabel.Text; } }

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