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
    }
}
