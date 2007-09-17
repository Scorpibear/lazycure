using System;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
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

    public abstract class ActivityBase:IActivity
    {
        protected string name;
        protected TimeSpan duration;
        protected DateTime start;

        public string Name { get { return name; } set { name = value; } }
        virtual public TimeSpan Duration { get { return duration; } set { duration = value; } }
        virtual public DateTime StartTime {get { return start; } set { start=value; } }
        public override string ToString()
        {
            return Serializer.ActivityToString(this);
        }
    }
}
