using LifeIdea.LazyCure.Properties;
using NUnit.Framework;

namespace LifeIdea.LazyCure
{
    [TestFixture]
    public class ProgramTests: Assert
    {
        static void Main(string[] args)
        {
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
            AreEqual(30,Settings.Default.MaxActivitiesInHistory);
        }
    }
}
