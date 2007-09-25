using System;

namespace LifeIdea.LazyCure.Core
{
    public class LiveActivity : ActivityBase
    {
        public ITimeSystem timeSystem;
        public Boolean IsRunning = true;
        public override DateTime StartTime
        {
            get
            {
                if (start.Millisecond < 500)
                    return new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, start.Second);
                else
                    return new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, start.Second + 1);
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

        public LiveActivity(string name, ITimeSystem timeSystem)
        {
            this.name = name;
            this.timeSystem = timeSystem;
            this.start = timeSystem.Now;
        }
        public void Stop()
        {
            RecalculateDuration();
            IsRunning = false;
        }
        public static LiveActivity After(LiveActivity previous, string name)
        {
            return new LiveActivity(name, previous);
        }

        private LiveActivity(string name, LiveActivity previous)
        {
            this.name = name;
            this.timeSystem = previous.timeSystem;
            this.start = previous.start + previous.duration;
        }
        private TimeSpan RoundedDuration
        {
            get
            {
                if (duration.Milliseconds < 500)
                    return new TimeSpan(0, 0, 0, (int)duration.TotalSeconds);
                else
                    return new TimeSpan(0, 0, 0, (int)duration.TotalSeconds + 1);
            }
        }
        private void RecalculateDuration()
        {
            duration = timeSystem.Now - StartTime;
        }
    }
}
