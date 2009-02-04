using System;
using LifeIdea.LazyCure.Core.Time;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Activities
{
    [TestFixture]
    public class RunningActivityTest
    {
        private readonly Mockery mocks = new Mockery();
        private readonly DateTime startTime = DateTime.Parse("2007-08-29 0:00:00");
        private ITimeSystem mockTimeSystem;
        private RunningActivity activity;
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
            activity = new RunningActivity("test activity",mockTimeSystem);
            Assert.IsTrue(activity.IsRunning);
            activity.Stop();
            Assert.IsFalse(activity.IsRunning);
        }
        [Test]
        public void ActivityRecordsStartTime()
        {
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
            activity = new RunningActivity("test activity", mockTimeSystem);
            Assert.AreEqual(startTime, activity.Start);
        }
        [Test]
        public void DurationMillisecondsAreTruncatedAfterStop()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:07:00.123456")));
            }
            activity = new RunningActivity("activity", mockTimeSystem);
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
            activity = new RunningActivity("activity", mockTimeSystem);
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
            activity = new RunningActivity("first",mockTimeSystem);
            activity.Stop();
            activity = RunningActivity.After(activity, "second");
            Assert.AreEqual(TimeSpan.Parse("0:00:00"),activity.Duration);
        }
        [Test]
        public void StartTimeIsRounded()
        {
            Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2111-11-11 5:00:00.5")));
            activity = new RunningActivity("activity", mockTimeSystem);
            Assert.AreEqual(DateTime.Parse("2111-11-11 5:00:01"), activity.Start);
        }
        [Test]
        public void StartTimeAtTheEndOfMinute()
        {
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("17:59:59.500")));
            
            activity = new RunningActivity("name",mockTimeSystem);
            
            Assert.AreEqual(DateTime.Parse("18:00:00"), activity.Start);
        }
    }
}
