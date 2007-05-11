using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class TimeLogOtherTest
    {
        private Mockery mocks;
        private TimeLog timeLog;
        private DateTime startTime = DateTime.Parse("2125-06-30 05:00:00");
        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
            timeLog = new TimeLog(mockTimeSystem, "first");
        }
        [Test]
        public void SwitchTo()
        {
            string nextActivityName = "test next task";
            Assert.AreEqual(nextActivityName, timeLog.SwitchTo(nextActivityName).Name);
            Assert.AreEqual(nextActivityName, timeLog.CurrentActivity.Name);
        }
        [Test]
        public void SwitchToStartsNewActivity()
        {
            IActivity activity1, activity2;
            activity1 = timeLog.CurrentActivity;
            activity2 = timeLog.SwitchTo("next");
            Assert.AreNotSame(activity1, activity2);
        }
        [Test]
        public void CurrentTaskStartTime()
        {
            Assert.AreEqual(startTime, timeLog.CurrentActivity.StartTime);
        }
        [Test]
        public void CurrentActivityDuration()
        {
            TimeSpan duration = TimeSpan.FromMinutes(15);
            DateTime endTime = startTime + duration;

            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();

            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(endTime));
            }
            TimeLog timeLog = new TimeLog(mockTimeSystem,"first");
            Assert.AreEqual(duration, timeLog.CurrentActivity.Duration);
        }
        [Test]
        public void ReturnsPreviousActivity()
        {
            timeLog.SwitchTo("task2");
            Assert.AreEqual("first", timeLog.PreviousActivity.Name);
        }
        [Test]
        public void FinishActivity()
        {
            string finishedActivity = "prev";
            string currentActivity = "next";
            timeLog.FinishActivity(finishedActivity, currentActivity);
            Assert.AreEqual(finishedActivity, timeLog.PreviousActivity.Name, "previous check");
            Assert.AreEqual(currentActivity, timeLog.CurrentActivity.Name, "current check");
        }
        [Test]
        public void FinishedActivityReusesCurrentActivity()
        {
            IActivity currentActivity = timeLog.CurrentActivity;
            timeLog.FinishActivity("prev", "next");
            Assert.AreSame(currentActivity, timeLog.PreviousActivity, "last current now is previous");
            Assert.AreNotSame(timeLog.PreviousActivity, timeLog.CurrentActivity, "current and previous different");
        }
        [Test]
        public void SaveThreeActivities()
        {
            timeLog.SwitchTo("second");
            timeLog.SwitchTo("third");
            MockWriter mockWriter = new MockWriter();
            timeLog.Save(mockWriter);
            Console.WriteLine(mockWriter.Content);
            Assert.IsTrue(mockWriter.Content.Contains("first"),"first");
            Assert.IsTrue(mockWriter.Content.Contains("second"),"second");
            Assert.IsTrue(mockWriter.Content.Contains("third"), "third");
        }
    
        
    }
}
