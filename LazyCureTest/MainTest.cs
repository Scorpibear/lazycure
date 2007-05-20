using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.UI;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    [TestFixture]
    public class MainTest
    {
        [Test]
        public void MinimizeBoxExists()
        {
            Mockery mocks = new Mockery();
            ILazyCureDriver mockDriver = mocks.NewMock<ILazyCureDriver>();
            IActivity mockActivity = mocks.NewMock<IActivity>();
            Stub.On(mockActivity).GetProperty("Name");
            Stub.On(mockActivity).GetProperty("StartTime").Will(Return.Value(new DateTime()));
            Stub.On(mockDriver).GetProperty("CurrentActivity").Will(Return.Value(mockActivity));
            Stub.On(mockDriver).GetProperty("TimeLogData");
            Stub.On(mockDriver).GetProperty("ActivitiesSummaryData");
            Main form = new Main(mockDriver);
            Assert.IsTrue(form.MinimizeBox);
        }
    }
}
