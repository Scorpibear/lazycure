using System;
using System.Collections.Generic;
using NMock2;
using NUnit.Framework;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;
using Is = NMock2.Is;

namespace LifeIdea.LazyCure.Core.Time
{
    [TestFixture]
    public class TimeManagerTest : Mockery
    {
        private TimeManager timeManager;
        private readonly DateTime startTime = DateTime.Parse("2125-06-30 05:00:00");
        private ITimeLogsManager timeLogsManager = null;
        private ITimeLog timeLog;

        [SetUp]
        public void SetUp()
        {
            ITimeSystem mockTime = NewMock<ITimeSystem>();
            Stub.On(mockTime).GetProperty("Now").Will(Return.Value(startTime));
            timeLogsManager = NewMock<ITimeLogsManager>();
            timeLog = NewMock<ITimeLog>();
            Stub.On(timeLogsManager).Method("ActivateTimeLog").Will(Return.Value(timeLog));
            timeManager = new TimeManager(mockTime, timeLogsManager);
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
            timeManager.TimeLogsManager = null;
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
            timeManager.TimeLogsManager = null;
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
        public void FinishActivityReturnsListOfActivities()
        {
            timeManager.TimeLogsManager = null;
            timeManager.SplitByComma = true;
            List<IActivity> activities = timeManager.FinishActivity("one, two", "not used");
            Assert.AreEqual("one", activities[0].Name);
            Assert.AreEqual(2, activities.Count, "number of finished activities");
            Assert.AreEqual("two", activities[1].Name);
        }
        [Test]
        public void SplitByComma()
        {
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(timeLog));
            Expect.Exactly(2).On(timeLog).Method("AddActivity");
            timeManager.SplitByComma = true;
            timeManager.FinishActivity("first,second", "second");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void Split3ByComma()
        {
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(timeLog));
            Expect.Exactly(3).On(timeLog).Method("AddActivity");
            timeManager.SplitByComma = true;
            timeManager.FinishActivity("first,second,third", "next");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SplitByCommaAddActivitiesWithDifferentNamesToTimeLog()
        {
            timeManager.SplitByComma = true;
            Expect.Once.On(timeLog).Method("AddActivity").With(new Activity("first", startTime, TimeSpan.Zero));
            Expect.Once.On(timeLog).Method("AddActivity").With(new Activity("second", startTime, TimeSpan.Zero));
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(timeLog));

            timeManager.FinishActivity("first, second", "next");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void DontSplitByComma()
        {
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(timeLog));
            Expect.Exactly(1).On(timeLog).Method("AddActivity");
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
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(timeLog));
            using (Ordered)
            {
                Expect.Once.On(timeLog).Method("AddActivity").With(new Activity("first", DateTime.Parse("2008-08-07 23:59:55"), TimeSpan.Parse("0:00:05")));
                Expect.Once.On(timeLog).Method("AddActivity").With(new Activity("first", DateTime.Parse("2008-08-08 0:00:00"), TimeSpan.Parse("0:00:05")));
                Expect.Once.On(timeLog).Method("AddActivity").With(new Activity("second", DateTime.Parse("2008-08-08 0:00:05"), TimeSpan.Parse("0:00:10")));
            }
            timeManager = new TimeManager(mockTime, timeLogsManager);
            timeManager.SwitchAtMidnight = true;
            timeManager.SplitByComma = true;
            timeManager.FinishActivity("first,second", "next");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SwitchTo()
        {
            timeManager.TimeLogsManager = null;
            string nextActivityName = "test second task";
            timeManager.SwitchTo(nextActivityName);
            Assert.AreEqual(nextActivityName, timeManager.CurrentActivity.Name);
        }
        [Test]
        public void SwitchToStartsNewActivity()
        {
            timeManager.TimeLogsManager = null;
            IActivity activity1 = timeManager.CurrentActivity;
            timeManager.SwitchTo("second");
            Assert.AreNotSame(activity1, timeManager.CurrentActivity);
        }
        [Test]
        public void AddRecordToTimeLog()
        {
            ITimeSystem mockSystem = NewMock<ITimeSystem>();
            Expect.Exactly(2).On(mockSystem).GetProperty("Now").Will(Return.Value(startTime));
            ITimeLog mockTimeLog = NewMock<ITimeLog>();
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(mockTimeLog));
            timeManager = new TimeManager(mockSystem, timeLogsManager);
            Expect.Once.On(mockTimeLog).Method("AddActivity").With(timeManager.CurrentActivity);

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
            Expect.Once.On(timeLogsManager).Method("ActivateTimeLog").With(DateTime.Parse("2008-08-07"));
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(timeLog1));
            timeManager = new TimeManager(mockTime, timeLogsManager);
            var midnightCorrector = NewMock<IMidnightCorrector>();
            timeManager.MidnightCorrector = midnightCorrector;
            Expect.Never.On(midnightCorrector).Method("PerformMidnightCorrection");
            timeManager.SwitchAtMidnight = false;
            timeManager.FinishActivity("first", "second");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void CheckForMidnightCallsPerformMidnightCorrection()
        {
            IMidnightCorrector midnightCorrector = NewMock<IMidnightCorrector>();
            Expect.Once.On(midnightCorrector).Method("PerformMidnightCorrection");
            timeManager.MidnightCorrector = midnightCorrector;
            timeManager.SwitchAtMidnight = true;

            timeManager.CheckForMidnight();

            VerifyAllExpectationsHaveBeenMet();
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

            timeManager.SwitchTo("second");
        }
        [Test]
        public void TimeLogGetUsesTimeLogsManager()
        {
            var timeLogsManager = NewMock<ITimeLogsManager>();
            timeManager.TimeLogsManager = timeLogsManager;
            TimeLog originalTimeLog = new TimeLog(DateTime.Today);
            Expect.Once.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(originalTimeLog));

            ITimeLog returnedTimeLog = timeManager.TimeLog;

            Assert.AreSame(originalTimeLog, returnedTimeLog);
        }
    }
}