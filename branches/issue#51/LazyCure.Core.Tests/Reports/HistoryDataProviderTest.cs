using System;
using System.Data;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Shared.Interfaces;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Time;

namespace LifeIdea.LazyCure.Core.Reports
{
    public class HistoryDataProviderTest: Mockery
    {
        HistoryDataProvider provider;
        [SetUp]
        public void SetUp()
        {
            provider = new HistoryDataProvider();
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
            DataTable table = provider.DataTable;
            
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
            Assert.AreEqual(0, provider.DataTable.Rows.Count);
        }
        [Test]
        public void SummarizeSpentForActivityInTimeLog()
        {
            var activities = new List<IActivity>(
                new IActivity[] {
                    new Activity("activity1", DateTime.Now, TimeSpan.Parse("0:10")),
                    new Activity("activity1", DateTime.Now, TimeSpan.Parse("0:15"))
                });
            TimeSpan spent = provider.SummarizeSpentForActivity(activities, "activity1");
            Assert.AreEqual(TimeSpan.Parse("0:25"), spent);
        }
        [Test]
        public void NoRowsIfSpentIsZero()
        {
            provider.UpdateDataTableForActivity("No data");
            Assert.AreEqual(0, provider.DataTable.Rows.Count);
        }
    }
}
