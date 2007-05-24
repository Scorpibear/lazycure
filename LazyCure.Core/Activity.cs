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
        private DateTime start;

        public string Name { get { return name; } set { name = value; } }
        public DateTime StartTime{get{return start;}set{start=value;}}
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
            this.start = timeSystem.Now;
        }
        public Activity(string name):this(name,new RunTimeSystem()) { }
        public Activity(string name, DateTime start, TimeSpan duration)
        {
            this.name = name;
            this.start = start;
            this.duration = duration;
            IsRunning = false;
        }
        private Activity(string name, Activity previous)
        {
            this.name = name;
            this.timeSystem = previous.timeSystem;
            this.start = previous.start + previous.duration;
        }

        public void Stop()
        {
            RecalculateDuration();
            RoundDuration();
            IsRunning = false;
        }

        private void RoundDuration()
        {
            if (duration.Milliseconds < 500)
                duration = new TimeSpan(0, 0, 0, (int)duration.TotalSeconds);
            else
                duration = new TimeSpan(0, 0, 0, (int)duration.TotalSeconds+1);
        }

        private void RecalculateDuration()
        {
            duration = timeSystem.Now - StartTime;
        }
        public Boolean IsRunning = true;
        public override string ToString()
        {
            string s = "<Records>" +
               "<Activity>"+Name+"</Activity>" + 
               "<Begin>"+Format.Time(StartTime)+"</Begin>"+
               "<Duration>"+Format.Duration(Duration)+"</Duration>"+
               "</Records>";
            return s;
        }

        internal static Activity After(Activity previous, string name)
        {
            return new Activity(name,previous);
        }
    }
}
