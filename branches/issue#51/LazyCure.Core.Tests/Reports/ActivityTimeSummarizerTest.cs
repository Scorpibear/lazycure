using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Core.Activities;

namespace LifeIdea.LazyCure.Core.Reports
{
    public class ActivityTimeSummarizerTest
    {
        [Test]
        public void SummarizeSpentForActivityInTimeLog()
        {
            var activities = new List<IActivity>(
                new IActivity[] {
                    new Activity("activity1", DateTime.Now, TimeSpan.Parse("0:10")),
                    new Activity("activity1", DateTime.Now, TimeSpan.Parse("0:15"))
                });
            var summarizer = new ActivityTimeSummarizer("activity1", null, null);
            TimeSpan spent = summarizer.SummarizeSpent(activities);
            Assert.AreEqual(TimeSpan.Parse("0:25"), spent);
        }
    }
}
