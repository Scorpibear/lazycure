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
            Assert.AreEqual(startTime,activity.Start);
            Assert.AreEqual(duration, activity.Duration);
        }
        [Test]
        public void EqualActivities()
        {
            Activity activity1 = new Activity("activity", DateTime.Parse("5:00:00"),TimeSpan.Parse("1:23:45"));
            Activity activity2 = new Activity("activity", DateTime.Parse("5:00:00"), TimeSpan.Parse("1:23:45"));
            Assert.AreEqual(activity1, activity2);
        }
        [Test]
        public void SettingNullNameInConstructorShouldSetItToEmpty()
        {
            Activity activity = new Activity(null, DateTime.Now, TimeSpan.FromSeconds(20));
            Assert.AreEqual("", activity.Name);
        }
    }
}
