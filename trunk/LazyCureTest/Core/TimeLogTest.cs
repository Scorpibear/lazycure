using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;
using System.Data;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class TimeLogTest
    {
        private Mockery mocks;
        private TimeLog timeLog;
        private readonly DateTime startTime = DateTime.Parse("2125-06-30 05:00:00");
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
        public void FinishActivity()
        {
            string finishedActivity = "prev";
            string currentActivity = "next";
            timeLog.FinishActivity(finishedActivity, currentActivity);
            Assert.AreEqual(finishedActivity, timeLog.Activities[0].Name, "previous check");
            Assert.AreEqual(currentActivity, timeLog.CurrentActivity.Name, "current check");
        }
        [Test]
        public void CurrentActivityDiffersFromFinished()
        {
            timeLog.FinishActivity("prev", "next");
            Assert.AreNotSame(timeLog.Activities[0], timeLog.CurrentActivity, "current and previous different");
        }
        [Test]
        public void DataColumns()
        {
            Assert.AreEqual(Type.GetType("System.String"), timeLog.Data.Columns["Activity"].DataType, "Activity column");
            Assert.AreEqual(Type.GetType("System.DateTime"), timeLog.Data.Columns["Start"].DataType, "Start column");
            Assert.AreEqual(Type.GetType("System.TimeSpan"), timeLog.Data.Columns["Duration"].DataType, "Duration column");
            Assert.AreEqual(Type.GetType("System.DateTime"), timeLog.Data.Columns["End"].DataType, "End column");
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
        [Test]
        public void EndCalculation()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["Start"] = DateTime.Parse("15:11:00");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10:00");
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(1,timeLog.Data.Rows.Count);
            Assert.AreEqual(DateTime.Parse("15:21:00"), timeLog.Data.Rows[0]["End"]);
            theRow = timeLog.Data.NewRow();
            theRow["Duration"] = TimeSpan.Parse("0:15:00");
            theRow["Start"] = DateTime.Parse("15:21:00");
            theRow["Activity"] = "test2";
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(2, timeLog.Data.Rows.Count);
            Assert.AreEqual(DateTime.Parse("15:36:00"), timeLog.Data.Rows[1]["End"]);
        }
        [Test]
        public void StartCalculation()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["End"] = DateTime.Parse("15:10:00");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10:00");
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(DateTime.Parse("15:00:00"), timeLog.Data.Rows[0]["Start"]);
            theRow = timeLog.Data.NewRow();
            theRow["Activity"] = "test2";
            theRow["Duration"] = TimeSpan.Parse("0:15:00");
            theRow["End"] = DateTime.Parse("15:25:00");
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(DateTime.Parse("15:10:00"), timeLog.Data.Rows[1]["Start"]);
        }
        [Test]
        public void DurationCalculation()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["Start"] = DateTime.Parse("15:00:00");
            theRow["End"] = DateTime.Parse("15:10:00");
            theRow["Activity"] = "test1";
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(TimeSpan.Parse("0:10:00"), timeLog.Data.Rows[0]["Duration"]);
            theRow = timeLog.Data.NewRow();
            theRow["End"] = DateTime.Parse("15:25:00");
            theRow["Start"] = DateTime.Parse("15:10:00");
            theRow["Activity"] = "test1";
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(TimeSpan.Parse("0:15:00"), timeLog.Data.Rows[1]["Duration"]);
    
        }
        [Test]
        public void ChangeStartEndChanged()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["Start"] = DateTime.Parse("15:00:00");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10:00");
            timeLog.Data.Rows.Add(theRow);
            timeLog.Data.Rows[0]["Start"] = DateTime.Parse("14:30:00");
            Assert.AreEqual(DateTime.Parse("14:40:00"), timeLog.Data.Rows[0]["End"]);
        }
        [Test]
        public void ChangeDurationEndChanged()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["Start"] = DateTime.Parse("15:00:00");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10:00");
            timeLog.Data.Rows.Add(theRow);
            timeLog.Data.Rows[0]["Duration"] = TimeSpan.Parse("0:15:00");
            Assert.AreEqual(DateTime.Parse("15:15:00"), timeLog.Data.Rows[0]["End"]);
        }
        [Test]
        public void ChangeEndDurationChanged()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["Start"] = DateTime.Parse("15:00:00");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10:00");
            timeLog.Data.Rows.Add(theRow);
            timeLog.Data.Rows[0]["End"] = DateTime.Parse("16:12:34");
            Assert.AreEqual(TimeSpan.Parse("01:12:34"), timeLog.Data.Rows[0]["Duration"]);
        }
        [Test]
        public void ChangesSaved()
        {
            timeLog.SwitchTo("second");
            timeLog.Data.Rows[0]["Activity"] = "changed";
            MockWriter mockWriter = new MockWriter();
            timeLog.Save(mockWriter);
            Assert.IsTrue(mockWriter.Content.Contains("changed"));
        }
        [Test]
        public void StartCouldNotBeDBNull()
        {
            Assert.IsFalse(timeLog.Data.Columns["Start"].AllowDBNull);
        }
        [Test]
        public void ActivityCouldNotBeDBNull()
        {
            Assert.IsFalse(timeLog.Data.Columns["Activity"].AllowDBNull);
        }
        [Test]
        public void GetActivitiesWithEmptyDurationAndEnd()
        {
            timeLog.SwitchTo("second");
            timeLog.Data.Rows[0]["Duration"] = DBNull.Value;
            timeLog.Data.Rows[0]["End"] = DBNull.Value;
            Assert.IsEmpty(timeLog.Activities);
        }
    }
}
