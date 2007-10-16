using System;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Activities
{
    [TestFixture]
    public class ActivityTest
    {
        [Test]
        public void StoreActivity()
        {
            string name = "activity1";
            DateTime startTime = DateTime.Parse("2007-02-16 13:00:00");
            TimeSpan duration = TimeSpan.FromMinutes(15.0);
            Activity activity = new Activity(name, startTime, duration);
            Assert.AreEqual(name, activity.Name);
            Assert.AreEqual(startTime,activity.StartTime);
            Assert.AreEqual(duration, activity.Duration);
        }
    }
}
