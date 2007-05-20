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
        [Test]
        public void ActivityDurationAfterStop()
        {
            DateTime startTime = DateTime.Parse("2007-08-29 0:00:00");
            DateTime endTime = DateTime.Parse("2007-08-29 12:34:56");
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(endTime));
            }
            Activity activity = new Activity("activity",mockTimeSystem);
            activity.Stop();
            Assert.AreEqual(TimeSpan.Parse("12:34:56"), activity.Duration);
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void MillisecondsAreTruncated()
        {
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:07:00.123456")));
            }
            Activity activity = new Activity("activity", mockTimeSystem);
            activity.Stop();
            Assert.AreEqual(TimeSpan.Parse("0:07:00"), activity.Duration);
        }
        [Test]
        public void MillisecondsAreRounded()
        {
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:07:00.5")));
            }
            Activity activity = new Activity("activity", mockTimeSystem);
            activity.Stop();
            Assert.AreEqual(TimeSpan.Parse("0:07:01"), activity.Duration);
        }
        [Test]
        public void LogTest()
        {
            Exception ex = new Exception("message");
            ex.Source = "LogTest";
            
            Log.Writer = mocks.NewMock<IWriter>();
            using (mocks.Ordered)
            {
                Expect.Once.On(Log.Writer).Method("WriteLine").With(ex.Message);
                Expect.Once.On(Log.Writer).Method("WriteLine").With(ex.Source);
            }
            Log.Exception(ex);
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
        
    }
}
