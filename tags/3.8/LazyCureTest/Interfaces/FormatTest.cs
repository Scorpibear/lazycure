using System;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Interfaces
{
    [TestFixture]
    public class FormatTest
    {
        [Test]
        public void EncodeWithNull()
        {
            Assert.IsEmpty(Format.Encode(null));
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
    }
}
