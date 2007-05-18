using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;
using System.Data;

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
        [Test]
        public void DataColumns()
        {
            Assert.AreEqual(3, timeLog.Data.Columns.Count, "columns count");
            Assert.AreEqual(Type.GetType("System.String"), timeLog.Data.Columns["Activity"].DataType, "Activity column");
            Assert.AreEqual(Type.GetType("System.DateTime"), timeLog.Data.Columns["Start"].DataType, "Start column");
            Assert.AreEqual(Type.GetType("System.TimeSpan"), timeLog.Data.Columns["Duration"].DataType, "Duration column");
            //Assert.AreEqual(Type.GetType("System.DateTime"), timeLog.Data.Columns["End"].DataType, "End column");
        }
        [Test]
        public void ActivitiesSummaryDataColumns()
        {
            DataTable summary = timeLog.ActivitiesSummary;
            Assert.AreEqual(Type.GetType("System.String"), summary.Columns["Activity"].DataType);
            Assert.AreEqual(Type.GetType("System.TimeSpan"), summary.Columns["Spent"].DataType);
        }
        [Test]
        public void ActivitiesSummarySimpleRecord()
        {
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 6:23:45")));
            }
            timeLog = new TimeLog(mockTimeSystem, "first");
            timeLog.SwitchTo("second");
            DataTable summary = timeLog.ActivitiesSummary;
            DataRow firstRow = summary.Rows[0];
            Assert.AreEqual("first", firstRow["Activity"]);
            Assert.AreEqual(TimeSpan.Parse("1:23:45"), firstRow["Spent"]);
        }
        
        [Test]
        public void ActivitiesSummaryTwoDiffRecords()
        {
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:07:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:10:00")));
            }
            timeLog = new TimeLog(mockTimeSystem,"first");
            timeLog.SwitchTo("second");
            timeLog.SwitchTo("third");
            DataTable summary = timeLog.ActivitiesSummary;
            DataRow firstRow = summary.Rows[0];
            DataRow secondRow = summary.Rows[1];
            Assert.AreEqual(2, summary.Rows.Count, "rows count");
            Assert.AreEqual("first", firstRow["Activity"]);
            Assert.AreEqual(TimeSpan.Parse("0:07:00"), firstRow["Spent"]);
            Assert.AreEqual("second", secondRow["Activity"]);
            Assert.AreEqual(TimeSpan.Parse("0:03:00"), secondRow["Spent"]);
        }
        [Test]
        public void ActivitiesSummaryTwoEqualRecords()
        {
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:07:00")));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:10:00")));
            }
            timeLog = new TimeLog(mockTimeSystem,"first");
            timeLog.SwitchTo("first");
            timeLog.SwitchTo("second");
            DataTable summary = timeLog.ActivitiesSummary;
            DataRow firstRow = summary.Rows[0];
            Assert.AreEqual(1, summary.Rows.Count, "rows count");
            Assert.AreEqual(firstRow["Activity"], "first");
            Assert.AreEqual(firstRow["Spent"], TimeSpan.Parse("0:10:00"));
        }
        [Test]
        public void SummaryChangedAfterDataTableChange()
        {
            timeLog.SwitchTo("second");
            timeLog.Data.Rows[0]["Activity"] = "aaa";
            Assert.AreEqual("aaa",timeLog.ActivitiesSummary.Rows[0]["Activity"]);
        }
        [Test]
        public void ColumnsOrder()
        {
            Assert.AreEqual(0, timeLog.Data.Columns["Start"].Ordinal);
            Assert.AreEqual(1, timeLog.Data.Columns["Activity"].Ordinal);
            Assert.AreEqual(2, timeLog.Data.Columns["Duration"].Ordinal);
        }
    }
}
