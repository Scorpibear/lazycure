using System;

namespace LifeIdea.LazyCure.Core.Activities
{
    /// <summary>
    /// Store one activity properties
    /// </summary>
    public class Activity:ActivityBase
    {
        public Activity(string name, DateTime start, TimeSpan duration)
        {
            Name = name;
            this.start = start;
            this.duration = duration;
        }
    }
}