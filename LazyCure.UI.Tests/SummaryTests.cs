using System;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;
using System.Collections.Generic;

namespace LifeIdea.LazyCure.UI
{
    [TestFixture]
    public class SummaryTests: Mockery
    {
        private Summary summary;
        [SetUp]
        public void SetUp()
        {
            this.summary = new Summary(null, null);
        }
        [Test]
        public void IfOneDayButtonCheckedOthersUnchecked()
        {
            summary.ClickDayButton("Last Month");
            summary.ClickDayButton("Prev Week");

            Assert.AreEqual("Prev Week", summary.GetCheckedDayButtonsText());
        }
        [Test]
        public void TodayIsCheckedByDefault()
        {
            Assert.AreEqual("Today", summary.GetCheckedDayButtonsText());
        }
        [Test]
        public void DoubleCheckKeepsChecked()
        {
            summary.ClickDayButton("This Week");
            summary.ClickDayButton("This Week");

            Assert.AreEqual("This Week", summary.GetCheckedDayButtonsText());
        }
        [Test]
        public void CurrentDateIsDisplayedInFrom()
        {
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), summary.GetFromDate());
        }
        [Test]
        public void CurrentDateIsDisplayedInTo()
        {
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), summary.GetToDate());
        }
        [Test]
        public void YesterdayIsDisplayedInFromAndTo()
        {
            summary.ClickDayButton("Yesterday");

            Assert.AreEqual(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), summary.GetToDate(), "to");
            Assert.AreEqual(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), summary.GetFromDate(), "from");
        }
        [Test]
        public void ThisWeekDropDownsUpdated()
        {
            summary.ClickDayButton("This Week");

            DateTime monday, sunday;

            monday = DateTime.Now.AddDays(1-(int)DateTime.Now.DayOfWeek);
            sunday = monday.AddDays(6);

            Assert.AreEqual(Format.Date(monday), summary.GetFromDate(), "from");
            Assert.AreEqual(Format.Date(sunday), summary.GetToDate(), "to");
        }
        [Test]
        public void PrevWeekDropDownsUpdated()
        {
            summary.ClickDayButton("Prev Week");

            DateTime monday, sunday;

            monday = DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek - 7);
            sunday = monday.AddDays(6);

            Assert.AreEqual(Format.Date(monday), summary.GetFromDate(), "from");
            Assert.AreEqual(Format.Date(sunday), summary.GetToDate(), "to");
        }
        [Test]
        public void LastMonthDropDownsUpdated()
        {
            summary.ClickDayButton("Last Month");

            DateTime from, to;

            to = DateTime.Now;
            from = DateTime.Now.AddDays(-30);

            Assert.AreEqual(Format.Date(from), summary.GetFromDate(), "from");
            Assert.AreEqual(Format.Date(to), summary.GetToDate(), "to");
        }
        [Test]
        public void FromDatePopupUsingLazyCureProviderOfAvailableDates()
        {
            ILazyCureDriver lcdriver = NewMock<ILazyCureDriver>();
            Stub.On(lcdriver).GetProperty("ActivitiesSummaryData").Will(Return.Value(null));
            Stub.On(lcdriver).GetProperty("TasksSummaryData").Will(Return.Value(null));
            Stub.On(lcdriver).GetProperty("WorkingTimeIntervalsData").Will(Return.Value(null));
            Stub.On(lcdriver).GetProperty("PossibleWorkInterruptionDuration").Will(Return.Value(TimeSpan.Zero));
            Stub.On(lcdriver).SetProperty("PossibleWorkInterruptionDuration");
            IHistoryDataProvider dataProvider = NewMock<IHistoryDataProvider>();
            Stub.On(lcdriver).GetProperty("HistoryDataProvider").Will(Return.Value(dataProvider));
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(dataProvider).GetProperty("TimeLogsManager").Will(Return.Value(timeLogsManager));
            Expect.Once.On(timeLogsManager).GetProperty("AvailableDays").Will(Return.Value(new List<DateTime>(new DateTime[]{DateTime.Parse("2000-12-31")})));
            
            summary = new Summary(lcdriver, null);

            VerifyAllExpectationsHaveBeenMet();
        }
    }
}
