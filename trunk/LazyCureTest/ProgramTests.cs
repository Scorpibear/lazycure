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
            AreEqual(@"d:\Programs\LazyCure\TimeLogs", LazyCureSettings.Default.TimeLogsFolder);
        }
        [Test]
        public void SaveAfterDoneDefaultSetting()
        {
            AreEqual(true, LazyCureSettings.Default.SaveAfterDone);
        }
        [Test]
        public void MaxActivitiesInHistoryDefaultSetting()
        {
            AreEqual(30,LazyCureSettings.Default.MaxActivitiesInHistory);
        }
    }
}
