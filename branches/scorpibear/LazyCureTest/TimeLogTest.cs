using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class TimeLogTest
    {
        private Mockery mocks;
        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
        }
        [Test]
        public void SwitchTo()
        {
            string nextActivityName = "test next task";
            ITimeSystem mockTimeSystem = mocks.NewMock<ITimeSystem>();
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Now));
            TimeLog timeLog = new TimeLog(mockTimeSystem, "first");
            Assert.AreEqual(nextActivityName, timeLog.SwitchTo(nextActivityName).Name);
            Assert.AreEqual(nextActivityName, timeLog.CurrentActivity.Name);
        }
    }
}
