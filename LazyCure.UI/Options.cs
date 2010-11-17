using System;
using System.Text;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    public partial class Options : Form
    {
        private ISettings settings;
        private readonly FolderBrowserDialog timeLogFolderBrowser = new FolderBrowserDialog();

        public Options()
        {
            InitializeComponent();
            twitterLink.Links.Add(0, twitterLink.Text.Length, "http://twitter.com/");
            UpdateTwitterControlsEnabledProperty();
        }

        public Options(ISettings settings):this()
        {
            this.settings = settings;
            LoadSettings(settings);
        }

        #region Properties

        public ISettings Settings { set { this.settings = value; } }

        #endregion Properties

        #region Public methods

        public void UpdateSettings(TimeSpan parsedReminderTime)
        {
            settings.ActivitiesNumberInTray = (int)activitiesNumberInTray.Value;
            settings.HotKeyToActivate = hotKeyToActivateLabel.Text;
            settings.HotKeyToSwitch = hotKeyToSwitchLabel.Text;
            settings.LeftClickOnTray = showsRecentActivities.Checked;
            settings.MaxActivitiesInHistory = (int)maxActivitiesInHistory.Value;
            settings.ReminderTime = parsedReminderTime;
            settings.SaveAfterDone = saveAfterDone.Checked;
            settings.SplitByComma = splitByComma.Checked;
            settings.SwitchOnLogOff = switchOnLogOff.Checked;
            settings.SwitchTimeLogAtMidnight = switchTimeLogAtMidnight.Checked;
            settings.TimeLogsFolder = timeLogFolder.Text;
            settings.TweetingActivity = twitterActivityField.Text;
            settings.UseTweetingActivity = twitterActivitySpecificRadioButton.Checked;
            settings.TwitterEnabled = enableTwitterCheckbox.Checked;
            settings.TwitterPassword = Format.Encode(passwordField.Text);
            settings.TwitterUsername = usernameField.Text;
        }

        #endregion Public methods

        #region Private methods

        private void ChangeTwitterControlsEnabled(bool enabled)
        {
            Control[] twitterControls = new Control[] { usernameLabel, usernameField, passwordLabel, passwordField, whatAddtj};
            foreach (Control control in twitterControls)
                control.Enabled = enabled;
        }

        private void EditHotKeyLabel(Label hotKeyLabel)
        {
            HotKeysEditor keysEditor = new HotKeysEditor();
            keysEditor.Keys = hotKeyLabel.Text;
            DialogResult result = keysEditor.ShowDialog(this);
            if (result == DialogResult.OK)
                hotKeyLabel.Text = keysEditor.Keys;
        }

        private void LoadSettings(ISettings settings)
        {
            activitiesNumberInTray.Value = settings.ActivitiesNumberInTray;
            hotKeyToActivateLabel.Text = settings.HotKeyToActivate;
            hotKeyToSwitchLabel.Text = settings.HotKeyToSwitch;
            showsRecentActivities.Checked = settings.LeftClickOnTray;
            maxActivitiesInHistory.Value = settings.MaxActivitiesInHistory;
            SetReminderTime(settings.ReminderTime);
            saveAfterDone.Checked = settings.SaveAfterDone;
            splitByComma.Checked = settings.SplitByComma;
            switchOnLogOff.Checked = settings.SwitchOnLogOff;
            switchTimeLogAtMidnight.Checked = settings.SwitchTimeLogAtMidnight;
            timeLogFolder.Text = settings.TimeLogsFolder;
            enableTwitterCheckbox.Checked = settings.TwitterEnabled;
            twitterActivityField.Text = settings.TweetingActivity;
            twitterActivityTheSameRadioButton.Checked = !(twitterActivitySpecificRadioButton.Checked = settings.UseTweetingActivity);
            usernameField.Text = settings.TwitterUsername;
            passwordField.Text = Format.Decode(settings.TwitterPassword);
        }

        private void SetReminderTime(TimeSpan newTime)
        {
            this.reminderTime.ValidatingType = newTime.GetType();
            this.reminderTime.Text = Format.MaskedText(newTime);
        }

        private void UpdateTwitterControlsEnabledProperty()
        {
            bool twitterEnabled = enableTwitterCheckbox.Checked;
            ChangeTwitterControlsEnabled(twitterEnabled);
        }
        
        #endregion

        #region Options form event handlers

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        #endregion

        #region Options controls event handlers

        private void cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void editActivateKey_Click(object sender, EventArgs e)
        {
            EditHotKeyLabel(hotKeyToActivateLabel);
        }

        private void editSwitchKey_Click(object sender, EventArgs e)
        {
            EditHotKeyLabel(hotKeyToSwitchLabel);
        }

        private void enableTwitterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTwitterControlsEnabledProperty();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            TimeSpan parsedReminderTime;
            if (TimeSpan.TryParse(reminderTime.Text, out parsedReminderTime))
            {
                UpdateSettings(parsedReminderTime);
                settings.Save();
                Dialogs.LazyCureDriver.ApplySettings(settings);
                Dialogs.MainForm.PostToTwitterEnabled = enableTwitterCheckbox.Checked;
                Dialogs.MainForm.RegisterHotKeys();
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

        #endregion Options controls event handlers
    }
}
