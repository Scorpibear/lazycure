using System;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Activities
{
    public abstract class ActivityBase : IActivity
    {
        protected string name;
        protected TimeSpan duration;
        protected DateTime start;

        public string Name { get { return name; } set { name = value; } }
        virtual public TimeSpan Duration { get { return duration; } set { duration = value; } }
        virtual public DateTime StartTime { get { return start; } set { start = value; } }
        public override string ToString()
        {
            return Name + " " + StartTime + " " + Duration;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return GetHashCode()==obj.GetHashCode();
        }
    }
}