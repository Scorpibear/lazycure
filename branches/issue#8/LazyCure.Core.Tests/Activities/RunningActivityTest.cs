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
        public void SplitNames()
        {
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
            activity = new RunningActivity("first,second", mockTimeSystem);
            activity.Stop();
            RunningActivity second = activity.SplitByComma()[1];
            Assert.AreEqual("first", activity.Name);
            Assert.AreEqual("second", second.Name);
        }
        [Test]
        public void SplitTime()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("7:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("8:00:00")));
            }
            activity = new RunningActivity("first, second", mockTimeSystem);
            activity.Stop();
            RunningActivity second = activity.SplitByComma()[1];
            Assert.AreEqual(DateTime.Parse("7:00:00"), activity.Start);
            Assert.AreEqual(DateTime.Parse("7:30:00"), activity.End, "first end");
            Assert.AreEqual(DateTime.Parse("7:30:00"), second.Start, "second start");
            Assert.AreEqual(DateTime.Parse("8:00:00"), second.End);
        }
        [Test]
        public void Split3()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("7:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("8:00:00")));
            }
            activity = new RunningActivity("first, second, third", mockTimeSystem);
            activity.Stop();
            RunningActivity third = activity.SplitByComma()[2];
            Assert.AreEqual(DateTime.Parse("7:40:00"), third.Start);
        }
        [Test]
        public void SplitByCommaWhenNoComma()
        {
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
            activity = new RunningActivity("just one", mockTimeSystem);
            Assert.AreEqual(1,activity.SplitByComma().Length);
        }
        [Test]
        public void SplitByCommaTrim()
        {
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
            activity = new RunningActivity("first, second", mockTimeSystem);
            Assert.AreEqual("second", activity.SplitByComma()[1].Name);
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
