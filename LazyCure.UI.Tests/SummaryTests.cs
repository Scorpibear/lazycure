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
        [TearDown]
        public void TearDown()
        {
            this.summary = null;
        }
        [Test]
        public void IfOneDayButtonCheckedOthersUnchecked()
        {
            summary.ClickDayButton(Summary.Period.LastMonth);
            summary.ClickDayButton(Summary.Period.PrevWeek);

            Assert.AreEqual(Summary.Period.PrevWeek, summary.GetCheckedDayButtonPeriod());
        }
        [Test]
        public void TodayIsCheckedByDefault()
        {
            Assert.AreEqual(Summary.Period.Today, summary.GetCheckedDayButtonPeriod());
        }
        [Test]
        public void DoubleCheckKeepsChecked()
        {
            summary.ClickDayButton(Summary.Period.ThisWeek);
            summary.ClickDayButton(Summary.Period.ThisWeek);

            Assert.AreEqual(Summary.Period.ThisWeek, summary.GetCheckedDayButtonPeriod());
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
            summary.ClickDayButton(Summary.Period.Yesterday);

            Assert.AreEqual(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), summary.GetToDate(), "to");
            Assert.AreEqual(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), summary.GetFromDate(), "from");
        }
        [Test]
        public void ThisWeekDropDownsUpdated()
        {
            summary.ClickDayButton(Summary.Period.ThisWeek);

            DateTime monday, sunday;

            monday = DateTime.Now.AddDays(1-(int)DateTime.Now.DayOfWeek);
            sunday = monday.AddDays(6);

            Assert.AreEqual(Format.Date(monday), summary.GetFromDate(), "from");
            Assert.AreEqual(Format.Date(sunday), summary.GetToDate(), "to");
        }
        [Test]
        public void DayButtonsWorkWithLocalization()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
            Summary summary = new Summary(null, null);

            summary.ClickDayButton(Summary.Period.LastMonth);
            
            Assert.AreEqual(Format.Date(DateTime.Now.AddDays(-30)), summary.GetFromDate());
        }
        [Test]
        public void PrevWeekDropDownsUpdated()
        {
            summary.ClickDayButton(Summary.Period.PrevWeek);

            DateTime monday, sunday;

            monday = DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek - 7);
            sunday = monday.AddDays(6);

            Assert.AreEqual(Format.Date(monday), summary.GetFromDate(), "from");
            Assert.AreEqual(Format.Date(sunday), summary.GetToDate(), "to");
        }
        [Test]
        public void LastMonthDropDownsUpdated()
        {
            summary.ClickDayButton(Summary.Period.LastMonth);

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
            Stub.On(lcdriver).GetProperty("WorkingTimeIntervalsData").Will(Return.Value(null));
            Stub.On(lcdriver).GetProperty("PossibleWorkInterruptionDuration").Will(Return.Value(TimeSpan.Zero));
            Stub.On(lcdriver).SetProperty("PossibleWorkInterruptionDuration");
            IHistoryDataProvider dataProvider = NewMock<IHistoryDataProvider>();
            Stub.On(lcdriver).GetProperty("HistoryDataProvider").Will(Return.Value(dataProvider));
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(dataProvider).GetProperty("ActivitiesSummaryData").Will(Return.Value(null));
            Stub.On(dataProvider).GetProperty("TasksSummaryData").Will(Return.Value(null));
            Stub.On(dataProvider).GetProperty("TimeLogsManager").Will(Return.Value(timeLogsManager));
            Stub.On(dataProvider).Method("SetSummaryPeriod").WithAnyArguments();
            Expect.Once.On(timeLogsManager).GetProperty("AvailableDays").Will(Return.Value(new List<DateTime>(new DateTime[]{DateTime.Parse("2000-12-31")})));
            
            summary = new Summary(lcdriver, null);

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void ActivitiesForMonthAreRequested()
        {
            ILazyCureDriver lcdriver = NewMock<ILazyCureDriver>();
            DateTime from, to;
            to = DateTime.Parse(Format.Date(DateTime.Now));
            from = to.AddDays(-30);
            Stub.On(lcdriver).GetProperty("WorkingTimeIntervalsData").Will(Return.Value(null));
            Stub.On(lcdriver).GetProperty("PossibleWorkInterruptionDuration").Will(Return.Value(TimeSpan.Zero));
            Stub.On(lcdriver).SetProperty("PossibleWorkInterruptionDuration");
            IHistoryDataProvider dataProvider = NewMock<IHistoryDataProvider>();
            Stub.On(lcdriver).GetProperty("HistoryDataProvider").Will(Return.Value(dataProvider));
            Expect.Once.On(dataProvider).GetProperty("ActivitiesSummaryData").Will(Return.Value(null));
            Stub.On(dataProvider).GetProperty("TasksSummaryData").Will(Return.Value(null));
            Stub.On(dataProvider).GetProperty("TimeLogsManager").Will(Return.Value(null));
            Expect.Once.On(dataProvider).Method("SetSummaryPeriod").With(to, to);
            summary = new Summary(lcdriver, null);
            Expect.Once.On(dataProvider).Method("SetSummaryPeriod").With(from, to);
            Expect.Once.On(dataProvider).GetProperty("ActivitiesSummaryData").Will(Return.Value(null));

            summary.ClickDayButton(Summary.Period.LastMonth);

            VerifyAllExpectationsHaveBeenMet();
        }
    }
}
