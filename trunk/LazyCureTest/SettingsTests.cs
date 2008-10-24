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
            AreEqual(30, Settings.Default.MaxActivitiesInHistory);
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
    }
}
