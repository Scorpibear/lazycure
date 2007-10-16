using System;
using NUnit.Framework;
using NMock2;
using System.Data;

namespace LifeIdea.LazyCure.Core.Time
{
    [TestFixture]
    public class TimeLogCheckEndTimeTest
    {
        private Mockery mocks;
        private TimeLog timeLog;
        private readonly DateTime startTime = DateTime.Parse("2125-06-30 05:00:00");
        private readonly DateTime endTime = DateTime.Parse("2125-06-30 5:06:43");
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