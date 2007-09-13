using System;
using NUnit.Framework;
using NMock2;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class LiveActivityTest
    {
        private readonly Mockery mocks = new Mockery();
        private readonly DateTime startTime = DateTime.Parse("2007-08-29 0:00:00");
        private ITimeSystem mockTimeSystem;
        private LiveActivity activity;
        [SetUp]
        public void SetUp()
        {
            mockTimeSystem = mocks.NewMock<ITimeSystem>();
        }
        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void StopActivity()
        {
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
            activity = new LiveActivity("test activity",mockTimeSystem);
            Assert.IsTrue(activity.IsRunning);
            activity.Stop();
            Assert.IsFalse(activity.IsRunning);
        }
        [Test]
        public void ActivityRecordsStartTime()
        {
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
            activity = new LiveActivity("test activity", mockTimeSystem);
            Assert.AreEqual(startTime, activity.StartTime);
        }
        [Test]
        public void DurationMillisecondsAreTruncatedAfterStop()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:07:00.123456")));
            }
            activity = new LiveActivity("activity", mockTimeSystem);
            activity.Stop();
            Assert.AreEqual(TimeSpan.Parse("0:07:00"), activity.Duration);
        }
        [Test]
        public void MillisecondsAreRounded()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:07:00.5")));
            }
            activity = new LiveActivity("activity", mockTimeSystem);
            activity.Stop();
            Assert.AreEqual(TimeSpan.Parse("0:07:01"), activity.Duration);
        }
        [Test]
        public void ThereIsNoNegativeDuration()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(
                    Return.Value(DateTime.Parse("2111-11-11 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(
                    Return.Value(DateTime.Parse("2111-11-11 5:00:00.6")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(
                    Return.Value(DateTime.Parse("2111-11-11 5:00:00.7")));
            }
            activity = new LiveActivity("first",mockTimeSystem);
            activity.Stop();
            activity = LiveActivity.After(activity, "second");
            Assert.AreEqual(TimeSpan.Parse("0:00:00"),activity.Duration);
        }
        [Test]
        public void StartTimeIsRounded()
        {
            Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2111-11-11 5:00:00.5")));
            activity = new LiveActivity("activity", mockTimeSystem);
            Assert.AreEqual(DateTime.Parse("2111-11-11 5:00:01"), activity.StartTime);
        }
    }
}
