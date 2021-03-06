using NUnit.Framework;
using System;
using System.Drawing;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Properties
{
    [TestFixture]
    public class SettingsTests: Assert
    {
        [Test]
        public void DefaultLanguage()
        {
            AreEqual(string.Empty, Settings.Default.Language);
            AreSame(string.Empty, Settings.Default.Language);
        }
        [Test]
        public void TimeLogsFolderDefaultSetting()
        {
            AreEqual("TimeLogs", Settings.Default.TimeLogsFolder);
        }
        [Test]
        public void SaveAfterDoneDefaultSetting()
        {
            AreEqual(true, Settings.Default.SaveAfterDone);
        }
        [Test]
        public void MaxActivitiesInHistoryDefaultSetting()
        {
            AreEqual(LazyCure.Core.Activities.ActivitiesHistory.DEFAULT_SIZE, Settings.Default.MaxActivitiesInHistory);
        }
        [Test]
        public void NumberOfActivitiesAvailableFromTray()
        {
            AreEqual(LazyCure.Core.Activities.ActivitiesHistory.DEFAULT_LATEST_SIZE, Settings.Default.ActivitiesNumberInTray);
        }
        [Test]
        public void ReminderTimeDefaultSetting()
        {
            AreEqual(TimeSpan.Parse("1:00"), Settings.Default.ReminderTime);
        }
        [Test]
        public void MainWindowLocationDefaultSetting()
        {
            AreEqual(new Point(1280, 1024), Settings.Default.MainWindowLocation);
        }
        [Test]
        public void TwitterIsDisabledByDefault()
        {
            IsFalse(Settings.Default.TwitterEnabled);
        }
        [Test]
        public void TweetingActivityDefault()
        {
            AreEqual(string.Empty, Settings.Default.TweetingActivity);
        }
        [Test]
        public void TweetingActivityIsTheSameAsTweetByDefault()
        {
            IsFalse(Settings.Default.UseTweetingActivity);
        }
        [Test]
        public void SwitchOnLogOffIsTurnedOffByDefault()
        {
            IsFalse(Settings.Default.SwitchOnLogOff);
        }
        [Test]
        public void LeftClickOnTrayOpensMainWindowByDefault()
        {
            AreEqual(LeftClickOnTray.ShowsMainWindow, Settings.Default.LeftClickOnTray);
        }
        [Test]
        public void DefaultHotKeyToActivate()
        {
            AreEqual("Ctrl+F12", Settings.Default.HotKeyToActivate);
        }
        [Test]
        public void DefaultHotKeyToSwitch()
        {
            AreEqual("Ctrl+Alt+Shift+F12", Settings.Default.HotKeyToSwitch);
        }
        [Test]
        public void DefaultSwitchTimeLogAtMidnight()
        {
            IsTrue(Settings.Default.SwitchTimeLogAtMidnight);
        }
        [Test]
        public void SplitByCommaIsTurnedOffByDefault()
        {
            IsTrue(Settings.Default.SplitByComma);
        }
    }
}
