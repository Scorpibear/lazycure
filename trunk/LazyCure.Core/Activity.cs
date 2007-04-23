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

        public string Name{get{return name;}}
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
            StartTime = timeSystem.Now;
        }
        public Activity(string name):this(name,new RunTimeSystem()) { }
        
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
    }
}
