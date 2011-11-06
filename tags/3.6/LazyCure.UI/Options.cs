using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;
using System.Text;

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
            activitiesNumberInTray.Value = settings.ActivitiesNumberInTray;
            showsRecentActivities.Checked = settings.LeftClickOnTray;
            saveAfterDone.Checked = settings.SaveAfterDone;
            timeLogFolder.Text = settings.TimeLogsFolder;
            reminderTime.Text = Format.MaskedText(settings.ReminderTime);
            switchOnLogOff.Checked = settings.SwitchOnLogOff;
            enableTwitterCheckbox.Checked = settings.TwitterEnabled;
            usernameField.Text = settings.TwitterUsername;
            passwordField.Text = Format.Decode(settings.TwitterPassword);
            twitterLink.Links.Add(0, twitterLink.Text.Length, "http://twitter.com/");
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void enableTwitterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            bool twitterEnabled = enableTwitterCheckbox.Checked;
            usernameLabel.Enabled = twitterEnabled;
            usernameField.Enabled = twitterEnabled;
            passwordLabel.Enabled = twitterEnabled;
            passwordField.Enabled = twitterEnabled;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            TimeSpan parsedReminderTime;
            if (TimeSpan.TryParse(reminderTime.Text, out parsedReminderTime))
            {
                settings.ReminderTime = parsedReminderTime;
                settings.MaxActivitiesInHistory = (int)maxActivitiesInHistory.Value;
                settings.ActivitiesNumberInTray = (int)activitiesNumberInTray.Value;
                settings.LeftClickOnTray = showsRecentActivities.Checked;
                settings.SaveAfterDone = saveAfterDone.Checked;
                settings.TimeLogsFolder = timeLogFolder.Text;
                settings.SwitchOnLogOff = switchOnLogOff.Checked;
                settings.TwitterEnabled = enableTwitterCheckbox.Checked;
                settings.TwitterUsername = usernameField.Text;
                settings.TwitterPassword = Format.Encode(passwordField.Text);
                settings.Save();
                Dialogs.LazyCureDriver.ApplySettings(settings);
                Dialogs.MainForm.PostToTwitterEnabled = enableTwitterCheckbox.Checked;
                Hide();
            }
            else
                MessageBox.Show(String.Format("'{0}' is invalid reminder time. Please, correct it and press 'OK' then", reminderTime.Text), "Options could not be saved");
        }

        private void selectTimeLogsFolder_Click(object sender, EventArgs e)
        {
            timeLogFolderBrowser.SelectedPath = timeLogFolder.Text;
            DialogResult result = timeLogFolderBrowser.ShowDialog(this);
            if(result==DialogResult.OK)
                timeLogFolder.Text = timeLogFolderBrowser.SelectedPath;
        }

        private void twitterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;
            if (null != target)
            {
                System.Diagnostics.Process.Start(target);
            }

        }
    }
}