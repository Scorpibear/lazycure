using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Interfaces
{
    [TestFixture]
    public class FormatTest
    {
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
    }
}
