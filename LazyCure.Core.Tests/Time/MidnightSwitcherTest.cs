using System;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    [TestFixture]
    public class MidnightSwitcherTest: Mockery
    {
        MidnightSwitcher midnightSwitcher;

        [SetUp]
        public void SetUp()
        {
            midnightSwitcher = new MidnightSwitcher();
        }

        [Test]
        public void AfterMidnightActivityIsCut()
        {
            IActivity activity = NewMock<IActivity>();
            Stub.On(activity).GetProperty("Start").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
            Stub.On(activity).GetProperty("End").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:10")));
            using (Ordered)
            {
                Expect.Once.On(activity).SetProperty("Duration").To(TimeSpan.Parse("0:00:05"));
                Expect.Once.On(activity).SetProperty("Start").To(DateTime.Parse("2008-08-08"));
                Expect.Once.On(activity).SetProperty("Duration").To(TimeSpan.Parse("0:00:10"));
            }

            midnightSwitcher.PerformMidnightCorrection(activity, null);

            VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void PerformMidnightCorrectionCallsActivateTimeLog()
        {
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog");
            Expect.Once.On(timeLogsManager).Method("ActivateTimeLog").Will(Return.Value(null));
            IActivity activity = NewMock<IActivity>();
            Stub.On(activity).GetProperty("Start").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
            Stub.On(activity).GetProperty("End").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:10")));
            Stub.On(activity).SetProperty("Start");
            Stub.On(activity).SetProperty("Duration");

            midnightSwitcher.PerformMidnightCorrection(activity, timeLogsManager);

            VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void PerformMidnightCorrectionWithGapBeetweenDates()
        {
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog");
            Expect.Once.On(timeLogsManager).Method("ActivateTimeLog").With(DateTime.Parse("2020-10-10")).Will(Return.Value(null));
            IActivity activity = NewMock<IActivity>();
            Stub.On(activity).GetProperty("Start").Will(Return.Value(DateTime.Parse("2020-10-05 23:59:55")));
            Stub.On(activity).GetProperty("End").Will(Return.Value(DateTime.Parse("2020-10-10 0:00:10")));
            Stub.On(activity).SetProperty("Start");
            Stub.On(activity).SetProperty("Duration");

            midnightSwitcher.PerformMidnightCorrection(activity, timeLogsManager);

            VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void PerformMidnightCorrectionForTheSameDate()
        {
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            Expect.Never.On(timeLogsManager).Method("ActivateTimeLog");
            IActivity activity = NewMock<IActivity>();
            Stub.On(activity).GetProperty("Start").Will(Return.Value(DateTime.Parse("2020-11-18 13:59:55")));
            Stub.On(activity).GetProperty("End").Will(Return.Value(DateTime.Parse("2020-11-18 15:00:10")));

            midnightSwitcher.PerformMidnightCorrection(activity, timeLogsManager);

            VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void PerformMidnightCorrectionAddActivityToTimeLog()
        {
            ITimeLogsManager timeLogsManager = NewMock<ITimeLogsManager>();
            ITimeLog timeLog = NewMock<ITimeLog>();
            IActivity activity = NewMock<IActivity>();
            Expect.Once.On(timeLog).Method("AddActivity").With(activity);
            Stub.On(timeLogsManager).GetProperty("ActiveTimeLog").Will(Return.Value(timeLog));
            Stub.On(timeLogsManager).Method("ActivateTimeLog");
            Stub.On(activity).GetProperty("Start").Will(Return.Value(DateTime.Parse("2008-08-07 23:59:55")));
            Stub.On(activity).GetProperty("End").Will(Return.Value(DateTime.Parse("2008-08-08 0:00:10")));
            Stub.On(activity).SetProperty("Start");
            Stub.On(activity).SetProperty("Duration");

            midnightSwitcher.PerformMidnightCorrection(activity, timeLogsManager);

            VerifyAllExpectationsHaveBeenMet();
        }
    }
}
