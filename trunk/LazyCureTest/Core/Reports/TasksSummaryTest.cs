using System;
using System.Data;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Reports
{
    [TestFixture]
    public class TasksSummaryTest
    {
        private TasksSummary tasksSummary;
        [SetUp]
        public void SetUp()
        {
            DataTable activitiesSummaryTable = new DataTable("ActivitiesSummary");
            activitiesSummaryTable.Columns.Add("Task");
            activitiesSummaryTable.Columns.Add("Spent", TimeSpan.Zero.GetType());
            tasksSummary = new TasksSummary(activitiesSummaryTable);
        }
        [Test]
        public void DataTableColumnsTypes()
        {
            Assert.AreEqual("task1".GetType(),tasksSummary.Data.Columns["Task"].DataType);
            Assert.AreEqual(TimeSpan.Zero.GetType(),tasksSummary.Data.Columns["Spent"].DataType);
        }
        [Test]
        public void ActivitySummaryIsUsed()
        {
            tasksSummary.ActivitiesSummaryTable.Rows.Add("task1", TimeSpan.Parse("0:13"));
            
            tasksSummary.Calculate();

            Assert.AreEqual(TimeSpan.Parse("0:13"),tasksSummary.Data.Rows[0]["Spent"]);
        }
        [Test]
        public void SumTheSameTasks()
        {
            tasksSummary.ActivitiesSummaryTable.Rows.Add("task1", TimeSpan.Parse("0:13"));
            tasksSummary.ActivitiesSummaryTable.Rows.Add("task1", TimeSpan.Parse("0:17"));
            
            tasksSummary.Calculate();

            Assert.AreEqual(TimeSpan.Parse("0:30"), tasksSummary.Data.Rows[0]["Spent"], "spent time");
            Assert.AreEqual(1, tasksSummary.Data.Rows.Count, "rows count");
        }
        [Test]
        public void DataUpdatedWhenActivitiesSummaryIsUpdated()
        {
            tasksSummary.ActivitiesSummaryTable.Rows.Add("task1", TimeSpan.Parse("1:02:03"));

            Assert.AreEqual(TimeSpan.Parse("1:02:03"), tasksSummary.Data.Rows[0]["Spent"]);
        }
        [Test]
        public void TaskRenameRecalculateData()
        {
            tasksSummary.ActivitiesSummaryTable.Rows.Add("task1", TimeSpan.Parse("0:01"));
            tasksSummary.ActivitiesSummaryTable.Rows.Add("task2", TimeSpan.Parse("0:02"));
            tasksSummary.ActivitiesSummaryTable.Rows[1]["Task"] = "task1";

            Assert.AreEqual(TimeSpan.Parse("0:03"), tasksSummary.Data.Rows[0]["Spent"]);
            Assert.AreEqual(1, tasksSummary.Data.Rows.Count, "rows count");
        }
        [Test]
        public void ActivitiesSummaryRowDeleteUpdatesTasksSummary()
        {
            tasksSummary.ActivitiesSummaryTable.Rows.Add("task1", TimeSpan.Parse("0:01"));
            tasksSummary.ActivitiesSummaryTable.Rows[0].Delete();

            Assert.AreEqual(0, tasksSummary.Data.Rows.Count, "rows count");
        }
    }
}
