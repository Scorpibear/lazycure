using System;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Shared.Tools
{
    [TestFixture]
    public class FormatTest
    {
        [Test]
        public void DurationToString()
        {
            Assert.AreEqual("0:03:12", Format.Duration(TimeSpan.Parse("0:03:12")));
        }
        [Test]
        public void DurationIsTruncatedInString()
        {
            Assert.AreEqual("1:22:33", Format.Duration(TimeSpan.Parse("1:22:33.666")));
        }
        [Test]
        public void SimpleShortDuration()
        {
            Assert.AreEqual("8:00",Format.ShortDuration(TimeSpan.Parse("8:00:00")));
        }
        [Test]
        public void LongShortDuration()
        {
            Assert.AreEqual("23:59", Format.ShortDuration(TimeSpan.Parse("23:59:00")));
        }
        [Test]
        public void ZeroShortDuration()
        {
            Assert.AreEqual("0:00", Format.ShortDuration(TimeSpan.Parse("0:00:00")));
        }
        [Test]
        public void ShortDurationRoundMinutes()
        {
            Assert.AreEqual("0:02",Format.ShortDuration(TimeSpan.Parse("0:01:30")));
        }
        [Test]
        public void Percent()
        {
            Assert.AreEqual("75%",Format.Percent(0.75));
        }
        [Test]
        public void PercentForNaN()
        {
            Assert.AreEqual("0%",Format.Percent(double.NaN));
        }
        [Test]
        public void FormatDurationWithIncorrectString()
        {
            Assert.AreEqual(TimeSpan.Zero,Format.Duration("0-3"));
        }
        [Test]
        public void FormatMaskedText()
        {
            Assert.AreEqual("934", Format.MaskedText(TimeSpan.Parse("9:34")));
        }
        [Test]
        public void MaskedTextWithLeadingZero()
        {
            Assert.AreEqual("001", Format.MaskedText(TimeSpan.Parse("0:01")));
        }
        [Test]
        public void MaskedTextForZeroTimeSpan()
        {
            Assert.AreEqual("000", Format.MaskedText(TimeSpan.Zero));
        }
        [Test]
        public void LeadingHourZeroInFormatTimeIsSkipped()
        {
            Assert.AreEqual("6:01:01", Format.Time(DateTime.Parse("06:01:01")));
        }
        [Test]
        public void TimeSpanCouldBeFormattedToTime()
        {
            Assert.AreEqual(DateTime.Parse("13:59:00"), Format.Time(TimeSpan.Parse("13:59:00")));
        }
    }
}
