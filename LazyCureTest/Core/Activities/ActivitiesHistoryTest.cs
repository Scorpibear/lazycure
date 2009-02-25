using System.IO;
using System.Text;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Activities
{
    [TestFixture]
    public class ActivitiesHistoryTest: Mockery
    {
        private ActivitiesHistory history;

        [SetUp]
        public void SetUp()
        {
            history = new ActivitiesHistory();
            history.Size = 5;
        }
        [TearDown]
        public void TearDown()
        {
            this.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void EmptyLatestActivities()
        {
            Assert.IsEmpty(history.Activities);
        }
        [Test]
        public void OneActivity()
        {
            history.AddActivity("reading");
            
            Assert.Contains("reading", history.Activities);
        }
        [Test]
        public void TheSameActivity()
        {
            history.AddActivity("writing");
            history.AddActivity("writing");

            Assert.AreEqual(1, history.Activities.Length);
        }
        [Test]
        public void LIFO()
        {
            history.AddActivity("first");
            history.AddActivity("second");

            Assert.AreEqual(new string[] { "second", "first" }, history.Activities);
        }
        [Test]
        public void LastMoveToFirst()
        {
            history.AddActivity("one");
            history.AddActivity("two");
            history.AddActivity("one");

            Assert.AreEqual(new string[] { "one", "two" }, history.Activities);
        }
        [Test]
        public void Load()
        {
            File.CreateText("HistoryTest-Load.txt").Close();
            Assert.IsTrue(history.Load("HistoryTest-Load.txt"));
        }
        [Test]
        public void LoadMultiple()
        {
            history.Load(new StringReader("first\r\nsecond\r\n"));
            Assert.AreEqual(new string[] { "first", "second" }, history.Activities);
        }
        [Test]
        public void LoadDuplicates()
        {
            history.Load(new StringReader("duplicate\r\nduplicate\r\n"));
            Assert.AreEqual(1, history.Activities.Length);
        }
        [Test]
        public void LoadFromUnexistedPath()
        {
            Assert.IsFalse(history.Load("HistoryTest-notexistentfile.txt"));
        }
        [Test]
        public void Save()
        {
            Assert.IsTrue(history.Save("HistoryTest-Save.txt"));
        }
        [Test]
        public void SaveToUnexistedPath()
        {
            Assert.IsFalse(history.Save(@"m:\m\m.m"));
        }
        [Test]
        public void SaveAndLoad()
        {
            history.AddActivity("saved");
            history.AddActivity("saved2");
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            history.Save(writer);
            history = new ActivitiesHistory();
            history.Load(new StringReader(sb.ToString()));
            Assert.AreEqual(new string[] { "saved2", "saved" }, history.Activities);
        }
        [Test]
        public void LoadLimit()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 6;i++ )
                sb.AppendLine(i.ToString());
            history.Load(new StringReader(sb.ToString()));
            Assert.IsTrue(history.ContainsActivity("5"));
            Assert.IsFalse(history.ContainsActivity("6"));
        }
        [Test]
        public void LimitActivitiesWhileAdd()
        {
            for (int i = 5; i >= 1; i--)
                history.AddActivity(i.ToString());
            history.AddActivity("test");
            Assert.IsTrue(history.ContainsActivity("4"));
            Assert.IsFalse(history.ContainsActivity("5"));
        }
        [Test]
        public void GenerateUniqueActivityName()
        {
            Assert.AreEqual("activity1", history.UniqueName);
        }
        [Test]
        public void GenerateNextUnique()
        {
            history.AddActivity("activity1");
            Assert.AreEqual("activity2", history.UniqueName);
        }
        [Test]
        public void LatestActivities()
        {
            history.LatestSize = 1;
            history.AddActivity("a");
            history.AddActivity("b");
            Assert.AreEqual(new string[] { "b" }, history.LatestActivities);
        }
        [Test]
        public void AfterRenameDuplicatesAreRemoved()
        {
            history.AddActivity("a");
            history.AddActivity("b");
            history.RenameActivity("b", "a");
            Assert.AreEqual(1, history.Activities.Length);
        }
    }
}
