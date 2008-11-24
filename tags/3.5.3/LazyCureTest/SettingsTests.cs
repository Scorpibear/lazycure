using NUnit.Framework;
using System;
using System.Drawing;

namespace LifeIdea.LazyCure.Properties
{
    [TestFixture]
    public class SettingsTests: Assert
    {
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
        public void SwitchOnLogOffIsTurnedOffByDefault()
        {
            IsFalse(Settings.Default.SwitchOnLogOff);
        }
    }
}
