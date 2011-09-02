using System;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Reports
{
    [TestFixture]
    public class EfficiencyCalculatorTest: Mockery
    {
        [TearDown]
        public void TearDown()
        {
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void Efficiency()
        {
            IWorkingTimeManager workingTime = NewMock<IWorkingTimeManager>();
            Expect.Once.On(workingTime).GetProperty("WorkingTasksTime").Will(Return.Value(TimeSpan.Parse("0:45")));
            Expect.Once.On(workingTime).GetProperty("TimeOnWork").Will(Return.Value(TimeSpan.Parse("1:00")));
            
            EfficiencyCalculator efficiencyCalculator = new EfficiencyCalculator(workingTime);
            
            Assert.AreEqual(0.75, efficiencyCalculator.Efficiency);
        }
        [Test]
        public void EfficiencyTimeOnWorkIsZero()
        {
            IWorkingTimeManager workingTime = NewMock<IWorkingTimeManager>();
            Expect.Once.On(workingTime).GetProperty("TimeOnWork").Will(Return.Value(TimeSpan.Zero));

            EfficiencyCalculator efficiencyCalculator = new EfficiencyCalculator(workingTime);

            Assert.AreEqual(0, efficiencyCalculator.Efficiency);
        }
    }
}
