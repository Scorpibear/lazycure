using System;
using NUnit.Framework;
using NMock2;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class AllTests
    {
        private Mockery mocks;
        static void Main(string[] args)
        {
        }
        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
        }
        [Test]
        public void StoreActivity()
        {
            Activity activity = new Activity("activity1");
            activity.StartTime = DateTime.Parse("2007-02-16 13:00:00");
            activity.Duration = TimeSpan.FromMinutes(15.0);
        }
        
        [Test]
        public void StopActivity()
        {
            Activity activity = new Activity("test activity");
            Assert.IsTrue(activity.IsRunning);
            activity.Stop();
            Assert.IsFalse(activity.IsRunning);
        }
        [Test]
        public void ActivityRecordsStartTime()
        {
            DateTime startTime = DateTime.Parse("2007-02-16 13:00:00");

            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));

            Activity activity = new Activity("test activity",mockTimeSystem);
            Assert.AreEqual(startTime, activity.StartTime);
        }
        [Test]
        public void TaskName()
        {
            Task task = new Task("task1");
            Assert.AreEqual("task1", task.Name);
        }
    }
}