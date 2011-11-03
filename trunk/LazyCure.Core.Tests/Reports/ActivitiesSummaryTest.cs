using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Shared.Interfaces;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Reports
{
    [TestFixture]
    public class ActivitiesSummaryTest:Mockery
    {
        private ActivitiesSummary activitiesSummary;
        private ITimeLog timeLog;
        private ITaskActivityLinker linker;
        private readonly TimeSpan sevenSec = TimeSpan.Parse("0:07:00");
        private readonly TimeSpan threeSec = TimeSpan.Parse("0:03:00");
        private readonly TimeSpan tenSec = TimeSpan.Parse("0:10:00");

        [SetUp]
        public void SetUp()
        {
            timeLog = NewMock<ITimeLog>();
            linker = NewMock<ITaskActivityLinker>();
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            Stub.On(linker).Method("GetRelatedTaskName");
            activitiesSummary = new ActivitiesSummary(timeLog, linker);
        }
        [TearDown]
        public void TearDown()
        {
            this.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void DataColumnsTypes()
        {
            Assert.AreEqual(Type.GetType("System.String"), activitiesSummary.Data.Columns["Activity"].DataType);
            Assert.AreEqual(Type.GetType("System.TimeSpan"), activitiesSummary.Data.Columns["Spent"].DataType);
            Assert.AreEqual(Type.GetType("System.String"), activitiesSummary.Data.Columns["Task"].DataType);
        }
        [Test]
        public void SimpleRecord()
        {
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                                                                             new IActivity[] { new Activity("first", DateTime.Now, sevenSec) })));

            activitiesSummary.Update();

            DataRow firstRow = activitiesSummary.Data.Rows[0];
            Assert.AreEqual("first", firstRow["Activity"]);
            Assert.AreEqual(sevenSec, firstRow["Spent"]);
        }
        [Test]
        public void TwoDifferentActivities()
        {
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(new Activity[]{
                                                                                                               new Activity("first", DateTime.Now, sevenSec),
                                                                                                               new Activity("second", DateTime.Now, threeSec)})));

            activitiesSummary.Update();

            DataRow firstRow = activitiesSummary.Data.Rows[0];
            DataRow secondRow = activitiesSummary.Data.Rows[1];
            Assert.AreEqual(2, activitiesSummary.Data.Rows.Count, "rows count");
            Assert.AreEqual("first", firstRow["Activity"]);
            Assert.AreEqual(sevenSec, firstRow["Spent"]);
            Assert.AreEqual("second", secondRow["Activity"]);
            Assert.AreEqual(threeSec, secondRow["Spent"]);
        }
        [Test]
        public void TwoEqualActivities()
        {
            Stub.On(timeLog).GetProperty("Activities").Will(
                Return.Value(new List<IActivity>(new IActivity[]{
                    new Activity("first", DateTime.Now, sevenSec),
                    new Activity("first", DateTime.Now, threeSec)})));

            activitiesSummary.Update();

            Assert.AreEqual(1, activitiesSummary.Data.Rows.Count, "rows count");
            Assert.AreEqual(tenSec, activitiesSummary.Data.Rows[0]["Spent"]);
        }
        [Test]
        public void AllActivitiesTime()
        {
            Stub.On(timeLog).GetProperty("Activities").Will(
                Return.Value(new List<IActivity>(new IActivity[]{
                    new Activity("first", DateTime.Now, sevenSec),
                    new Activity("second", DateTime.Now, threeSec)})));

            activitiesSummary.Update();

            Assert.AreEqual(tenSec,activitiesSummary.AllActivitiesTime);
        }
        [Test]
        public void GetRelatedTask()
        {
            Stub.On(timeLog).GetProperty("Activities").Will(
                Return.Value(new List<IActivity>(
                new IActivity[] { new Activity("first", DateTime.Now, sevenSec) })));

            linker = NewMock<ITaskActivityLinker>();
            Expect.AtLeastOnce.On(linker).Method("GetRelatedTaskName").With("first").Will(Return.Value("related task"));
            
            activitiesSummary = new ActivitiesSummary(timeLog,linker);
            activitiesSummary.Update();
            string task = activitiesSummary.Data.Rows[0]["Task"] as string;
            Assert.IsNotNull(task);
            Assert.AreEqual("related task", task);
        }
        [Test]
        public void LinkActivityAndTask()
        {
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(
                                                                             new IActivity[] { new Activity("activity1", DateTime.Now, sevenSec) })));

            Expect.Once.On(linker).Method("LinkActivityAndTask").With("activity1","task1").Will(Return.Value(true));

            activitiesSummary.Update();

            activitiesSummary.Data.Rows[0]["Task"] = "task1";
        }
        [Test]
        public void TimeSpentOnAllActivitiesIsUpdatedWhenTimeLogsIsChanged()
        {
            timeLog = new TimeLog(DateTime.Now.Date);
            activitiesSummary.TimeLog = timeLog;
            timeLog.AddActivity(new Activity("test",DateTime.Parse("5:00:00"),TimeSpan.Parse("0:10:00")));

            timeLog.Data.Rows[0]["Duration"] = TimeSpan.Parse("0:05:00");
            
            Assert.AreEqual(TimeSpan.Parse("0:05:00"), activitiesSummary.AllActivitiesTime);
        }
        [Test]
        public void SummaryIsUpdatedWhenTimeLogEntryDeleted()
        {
            timeLog = new TimeLog(DateTime.Now.Date);
            activitiesSummary.TimeLog = timeLog;
            timeLog.AddActivity(new Activity("test", DateTime.Parse("5:00:00"), TimeSpan.Parse("0:10:00")));

            timeLog.Data.Rows[0].Delete();

            Assert.AreEqual(TimeSpan.Zero, activitiesSummary.AllActivitiesTime);
        }
    }
}