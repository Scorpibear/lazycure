using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Interfaces;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class ActivitiesSummaryTest:Mockery
    {
        private ActivitiesSummary activitiesSummary;
        private ITimeLog timeLog;
        private readonly TimeSpan sevenSec = TimeSpan.Parse("0:07:00");
        private readonly TimeSpan threeSec = TimeSpan.Parse("0:03:00");
        private readonly TimeSpan tenSec = TimeSpan.Parse("0:10:00");

        [SetUp]
        public void SetUp()
        {
            timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Data").Will(Return.Value(new DataTable()));
            activitiesSummary = new ActivitiesSummary(timeLog);
        }
        [Test]
        public void DataColumnsTypes()
        {
            Assert.AreEqual(Type.GetType("System.String"), activitiesSummary.Data.Columns["Activity"].DataType);
            Assert.AreEqual(Type.GetType("System.TimeSpan"), activitiesSummary.Data.Columns["Spent"].DataType);
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
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(new List<IActivity>(new IActivity[]{
                new Activity("first", DateTime.Now, sevenSec),
                new Activity("first", DateTime.Now, threeSec)})));

            activitiesSummary.Update();

            Assert.AreEqual(1, activitiesSummary.Data.Rows.Count, "rows count");
            Assert.AreEqual(tenSec, activitiesSummary.Data.Rows[0]["Spent"]);
        }
    }
}
