using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class DriverTest
    {
        Driver driver;
        private Mockery mocks;
        [SetUp]
        public void SetUp()
        {
            driver = new Driver();
            mocks = new Mockery();
        }
        [Test]
        public void Switch()
        {
            string nextActivityName = "test next task";
            Assert.AreEqual(nextActivityName, driver.SwitchTo(nextActivityName).Name);
            Assert.AreEqual(nextActivityName, driver.CurrentActivity.Name);
        }
        [Test]
        public void DriverStartsActivity()
        {
            IActivity activity1, activity2;
            activity1 = driver.SwitchTo("activity2");
            activity2 = driver.SwitchTo("activity3");

            Assert.AreNotSame(activity1, activity2);
        }
        [Test]
        public void CurrentTaskStartTime()
        {
            DateTime startTime=DateTime.Parse("2005-05-05 05:05:05");
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
            
            driver = new Driver(mockTimeSystem);
            Assert.AreEqual(startTime, driver.CurrentActivity.StartTime);
        }
        [Test]
        public void CurrentActivityDuration()
        {
            TimeSpan duration = TimeSpan.FromMinutes(15);
            DateTime startTime = DateTime.Parse("2006-06-06 06:06:06");
            DateTime endTime = startTime + duration;

            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();

            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(endTime));
            }
            driver = new Driver(mockTimeSystem);
            Assert.AreEqual(duration, driver.CurrentActivity.Duration);
        }
        [Test]
        public void ReturnsPreviousActivity()
        {
            driver.SwitchTo("task2");
            Assert.AreEqual(driver.FirstActivityName, driver.PreviousActivity.Name);
        }
        [Test]
        public void SaveTimeLog()
        {
            string folder = @"c:\temp\LazyCure\test\TimeLogs";
            string filename = folder + @"\2007-11-18.timelog";
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }

            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse("2007-11-18 5:00:00")));

            driver = new Driver(mockTimeSystem);
            driver.TimeLogsFolder = folder;
            driver.SaveTimeLog();

            Assert.Contains(filename,Directory.GetFiles(folder));
        }
        [Test]
        public void FinishActivity()
        {
            string finishedActivity = "prev";
            string currentActivity = "next";
            driver.FinishActivity(finishedActivity,currentActivity);
            Assert.AreEqual(finishedActivity, driver.PreviousActivity.Name,"previous check");
            Assert.AreEqual(currentActivity, driver.CurrentActivity.Name,"current check");
        }
        [Test]
        public void FinishedActivityReusesCurrentActivity()
        {
            IActivity currentActivity = driver.CurrentActivity;
            driver.FinishActivity("prev","next");
            Assert.AreSame(currentActivity, driver.PreviousActivity,"last current now is previous");
            Assert.AreNotSame(driver.PreviousActivity, driver.CurrentActivity,"current and previous different");
        }
    }
}
