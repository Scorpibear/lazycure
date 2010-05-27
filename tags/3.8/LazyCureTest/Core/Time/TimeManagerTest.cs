using System;
using LifeIdea.LazyCure.Interfaces;
using NMock2;
using NUnit.Framework;
using LifeIdea.LazyCure.Core.Activities;

namespace LifeIdea.LazyCure.Core.Time
{
    [TestFixture]
    public class TimeManagerTest : Mockery
    {
        private TimeManager timeManager;
        private readonly DateTime startTime = DateTime.Parse("2125-06-30 05:00:00");

        [SetUp]
        public void SetUp()
        {
            ITimeSystem mockTime = NewMock<ITimeSystem>();
            Stub.On(mockTime).GetProperty("Now").Will(Return.Value(startTime));
            timeManager = new TimeManager(mockTime);
        }
        [Test]
        public void CurrentActivityDuration()
        {
            TimeSpan duration = TimeSpan.FromMinutes(15);
            DateTime endTime = startTime + duration;
            ITimeSystem timeSystem = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(timeSystem).GetProperty("Now").Will(Return.Value(startTime));
                Expect.Once.On(timeSystem).GetProperty("Now").Will(Return.Value(endTime));
            }

            timeManager = new TimeManager(timeSystem);

            Assert.AreEqual(duration, timeManager.CurrentActivity.Duration);
        }
        [Test]
        public void CurrentActivityDiffersFromFinished()
        {
            timeManager.FinishActivity("prev", "second");
            Assert.AreNotSame(timeManager.PreviousActivity, timeManager.CurrentActivity, "current and previous different");
        }
        [Test]
        public void CurrentActivityStartTime()
        {
            Assert.AreEqual(startTime, timeManager.CurrentActivity.Start);
        }
        [Test]
        public void CurrentActivityIsLastingTooLongIsFalseByDefault()
        {
            Assert.IsFalse(timeManager.CurrentActivityIsLastingTooLong);
        }
        [Test]
        public void CurrentActivityIsLastingTooLongIsTrueAfterAnHourOfInactivity()
        {
            ITimeSystem timeSystem = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(timeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("5:00:00")));
                Expect.AtLeastOnce.On(timeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("6:00:00")));
            }
            timeManager = new TimeManager(timeSystem);
            Assert.IsTrue(timeManager.CurrentActivityIsLastingTooLong);
        }
        [Test]
        public void CurrentActivityIsLastingTooLongIsFalseBeforeAnHourOfInactivity()
        {
            ITimeSystem timeSystem = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(timeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("5:00:00")));
                Expect.AtLeastOnce.On(timeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("5:59:59")));
            }
            timeManager = new TimeManager(timeSystem);
            Assert.IsFalse(timeManager.CurrentActivityIsLastingTooLong);
        }
        [Test]
        public void FinishActivity()
        {
            string finishedActivity = "prev";
            string currentActivity = "second";
            timeManager.FinishActivity(finishedActivity, currentActivity);
            Assert.AreEqual(finishedActivity, timeManager.PreviousActivity.Name, "previous check");
            Assert.AreEqual(currentActivity, timeManager.CurrentActivity.Name, "current check");
        }
        [Test]
        public void FinishActivityUseNowOnce()
        {
            ITimeSystem mockSystem = NewMock<ITimeSystem>();
            Expect.Exactly(2).On(mockSystem).GetProperty("Now").Will(Return.Value(startTime));
            timeManager = new TimeManager(mockSystem);

            timeManager.FinishActivity("activityName", "second");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SplitByComma()
        {
            timeManager.TimeLog = NewMock<ITimeLog>();
            Expect.Exactly(2).On(timeManager.TimeLog).Method("AddActivity");
            timeManager.SplitByComma = true;
            timeManager.FinishActivity("first,second", "second");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void Split3ByComma()
        {
            timeManager.TimeLog = NewMock<ITimeLog>();
            Expect.Exactly(3).On(timeManager.TimeLog).Method("AddActivity");
            timeManager.SplitByComma = true;
            timeManager.FinishActivity("first,second,third", "next");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void DontSplitByComma()
        {
            timeManager.TimeLog = NewMock<ITimeLog>();
            Expect.Exactly(1).On(timeManager.TimeLog).Method("AddActivity");
            timeManager.SplitByComma = false;
            timeManager.FinishActivity("first,second", "next");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SplitByCommaAtMidnight()
        {
            ITimeSystem mockTime = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:15")));
            }
            ITimeLog timeLog = NewMock<ITimeLog>();
            using (Ordered)
            {
                Expect.Once.On(timeLog).Method("AddActivity").With(new Activity("first", DateTime.Parse("2008-08-07 23:59:55"), TimeSpan.Parse("0:00:05")));
                Expect.Once.On(timeLog).Method("AddActivity").With(new Activity("first", DateTime.Parse("2008-08-08 0:00:00"), TimeSpan.Parse("0:00:05")));
                Expect.Once.On(timeLog).Method("AddActivity").With(new Activity("second", DateTime.Parse("2008-08-08 0:00:05"), TimeSpan.Parse("0:00:10")));
            }
            timeManager = new TimeManager(mockTime);
            timeManager.TimeLog = timeLog;
            timeManager.SwitchAtMidnight = true;
            timeManager.SplitByComma = true;
            timeManager.FinishActivity("first,second", "next");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SwitchTo()
        {
            string nextActivityName = "test second task";
            Assert.AreEqual(nextActivityName, timeManager.SwitchTo(nextActivityName).Name);
            Assert.AreEqual(nextActivityName, timeManager.CurrentActivity.Name);
        }
        [Test]
        public void SwitchToStartsNewActivity()
        {
            IActivity activity1, activity2;
            activity1 = timeManager.CurrentActivity;
            activity2 = timeManager.SwitchTo("second");
            Assert.AreNotSame(activity1, activity2);
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

            timeManager.FinishActivity("first", "second");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void TimeLogIsSwitchedAtMidnight()
        {
            ITimeSystem mockTime = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:06")));
            }
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            Expect.Once.On(timeLogsManager).Method("Save").Will(Return.Value(true));
            Expect.Once.On(timeLogsManager).Method(Is.Anything);
            ITimeLog timeLog1 = new TimeLog(DateTime.Now);
            timeManager = new TimeManager(mockTime, timeLogsManager, timeLog1);
            timeManager.SwitchAtMidnight = true;
            timeManager.FinishActivity("first", "second");
            Assert.AreEqual(DateTime.Parse("2008-08-08"),timeManager.TimeLog.Date);
            Assert.AreNotEqual(timeLog1, timeManager.TimeLog);
        }
        [Test]
        public void AfterMidnightActivityIsCut()
        {
            ITimeSystem mockTime = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:06")));
            }
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            ITimeLog timeLog1 = NewMock<ITimeLog>();
            Expect.Once.On(timeLog1).Method("AddActivity").With(new Activities.Activity("first", DateTime.Parse("2008-08-07 23:59:55"), TimeSpan.Parse("0:00:05")));
            Stub.On(timeLogsManager).Method("Save").Will(Return.Value(true));
            Stub.On(timeLogsManager).Method(Is.Anything);
            timeManager = new TimeManager(mockTime, timeLogsManager, timeLog1);
            timeManager.SwitchAtMidnight = true;
            timeManager.FinishActivity("first", "second");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SwitchAtMidnightFalse()
        {
            ITimeSystem mockTime = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:06")));
            }
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            ITimeLog timeLog1 = NewMock<ITimeLog>();
            Expect.Once.On(timeLog1).Method("AddActivity").With(new Activities.Activity("first", DateTime.Parse("2008-08-07 23:59:55"), TimeSpan.Parse("0:00:11")));
            Expect.Never.On(timeLogsManager).Method("Save").Will(Return.Value(true));
            Stub.On(timeLogsManager).Method(Is.Anything);
            timeManager = new TimeManager(mockTime, timeLogsManager, timeLog1);
            timeManager.SwitchAtMidnight = false;
            timeManager.FinishActivity("first", "second");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void AfterMidnightNewLogContainsOneRecord()
        {
            ITimeSystem mockTime = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:06")));
            }
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(timeLogsManager).Method("Save").Will(Return.Value(true));
            Stub.On(timeLogsManager).Method(Is.Anything);
            timeManager = new TimeManager(mockTime, timeLogsManager,new TimeLog(DateTime.Now));
            timeManager.SwitchAtMidnight = true;
            timeManager.FinishActivity("first", "second");
            Assert.AreEqual(1, timeManager.TimeLog.Activities.Count, "1 activity");
            Assert.AreEqual(new Activity("first",DateTime.Parse("2008-08-08 0:00:00"),TimeSpan.Parse("0:00:06")), timeManager.TimeLog.Activities[0]);
        }
        [Test]
        public void SwitchAtMidnightWithoutTimeLogsManager()
        {
            ITimeSystem mockTime = NewMock<ITimeSystem>();
            using (Ordered)
            {
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
                Expect.Once.On(mockTime).GetProperty("Now").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:06")));
            }
            timeManager = new TimeManager(mockTime);
            timeManager.TimeLog = new TimeLog(DateTime.Now);
            timeManager.SwitchTo("second");
        }
        [Test]
        public void AtMidnightReferenciesUpdated()
        {
            timeManager.TimeLogsManager = NewMock<ITimeLogsManager>();
            Expect.Once.On(timeManager.TimeLogsManager).Method("UpdateTimeLogReferencies");
            Stub.On(timeManager.TimeLogsManager).Method("Save").Will(Return.Value(false));
            timeManager.PerformMidnightCorrection(DateTime.Now);
            VerifyAllExpectationsHaveBeenMet();
        }
    }
}