using System;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Activities
{
    /// <summary>
    /// Represent base abstract class for Activity and RunningActivity
    /// </summary>
    public abstract class ActivityBase : IActivity
    {
        protected string name;
        protected TimeSpan duration;
        protected DateTime start;

        public string Name { get { return name; } set { name = value; } }
        virtual public TimeSpan Duration { get { return duration; } set { duration = value; } }
        virtual public DateTime Start { get { return start; } set { start = value; } }
        virtual public DateTime End { get { return Start + Duration; } }
        public override string ToString()
        {
            return Name + " " + Start + " " + Duration;
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