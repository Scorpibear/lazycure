using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;
using System.Data;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class TimeLogCheckEndTimeTest
    {
        private Mockery mocks;
        private TimeLog timeLog;
        private DateTime startTime = DateTime.Parse("2125-06-30 05:00:00");
        private DateTime endTime = DateTime.Parse("2125-06-30 5:06:43");
        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            using (mocks.Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(endTime));
            }
            timeLog = new TimeLog(mockTimeSystem, "first");
        }
        [Test]
        public void FinishActivityUseNowOnce()
        {
            timeLog.FinishActivity("activityName", "next");
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void TestTimeLogContentAfterSave()
        {
            MockWriter mockWriter = new MockWriter();
            timeLog.Save(mockWriter);
            Assert.IsTrue(mockWriter.Content.Contains("first"), "activity");
            Assert.IsTrue(mockWriter.Content.Contains("5:00:00"), "start");
            Assert.IsTrue(mockWriter.Content.Contains("0:06:43"), "duration");
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SaveTimeLogXmlStructure()
        {
            string[] sExpectedContent = {"<?xml version=", "<LazyCureData>", "<Records>",
                "<Activity>first</Activity>","<Begin>5:00:00</Begin>","<Duration>0:06:43</Duration>",
                "</Records>","</LazyCureData>"};
            MockWriter mockWriter = new MockWriter();
            timeLog.Save(mockWriter);
            foreach (string s in sExpectedContent)
                Assert.IsTrue(mockWriter.Content.Contains(s), s);
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void DataSimpleRecord()
        {
            timeLog.SwitchTo("second");
            DataRow firstRow = timeLog.Data.Rows[0];
            Assert.AreEqual("first", firstRow["Activity"]);
            Assert.AreEqual(startTime, firstRow["Start"]);
            Assert.AreEqual(endTime - startTime, firstRow["Duration"]);
            Assert.AreEqual(endTime, firstRow["End"]);
        }
    }
}
