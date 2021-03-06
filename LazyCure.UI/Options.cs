﻿using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Constants;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;
using LifeIdea.LazyCure.UI.Backend;

namespace LifeIdea.LazyCure.UI
{
    public partial class Options : Dialog
    {
        private ISettings settings;
        private readonly FolderBrowserDialog timeLogFolderBrowser = new FolderBrowserDialog();

        public Options()
        {
            InitializeComponent();
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
            UpdateLanguage(LanguageOption);
            settings.LeftClickOnTray = showsRecentActivities.Checked;
            settings.MaxActivitiesInHistory = (int)maxActivitiesInHistory.Value;
            settings.ReminderTime = parsedReminderTime;
            settings.SaveAfterDone = saveAfterDone.Checked;
            settings.SplitByComma = splitByComma.Checked;
            settings.SwitchOnLogOff = switchOnLogOff.Checked;
            settings.SwitchTimeLogAtMidnight = switchTimeLogAtMidnight.Checked;
            settings.TimeLogsFolder = timeLogFolder.Text;
        }

        #endregion Public methods

        #region Private methods

        private void EditHotKeyLabel(Label hotKeyLabel)
        {
            HotKeysEditor keysEditor = new HotKeysEditor();
            keysEditor.Keys = hotKeyLabel.Text;
            DialogResult result = keysEditor.ShowDialog(this);
            if (result == DialogResult.OK)
                hotKeyLabel.Text = keysEditor.Keys;
        }

        const string russianLanguageCode = "ru";
        const string englishLanguageCode = "en";
        const string ukrainianLanguageCode = "uk";
        const string spanishLanguageCode = "es";
        const string belarusianLanguageCode = "be";
        
        private string LanguageOption
        {
            get
            {
                return
                    (languageOptionRussian.Checked) ? russianLanguageCode : (
                    (languageOptionUkrainian.Checked) ? ukrainianLanguageCode : (
                    (languageOptionSpanish.Checked) ? spanishLanguageCode : (
                    (languageOptionBelarusian.Checked) ? belarusianLanguageCode : (
                    englishLanguageCode))));
            }
            set
            {
                switch (value)
                {
                    case russianLanguageCode:
                        languageOptionRussian.Checked = true; break;
                    case ukrainianLanguageCode:
                        languageOptionUkrainian.Checked = true; break;
                    case spanishLanguageCode:
                        languageOptionSpanish.Checked = true; break;
                    case belarusianLanguageCode:
                        languageOptionBelarusian.Checked = true; break;
                    default:
                        languageOptionEnglish.Checked = true; break;
                }
            }
        }

        private void LoadSettings(ISettings settings)
        {
            activitiesNumberInTray.Value = settings.ActivitiesNumberInTray;
            hotKeyToActivateLabel.Text = settings.HotKeyToActivate;
            hotKeyToSwitchLabel.Text = settings.HotKeyToSwitch;
            LanguageOption = settings.Language;
            showsRecentActivities.Checked = settings.LeftClickOnTray;
            maxActivitiesInHistory.Value = settings.MaxActivitiesInHistory;
            SetReminderTime(settings.ReminderTime);
            saveAfterDone.Checked = settings.SaveAfterDone;
            splitByComma.Checked = settings.SplitByComma;
            switchOnLogOff.Checked = settings.SwitchOnLogOff;
            switchTimeLogAtMidnight.Checked = settings.SwitchTimeLogAtMidnight;
            timeLogFolder.Text = settings.TimeLogsFolder;
        }

        private void SetReminderTime(TimeSpan newTime)
        {
            this.reminderTime.ValidatingType = newTime.GetType();
            this.reminderTime.Text = Format.MaskedText(newTime);
        }

        public void UpdateLanguage(string language)
        {
            if(settings!=null)
                settings.Language = language;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            Dialogs.Reset();
        }

        #endregion

        #region Options controls event handlers

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.dialog_buttonCancel_Click(sender, e);
        }

        private void editActivateKey_Click(object sender, EventArgs e)
        {
            EditHotKeyLabel(hotKeyToActivateLabel);
        }

        private void editSwitchKey_Click(object sender, EventArgs e)
        {
            EditHotKeyLabel(hotKeyToSwitchLabel);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            TimeSpan parsedReminderTime;
            if (TimeSpan.TryParse(reminderTime.Text, out parsedReminderTime))
            {
                CultureInfo previousUICulture = Thread.CurrentThread.CurrentUICulture;
                UpdateSettings(parsedReminderTime);
                CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
                settings.Save();
                Dialogs.LazyCureDriver.ApplySettings(settings);
                Dialogs.MainForm.RegisterHotKeys();
                Hide();
                if (!previousUICulture.Equals(currentUICulture))
                    NotifyAboutLanguageApplyAfterRestart();
            }
            else
                MessageBox.Show(String.Format(Constants.InvalidReminderTime, reminderTime.Text), Constants.IncorrectSettings);
        }

        private void NotifyAboutLanguageApplyAfterRestart()
        {
            MessageBox.Show(this,
                "Language settings will be applied after LazyCure restart.\r\n"+ //en
                "Язык интерфейса будет изменён при следующей загрузке LazyCure.\r\n"+ //ru
                "Мову інтерфейсу буде змінено при наступному завантаженні LazyCure.\r\n"+ //uk
                "La configuración de idioma se aplicará tras reiniciar LazyCure.\r\n" + //es
                "Мова iнтерфейса будзе зменяна пры наступнай загрузцы LazyCure." //be
                , Constants.LanguageChange, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
