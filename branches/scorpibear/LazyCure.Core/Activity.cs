using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    public class Activity:IActivity
    {
        private string name;
        private TimeSpan duration;
        private DateTime startTime;

        public string Name { get { return name; } set { name = value; } }
        public DateTime StartTime{get{return startTime;}set{startTime=value;}}
        public TimeSpan Duration
        {
            get
            {
                if (IsRunning)
                    RecalculateDuration();
                return duration;
            }
            set
            {
                duration = value;
            }
        }
        public ITimeSystem timeSystem;
        public Activity(string name, ITimeSystem timeSystem)
        {
            this.name = name;
            this.timeSystem = timeSystem;
            this.startTime = timeSystem.Now;
        }
        public Activity(string name):this(name,new RunTimeSystem()) { }
        private Activity(string name, Activity previous)
        {
            this.name = name;
            this.timeSystem = previous.timeSystem;
            this.startTime = previous.startTime + previous.duration;
        }

        public void Stop()
        {
            RecalculateDuration();
            IsRunning = false;
        }

        private void RecalculateDuration()
        {
            duration = timeSystem.Now - StartTime;
        }
        public Boolean IsRunning = true;
        public override string ToString()
        {
            return name+startTime+Duration;
        }

        internal static Activity After(Activity previous, string name)
        {
            return new Activity(name,previous);
        }
    }
}
