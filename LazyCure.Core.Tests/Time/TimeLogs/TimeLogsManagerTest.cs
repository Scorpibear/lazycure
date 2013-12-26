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
            ITimeLogsFileManager fileManager = NewMock<ITimeLogsFileManager>();
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
            ITimeLogsFileManager fileManager = NewMock<ITimeLogsFileManager>();
            Stub.On(fileManager).GetProperty("AllTimeLogDates").Will(Return.Value(new List<DateTime>(new DateTime[] { DateTime.Parse("2011-12-13") })));
            var timeLogsManager = new TimeLogsManager(fileManager);
            var availableDays = timeLogsManager.AvailableDays;
            Assert.AreEqual(1, availableDays.Count);
            Assert.AreEqual(DateTime.Parse("2011-12-13"), availableDays[0]);
        }
        [Test]
        public void GetTimeLogsReturnsListOfTimeLogsFromFileManager()
        {
            DateTime from = DateTime.Parse("2020-01-01");
            DateTime to = DateTime.Parse("2020-01-02");
            ITimeLogsFileManager fileManager = NewMock<ITimeLogsFileManager>();
            Stub.On(fileManager).GetProperty("AllTimeLogDates").Will(Return.Value(new List<DateTime>(new DateTime[] { from, to })));
            Stub.On(fileManager).Method("GetTimeLog").With(from).Will(Return.Value(NewMock<ITimeLog>()));
            Stub.On(fileManager).Method("GetTimeLog").With(to).Will(Return.Value(NewMock<ITimeLog>()));
            var timeLogsManager = new TimeLogsManager(fileManager);
            Assert.AreEqual(2, timeLogsManager.GetTimeLogs(from, to).Count);
        }
        [Test]
        public void ActivateTimeLogCallsSave()
        {
            ITimeLogsFileManager fileManager = NewMock <ITimeLogsFileManager>();
            
            Stub.On(fileManager).Method("GetTimeLog").Will(Return.Value(NewMock<ITimeLog>()));
            var timeLogsManager = new TimeLogsManager(fileManager);
            Expect.Once.On(fileManager).Method("SaveTimeLog").Will(Return.Value(true));

            timeLogsManager.ActiveTimeLog = NewMock<ITimeLog>();
            timeLogsManager.ActivateTimeLog(DateTime.Now);

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void ActivateTimeLogCreatesNewTimeLogIfItsNotInFileSystem()
        {
            var fileManager = NewMock<ITimeLogsFileManager>();
            Expect.Once.On(fileManager).Method("GetTimeLog").Will(Return.Value(null));
            Stub.On(fileManager).Method("SaveTimeLog").Will(Return.Value(true));
            var timeLogsManager = new TimeLogsManager(fileManager);

            var timeLog = timeLogsManager.ActivateTimeLog(DateTime.Now);

            Assert.IsNotNull(timeLog);
        }
        [Test]
        public void GetTimeLogsUseActiveTimeLogInsteadOfFileSystemFileExists()
        {
            var date = DateTime.Today;
            var timeLog = new TimeLog(date);
            var fileManager = NewMock<ITimeLogsFileManager>();
            var timeLogsManager = new TimeLogsManager(fileManager);
            Stub.On(fileManager).GetProperty("AllTimeLogDates").Will(Return.Value(new List<DateTime> { date }));
            Expect.Never.On(fileManager).Method("GetTimeLog").With(date);
            
            timeLogsManager.ActiveTimeLog = timeLog;

            var timeLogs = timeLogsManager.GetTimeLogs(date, date);

            Assert.AreEqual(1, timeLogs.Count, "number of TimeLogs returned");
            Assert.AreSame(timeLog, timeLogs[0]);
        }
        [Test]
        public void GetTimeLogsUseActiveTimeLogInsteadOfFileSystemFileDoesNotExist()
        {
            var date = DateTime.Today;
            var timeLog = new TimeLog(date);
            var fileManager = NewMock<ITimeLogsFileManager>();
            var timeLogsManager = new TimeLogsManager(fileManager);
            Stub.On(fileManager).GetProperty("AllTimeLogDates").Will(Return.Value(new List<DateTime> { DateTime.Parse("2000-01-01") }));
            Expect.Never.On(fileManager).Method("GetTimeLog").With(date);

            timeLogsManager.ActiveTimeLog = timeLog;

            var timeLogs = timeLogsManager.GetTimeLogs(date, date);

            Assert.AreEqual(1, timeLogs.Count, "number of TimeLogs returned");
            Assert.AreSame(timeLog, timeLogs[0]);
        }
        [Test]
        public void SaveActiveTimeLogUsesFileManager()
        {
            var fileManager = NewMock<ITimeLogsFileManager>();
            ITimeLog activeTimeLog = new TimeLog(DateTime.Today);
            Expect.Once.On(fileManager).Method("SaveTimeLog").With(activeTimeLog).Will(Return.Value(true));
            TimeLogsManager timeLogsManager = new TimeLogsManager(fileManager);
            timeLogsManager.ActiveTimeLog = activeTimeLog;
            timeLogsManager.SaveActiveTimeLog();
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SaveActiveTimeLogDoesNotUserFileManagerIfActiveTimeLogIsNull()
        {
            var fileManager = NewMock<ITimeLogsFileManager>();
            Expect.Never.On(fileManager);
            TimeLogsManager timeLogsManager = new TimeLogsManager(fileManager);
            timeLogsManager.ActiveTimeLog = null;

            timeLogsManager.SaveActiveTimeLog();

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SaveActiveTimeLogWithNullFileManager()
        {
            var timeLogsManager = new TimeLogsManager(null);
            Assert.False(timeLogsManager.SaveActiveTimeLog());
        }
    }
}
