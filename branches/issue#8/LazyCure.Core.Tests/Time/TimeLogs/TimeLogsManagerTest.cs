using System;
using NMock2;
using NUnit.Framework;
using LifeIdea.LazyCure.Core.IO;
using System.Collections.Generic;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time.TimeLogs
{
    [TestFixture]
    public class TimeLogsManagerTest: Mockery
    {
        [Test]
        public void GetActivitiesUsesActivitiesFromTimeLogFromFileManager()
        {
            DateTime today = DateTime.Now;
            var activities = new List<IActivity>();
            ITimeLog timeLog = NewMock<ITimeLog>();
            Stub.On(timeLog).GetProperty("Activities").Will(Return.Value(activities));
            IFileManager fileManager = NewMock<IFileManager>();
            Expect.Once.On(fileManager).Method("GetTimeLog").With(today).Will(Return.Value(timeLog));
            var timeLogsManager = new TimeLogsManager(fileManager);

            Assert.AreSame(activities, timeLogsManager.GetActivities(today));
            
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void AvailableDaysReturnsEmptyArrayIfNothingIsFound()
        {
            var timeLogsManager = new TimeLogsManager(null);
            Assert.IsEmpty(timeLogsManager.AvailableDays);
        }
        [Test]
        public void AvailableDaysReturnsDayForWhichTimeLogExist()
        {
            IFileManager fileManager = NewMock<IFileManager>();
            Stub.On(fileManager).GetProperty("AllTimeLogDates").Will(Return.Value(new List<DateTime>(new DateTime[] { DateTime.Parse("2011-12-13") })));
            var timeLogsManager = new TimeLogsManager(fileManager);
            var availableDays = timeLogsManager.AvailableDays;
            Assert.AreEqual(1, availableDays.Count);
            Assert.AreEqual(DateTime.Parse("2011-12-13"), availableDays[0]);
        }
    }
}
