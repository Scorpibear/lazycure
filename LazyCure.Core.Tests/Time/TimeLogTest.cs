using System;
using System.Data;
using LifeIdea.LazyCure.Core.Activities;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Time
{
    [TestFixture]
    public class TimeLogTest:Mockery
    {
        private TimeLog timeLog;
        private readonly DateTime startTime = DateTime.Parse("2125-06-30 05:00:00");
        [SetUp]
        public void SetUp()
        {
            timeLog = new TimeLog(startTime.Date);
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
            theRow["Start"] = DateTime.Parse("15:11");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10");
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(1,timeLog.Data.Rows.Count);
            Assert.AreEqual(DateTime.Parse("15:21"), timeLog.Data.Rows[0]["End"]);
            theRow = timeLog.Data.NewRow();
            theRow["Duration"] = TimeSpan.Parse("0:15");
            theRow["Start"] = DateTime.Parse("15:21");
            theRow["Activity"] = "test2";
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(2, timeLog.Data.Rows.Count);
            Assert.AreEqual(DateTime.Parse("15:36"), timeLog.Data.Rows[1]["End"]);
        }
        [Test]
        public void StartCalculation()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["End"] = DateTime.Parse("15:10");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10");
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(DateTime.Parse("15:00"), timeLog.Data.Rows[0]["Start"]);
            theRow = timeLog.Data.NewRow();
            theRow["Activity"] = "test2";
            theRow["Duration"] = TimeSpan.Parse("0:15");
            theRow["End"] = DateTime.Parse("15:25");
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(DateTime.Parse("15:10"), timeLog.Data.Rows[1]["Start"]);
        }
        [Test]
        public void DurationCalculation()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["Start"] = DateTime.Parse("15:00");
            theRow["End"] = DateTime.Parse("15:10");
            theRow["Activity"] = "test1";
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(TimeSpan.Parse("0:10"), timeLog.Data.Rows[0]["Duration"]);
            theRow = timeLog.Data.NewRow();
            theRow["End"] = DateTime.Parse("15:25");
            theRow["Start"] = DateTime.Parse("15:10");
            theRow["Activity"] = "test1";
            timeLog.Data.Rows.Add(theRow);
            Assert.AreEqual(TimeSpan.Parse("0:15"), timeLog.Data.Rows[1]["Duration"]);
    
        }
        [Test]
        public void ChangeStartDurationChanged()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["Start"] = DateTime.Parse("15:00");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10");
            timeLog.Data.Rows.Add(theRow);
            timeLog.Data.Rows[0]["Start"] = DateTime.Parse("14:30");
            Assert.AreEqual(TimeSpan.Parse("0:40"), timeLog.Data.Rows[0]["Duration"]);
        }
        [Test]
        public void ChangeDurationEndChanged()
        {
            DataRow theRow = timeLog.Data.NewRow();
            theRow["Start"] = DateTime.Parse("15:00");
            theRow["Activity"] = "test1";
            theRow["Duration"] = TimeSpan.Parse("0:10");
            timeLog.Data.Rows.Add(theRow);
            timeLog.Data.Rows[0]["Duration"] = TimeSpan.Parse("0:15");
            Assert.AreEqual(DateTime.Parse("15:15"), timeLog.Data.Rows[0]["End"]);
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
            timeLog.AddActivity(new Activity("name",DateTime.MinValue,new TimeSpan()));
            timeLog.Data.Rows[0]["Duration"] = DBNull.Value;
            timeLog.Data.Rows[0]["End"] = DBNull.Value;
            Assert.IsEmpty(timeLog.Activities);
        }
        [Test]
        public void CalcDurationAtTheEndOfDay()
        {
            DataRow row = timeLog.Data.NewRow();
            row["Start"] = DateTime.Parse("23:00");
            row["Activity"] = "activity";
            row["End"] = DateTime.Parse("0:00");
            timeLog.Data.Rows.Add(row);
            Assert.AreEqual(TimeSpan.Parse("1:00"),timeLog.Activities[0].Duration);
        }
        [Test]
        public void Equal()
        {
            TimeLog timeLog1 = new TimeLog(DateTime.Parse("2008-02-19"));
            TimeLog timeLog2 = new TimeLog(DateTime.Parse("2008-02-19"));
            Assert.AreEqual(timeLog2, timeLog1);
        }
    }
}