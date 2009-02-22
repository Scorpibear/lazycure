using System;
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
        public override DateTime Start
        {
            get
            {
                if (start.Millisecond < MILLISECONDS_IN_ONE_SECOND / 2.0)
                    return DateTime.Parse(start.ToString("yyyy-MM-dd HH:mm:ss"));
                else
                    return DateTime.Parse(start.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss"));
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

        public RunningActivity[] SplitByComma()
        {
            string[] names = Name.Split(',');
            RunningActivity[] next = new RunningActivity[names.Length - 1];
            if (names.Length > 0)
            {
                Name = names[0];
                TimeSpan totalDuration = duration;
                duration = TimeSpan.FromMilliseconds(totalDuration.TotalMilliseconds / names.Length);

                RunningActivity previous = this;
                for (int i = 0; i < names.Length - 1; i++)
                {
                    next[i] = new RunningActivity(names[i + 1], previous);
                    next[i].IsRunning = false;
                    next[i].duration = duration;
                    previous = next[i];
                }
            }
            return next;
        }
    }
}
