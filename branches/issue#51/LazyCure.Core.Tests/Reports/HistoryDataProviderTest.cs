using System;
using System.Data;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Shared.Interfaces;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Core.Tasks;

namespace LifeIdea.LazyCure.Core.Reports
{
    public class HistoryDataProviderTest: Mockery
    {
        HistoryDataProvider provider;
        [SetUp]
        public void SetUp()
        {
            provider = new HistoryDataProvider(null, null);
        }
        [Test]
        public void DataReturnsValidDataTable()
        {
            object data = provider.Data;
            Assert.AreEqual(typeof(DataTable), data.GetType());
        }
        [Test]
        public void TodaysActivityIsReturned()
        {
            string uniqueActivity = "todays"+DateTime.Now;
            provider.TimeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(provider.TimeLogsManager).Method("GetActivities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity(uniqueActivity, DateTime.Now, TimeSpan.Parse("0:30"))
                })));
            Stub.On(provider.TimeLogsManager).GetProperty("AvailableDays").Will(Return.Value(new List<DateTime>((new DateTime[] { DateTime.Now }))));

            provider.UpdateDataTableForActivity(uniqueActivity);
            DataTable table = provider.Data;
            
            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), row["Day"]);
            Assert.AreEqual("0:30", row["Spent"]);
        }
        [Test]
        public void AskTimeLogsManagerForAvailableDays()
        {
            provider.TimeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(provider.TimeLogsManager).Method("GetActivities").Will(Return.Value(new List<IActivity>()));
            Expect.Once.On(provider.TimeLogsManager).GetProperty("AvailableDays").Will(Return.Value(new List<DateTime>()));

            provider.UpdateDataTableForActivity("yesterdayActivity");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void UpdateDataTableForTask()
        {
            provider.TaskCollection = NewMock<ITaskCollection>();
            Task task = new Task("task1");
            task.RelatedActivities.Add("activity1");
            provider.TimeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(provider.TimeLogsManager).Method("GetActivities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity("activity1", DateTime.Now, TimeSpan.Parse("0:30"))
                })));
            Stub.On(provider.TimeLogsManager).GetProperty("AvailableDays").Will(Return.Value(new List<DateTime>((new DateTime[] { DateTime.Now }))));
            Stub.On(provider.TaskCollection).Method("GetTask").With("task1").Will(Return.Value(task));

            provider.UpdateDataTableForTask("task1");
            DataTable table = provider.Data;
            
            Assert.AreEqual(1, table.Rows.Count);
            DataRow row = table.Rows[0];
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), row["Day"]);
            Assert.AreEqual("0:30", row["Spent"]);
        }
        [Test]
        public void TableIsEmptiedWhenActivityIsChanged()
        {
            string uniqueActivity = "todays" + DateTime.Now;
            provider.TimeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(provider.TimeLogsManager).Method("GetActivities").Will(Return.Value(new List<IActivity>(
                new IActivity[] {
                    new Activity(uniqueActivity, DateTime.Now, TimeSpan.Parse("0:30"))
                })));
            Stub.On(provider.TimeLogsManager).GetProperty("AvailableDays").Will(Return.Value(new List<DateTime>((new DateTime[] { DateTime.Now }))));

            provider.UpdateDataTableForActivity(uniqueActivity);
            provider.UpdateDataTableForActivity("unexistent one");
            Assert.AreEqual(0, provider.Data.Rows.Count);
        }
        [Test]
        public void NoRowsIfSpentIsZero()
        {
            provider.UpdateDataTableForActivity("No data");
            Assert.AreEqual(0, provider.Data.Rows.Count);
        }
        [Test]
        public void LatestActivitiesCallsHistory()
        {
            provider.ActivitiesHistory = NewMock<IActivitiesHistory>();
            Expect.Once.On(provider.ActivitiesHistory).GetProperty("LatestActivities").
                Will(Return.Value(new string[] { "test" }));
            Assert.AreEqual(new string[] { "test" }, provider.LatestActivities);
        }
        [Test]
        public void HistoryActivitiesCallsHistory()
        {
            provider.ActivitiesHistory = NewMock<IActivitiesHistory>();
            Expect.Once.On(provider.ActivitiesHistory).GetProperty("Activities").
                Will(Return.Value(new string[] { "test" }));
            Assert.AreEqual(new string[] { "test" }, provider.HistoryActivities);
        }
        [Test]
        public void TasksDoesNotThrowsException()
        {
            provider.TaskCollection = NewMock<ITaskCollection>();
            string[] test = new string[] { };
            Expect.Once.On(provider.TaskCollection).Method("GetAllTasksNames").Will(Return.Value(test));
            Assert.AreSame(test, provider.Tasks);
        }
        [Test]
        public void ConstructorInitializesActivitiesHistory()
        {
            Assert.IsNotNull(provider.ActivitiesHistory);
        }
        [Test]
        public void ApplySettings()
        {
            ISettings settings = NewMock<ISettings>();
            Stub.On(settings).GetProperty("MaxActivitiesInHistory").Will(Return.Value(13));
            Stub.On(settings).GetProperty("ActivitiesNumberInTray").Will(Return.Value(5));
            
            provider.ApplySettings(settings);

            Assert.AreEqual(5, provider.ActivitiesHistory.LatestSize);
            Assert.AreEqual(13, provider.ActivitiesHistory.Size);
        }
        [Test]
        public void TasksSummaryData()
        {
            provider.TasksSummary = NewMock<ITasksSummary>();
            DataTable test = new DataTable("test");
            Expect.Once.On(provider.TasksSummary).GetProperty("Data").Will(Return.Value(test));

            object data = provider.TasksSummaryData;

            Assert.AreEqual(test, data);
            VerifyAllExpectationsHaveBeenMet();
        }
    }
}
