using System;

namespace LifeIdea.LazyCure.Core.Activities
{
    public class Activity:ActivityBase
    {
        public Activity(string name, DateTime start, TimeSpan duration)
        {
            this.name = name;
            this.start = start;
            this.duration = duration;
        }
    }
}