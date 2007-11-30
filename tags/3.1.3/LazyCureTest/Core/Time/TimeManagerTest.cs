using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Interfaces;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Time
{
    [TestFixture]
    public class TimeManagerTest: Mockery
    {
        private TimeManager timeManager;
        private readonly DateTime startTime = DateTime.Parse("2125-06-30 05:00:00");

        [SetUp]
        public void SetUp()
        {
            ITimeSystem mockSystem = NewMock<ITimeSystem>();
            Stub.On(mockSystem).GetProperty("Now").Will(Return.Value(startTime));
            timeManager = new TimeManager(mockSystem);
        }
        [Test]
        public void SwitchTo()
        {
            string nextActivityName = "test next task";
            Assert.AreEqual(nextActivityName, timeManager.SwitchTo(nextActivityName).Name);
            Assert.AreEqual(nextActivityName, timeManager.CurrentActivity.Name);
        }
        [Test]
        public void SwitchToStartsNewActivity()
        {
            IActivity activity1, activity2;
            activity1 = timeManager.CurrentActivity;
            activity2 = timeManager.SwitchTo("next");
            Assert.AreNotSame(activity1, activity2);
        }
        [Test]
        public void CurrentTaskStartTime()
        {
            Assert.AreEqual(startTime, timeManager.CurrentActivity.StartTime);
        }
        [Test]
        public void FinishActivity()
        {
            string finishedActivity = "prev";
            string currentActivity = "next";
            timeManager.FinishActivity(finishedActivity, currentActivity);
            Assert.AreEqual(finishedActivity, timeManager.PreviousActivity.Name, "previous check");
            Assert.AreEqual(currentActivity, timeManager.CurrentActivity.Name, "current check");
        }
        [Test]
        public void CurrentActivityDiffersFromFinished()
        {
            timeManager.FinishActivity("prev", "next");
            Assert.AreNotSame(timeManager.PreviousActivity, timeManager.CurrentActivity, "current and previous different");
        }
        [Test]
        public void FinishActivityUseNowOnce()
        {
            ITimeSystem mockSystem = NewMock<ITimeSystem>();
            Expect.Exactly(2).On(mockSystem).GetProperty("Now").Will(Return.Value(startTime));
            timeManager = new TimeManager(mockSystem);
            
            timeManager.FinishActivity("activityName", "next");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void AddRecordToTimeLog()
        {
            ITimeSystem mockSystem = NewMock<ITimeSystem>();
            Expect.Exactly(2).On(mockSystem).GetProperty("Now").Will(Return.Value(startTime));
            timeManager = new TimeManager(mockSystem);

            ITimeLog mockTimeLog = NewMock<ITimeLog>();
            Expect.Once.On(mockTimeLog).Method("AddActivity").With(timeManager.CurrentActivity);
            timeManager.TimeLog = mockTimeLog;

            timeManager.FinishActivity("first","second");
            
            VerifyAllExpectationsHaveBeenMet();
        }
    }
}
