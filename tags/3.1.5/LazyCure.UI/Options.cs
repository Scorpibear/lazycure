using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    public partial class Options : Form
    {
        private readonly ISettings settings;
        private readonly FolderBrowserDialog timeLogFolderBrowser = new FolderBrowserDialog();

        public Options(ISettings settings)
        {
            InitializeComponent();
            reminderTime.ValidatingType = settings.ReminderTime.GetType();
            this.settings = settings;
            maxActivitiesInHistory.Value = settings.MaxActivitiesInHistory;
            saveAfterDone.Checked = settings.SaveAfterDone;
            timeLogFolder.Text = settings.TimeLogsFolder;
            reminderTime.Text = settings.ReminderTime.ToString();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            settings.MaxActivitiesInHistory = (int) maxActivitiesInHistory.Value;
            settings.SaveAfterDone = saveAfterDone.Checked;
            settings.TimeLogsFolder = timeLogFolder.Text;
            settings.ReminderTime = (TimeSpan) reminderTime.ValidateText();
            settings.Save();
            Dialogs.LazyCureDriver.ApplySettings(settings);
            Hide();
        }

        private void selectTimeLogsFolder_Click(object sender, EventArgs e)
        {
            timeLogFolderBrowser.SelectedPath = timeLogFolder.Text;
            DialogResult result = timeLogFolderBrowser.ShowDialog(this);
            if(result==System.Windows.Forms.DialogResult.OK)
                timeLogFolder.Text = timeLogFolderBrowser.SelectedPath;
        }
    }
}