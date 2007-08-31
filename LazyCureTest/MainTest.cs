using System;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.UI;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    //does not work
    //[TestFixture]
    public class MainTest
    {
        private Main form;
        [SetUp]
        public void SetUp()
        {
            Mockery mocks = new Mockery();
            ILazyCureDriver mockDriver = mocks.NewMock<ILazyCureDriver>();
            IActivity mockActivity = mocks.NewMock<IActivity>();
            Stub.On(mockActivity).GetProperty("Name");
            Stub.On(mockActivity).GetProperty("StartTime").Will(Return.Value(DateTime.Parse("2126-11-18")));
            Stub.On(mockDriver).GetProperty("CurrentActivity").Will(Return.Value(mockActivity));
            Stub.On(mockDriver).GetProperty("TimeLogData");
            Stub.On(mockDriver).GetProperty("ActivitiesSummaryData");
            Stub.On(mockDriver).GetProperty("TimeLogDate").Will(Return.Value("2126-11-18"));
            Stub.On(mockDriver).GetProperty("LatestActivities").Will(Return.Value(new string[] { }));
            form = new Main(mockDriver);
        }
        [Test]
        public void MinimizeBoxExists()
        {
            Assert.IsTrue(form.MinimizeBox);
        }
        [Test]
        public void CaptionDots()
        {
            Assert.AreEqual(2, form.Text.Split('.').Length, "only one dot");
        }
    }
}
