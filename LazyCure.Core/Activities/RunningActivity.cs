using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Time;

namespace LifeIdea.LazyCure.Core.Activities
{
    /// <summary>
    /// Represent running activity
    /// </summary>
    public class RunningActivity : ActivityBase
    {
        public ITimeSystem timeSystem;
        public Boolean IsRunning = true;
        public const double MILLISECONDS_IN_ONE_SECOND = 1000;
        public const string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public override DateTime Start
        {
            get
            {
                if (start.Millisecond < MILLISECONDS_IN_ONE_SECOND / 2.0)
                    return DateTime.Parse(start.ToString(TIME_FORMAT));
                else
                    return DateTime.Parse(start.AddSeconds(1).ToString(TIME_FORMAT));
            }
        }
        public override TimeSpan Duration
        {
            get
            {
                if (IsRunning)
                    RecalculateDuration();
                return RoundedDuration;
            }
        }

        public RunningActivity(string name, ITimeSystem timeSystem)
        {
            Name = name;
            this.timeSystem = timeSystem;
            this.start = timeSystem.Now;
        }
        public void Stop()
        {
            RecalculateDuration();
            IsRunning = false;
        }
        public static RunningActivity After(RunningActivity previous, string name)
        {
            return new RunningActivity(name, previous);
        }

        private RunningActivity(string name, RunningActivity previous)
        {
            Name = name;
            this.timeSystem = previous.timeSystem;
            this.start = previous.start + previous.duration;
        }
        private TimeSpan RoundedDuration
        {
            get
            {
                if (duration.Milliseconds < MILLISECONDS_IN_ONE_SECOND / 2)
                    return new TimeSpan(0, 0, 0, (int)duration.TotalSeconds);
                else
                    return new TimeSpan(0, 0, 0, (int)duration.TotalSeconds + 1);
            }
        }
        private void RecalculateDuration()
        {
            duration = timeSystem.Now - Start;
        }

        /// <summary>
        /// Split running activity by comma and returns array of activities
        /// </summary>
        /// <returns>array of activities, created after split</returns>
        public RunningActivity[] SplitByComma()
        {
            string[] names = Name.Split(',');
            RunningActivity[] next = new RunningActivity[names.Length];
            if (names.Length > 0)
            {
                this.Name = names[0];
                TimeSpan totalDuration = this.duration;
                this.duration = TimeSpan.FromMilliseconds(totalDuration.TotalMilliseconds / names.Length);
                next[0] = this;
                for (int i = 1; i < names.Length; i++)
                {
                    next[i] = RunningActivity.After(next[i - 1], names[i]);
                    next[i].IsRunning = false;
                    next[i].duration = this.duration;
                }
            }
            return next;
        }
    }
}
