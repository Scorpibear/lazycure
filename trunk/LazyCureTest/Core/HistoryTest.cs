using NUnit.Framework;
using NMock2;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class HistoryTest: Mockery
    {
        private History history;
        [SetUp]
        public void SetUp()
        {
            history = new History();
        }
        [TearDown]
        public void TearDown()
        {
            this.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void EmptyLatestActivities()
        {
            Assert.IsEmpty(history.LatestActivities);
        }
        [Test]
        public void OneActivity()
        {
            history.AddActivity("reading");
            
            Assert.Contains("reading", history.LatestActivities);
        }
        [Test]
        public void TheSameActivity()
        {
            history.AddActivity("writing");
            history.AddActivity("writing");

            Assert.AreEqual(1, history.LatestActivities.Length);
        }
        [Test]
        public void LIFO()
        {
            history.AddActivity("first");
            history.AddActivity("second");

            Assert.AreEqual(new string[] { "second", "first" }, history.LatestActivities);
        }
        [Test]
        public void LastMoveToFirst()
        {
            history.AddActivity("one");
            history.AddActivity("two");
            history.AddActivity("one");

            Assert.AreEqual(new string[] { "one", "two" }, history.LatestActivities);
        }
        [Test]
        public void Load()
        {
            File.CreateText(@"c:\temp\load.txt").Close();
            Assert.IsTrue(history.Load(@"c:\temp\load.txt"));
            File.Delete(@"c:\temp\load.txt");
        }
        [Test]
        public void LoadMultiple()
        {
            StreamWriter writer = File.CreateText(@"c:\temp\history.txt");
            writer.WriteLine("first");
            writer.WriteLine("second");
            writer.Close();
            history.Load(@"c:\temp\history.txt");
            Assert.AreEqual(new string[] { "first", "second" }, history.LatestActivities);
        }
        [Test]
        public void LoadDuplicates()
        {
            StreamWriter writer = System.IO.File.CreateText(@"c:\temp\history.txt");
            writer.WriteLine("duplicate");
            writer.WriteLine("duplicate");
            writer.Close();
            history = new History();
            history.Load(@"c:\temp\history.txt");
            Assert.AreEqual(1, history.LatestActivities.Length);
        }
        [Test]
        public void LoadFromUnexistedPath()
        {
            Assert.IsFalse(history.Load(@"c:\temp\notexistedfile.txt"));
        }
        [Test]
        public void Save()
        {
            Assert.IsTrue(history.Save(@"c:\temp\history.txt"));
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
            history.Save(@"c:\temp\history.txt");
            history = new History();
            history.Load(@"c:\temp\history.txt");
            Assert.AreEqual(new string[] { "saved2", "saved" }, history.LatestActivities);
        }
    }
}
