using NUnit.Framework;
using NMock2;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class AllTests: Mockery
    {
        static void Main(string[] args)
        {
        }
        [TearDown]
        public void TearDown()
        {
            this.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void TaskName()
        {
            Task task = new Task("task1");
            
            Assert.AreEqual("task1", task.Name);
        }
        [Test]
        public void TimeLogFolderDefaultSetting()
        {
            LazyCureSettings setting = new LazyCureSettings();
            Assert.AreEqual(@"d:\Programs\LazyCure\TimeLogs", setting.TimeLogsFolder);
        }
        [Test]
        public void SaveAfterDoneDefaultSetting()
        {
            LazyCureSettings setting = new LazyCureSettings();
            Assert.AreEqual(true, setting.SaveAfterDone);
        }
    }
}
