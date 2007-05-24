using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;
using System.IO;
using System.Data;

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
            Log.TextWriter = new MockWriter();
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

            Assert.IsTrue(driver.SaveTimeLog());
            Assert.Contains(filename,Directory.GetFiles(folder));
        }
        [Test]
        public void NullTimeLogs()
        {
            driver.TimeLogsFolder = "";
            Assert.IsFalse(driver.SaveTimeLog());
        }

        [Test]
        public void SaveTimeLogXmlStructure()
        {
            string folder = @"c:\temp\LazyCure\test\TimeLogs";
            string filename = folder + @"\2015-09-26.timelog";
            string[] sExpectedContent = {"<?xml version=", "<LazyCureData>", "<Records>",
                "<Activity>arrangement</Activity>","<Begin>9:07:13</Begin>","<Duration>0:04:40</Duration>",
                "</Records>","</LazyCureData>"};
            DateTime startTime = DateTime.Parse("2015-09-26 9:07:13");
            DateTime endTime = DateTime.Parse("2015-09-26 9:11:53");
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(endTime));
            }
            driver = new Driver(mockTimeSystem);
            driver.FinishActivity("arrangement","second");
            driver.TimeLogsFolder = folder;
            Assert.IsTrue(driver.SaveTimeLog());
            StreamReader stream = File.OpenText(filename);
            string sRealContent = stream.ReadToEnd();
            Console.WriteLine(sRealContent);
            stream.Close();
            foreach (string s in sExpectedContent)
                Assert.IsTrue(sRealContent.Contains(s),s);
            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void LoadTimeLog()
        {
            string sContent = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData><Records>"+
                "<Activity>changed</Activity><Begin>14:35:02</Begin><Duration>0:00:07</Duration>"+
                "</Records></LazyCureData>";
            DateTime date = DateTime.Parse("2126-11-18");
            string folder = @"c:\temp\LazyCure\test\TimeLogs";
            string filename = folder + @"\2126-11-18.timelog";
            if (File.Exists(filename))
                File.Delete(filename);
            StreamWriter writer = File.CreateText(filename);
            writer.Write(sContent);
            writer.Close();
            driver.TimeLogsFolder = folder;
            Assert.IsTrue(driver.LoadTimeLog(date),"log loaded");
            DataRow row = (driver.TimeLogData as DataTable).Rows[0];
            Assert.AreEqual("changed", row["Activity"],"activity name match");
            Assert.AreEqual(DateTime.Parse("14:35:02"), row["Start"], "start match");
            Assert.AreEqual(TimeSpan.Parse("0:00:07"), row["Duration"], "duration match");
        }
        [Test]
        public void LoadTwoActivites()
        {
            string sContent = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData>"+
                "<Records>"+
                "<Activity>sleep</Activity><Begin>0:00:00</Begin><Duration>5:00:00</Duration>" +
                "</Records>" +
                "<Records>" +
                "<Activity>clean</Activity><Begin>5:00:00</Begin><Duration>0:07:00</Duration>" +
                "</Records>"+
                "</LazyCureData>";
            DateTime date = DateTime.Parse("2025-12-31");
            string folder = @"c:\temp\LazyCure\test\TimeLogs";
            string filename = folder + @"\2025-12-31.timelog";
            if (File.Exists(filename))
                File.Delete(filename);
            StreamWriter writer = File.CreateText(filename);
            writer.Write(sContent);
            writer.Close();
            driver.TimeLogsFolder = folder;
            driver.LoadTimeLog(date);
            DataRow row1 = (driver.TimeLogData as DataTable).Rows[0];
            DataRow row2 = (driver.TimeLogData as DataTable).Rows[1];
            Assert.AreEqual("sleep", row1["Activity"], "activity name match");
            Assert.AreEqual(DateTime.Parse("0:00:00"), row1["Start"], "start match");
            Assert.AreEqual(TimeSpan.Parse("5:00:00"), row1["Duration"], "duration match");
            Assert.AreEqual("clean", row2["Activity"], "activity name match");
            Assert.AreEqual(DateTime.Parse("5:00:00"), row2["Start"], "start match");
            Assert.AreEqual(TimeSpan.Parse("0:07:00"), row2["Duration"], "duration match");
        }
        [Test]
        public void LoadUnexistingLog()
        {
            Assert.IsFalse(driver.LoadTimeLog(DateTime.Parse("2012-03-16")));
        }
        [Test]
        public void LoadBrokenXml()
        {
            string sBrokenContent = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData><Records>" +
                "<Activity>changed<Activity><Begin>14:35:02</Begin><Duration>0:00:07</Duration>" +
                "</Records></LazyCureData>";
            DateTime date = DateTime.Parse("2126-11-18");
            string folder = @"c:\temp\LazyCure\test\TimeLogs";
            string filename = folder + @"\2126-11-18.timelog";
            if (File.Exists(filename))
                File.Delete(filename);
            StreamWriter writer = File.CreateText(filename);
            writer.Write(sBrokenContent);
            writer.Close();
            driver.TimeLogsFolder = folder;
            Assert.IsFalse(driver.LoadTimeLog(date));
        }
    }
}
