using System;

namespace LifeIdea.LazyCure.Core
{
    public class LiveActivity:ActivityBase
    {
        public override TimeSpan Duration
        {
            get
            {
                if (IsRunning)
                    RecalculateDuration();
                return duration;
            }
            /*
            set
            {
                duration = value;
            }*/
        }
        public ITimeSystem timeSystem;
        public Boolean IsRunning = true;
        public LiveActivity(string name, ITimeSystem timeSystem)
        {
            this.name = name;
            this.timeSystem = timeSystem;
            this.start = timeSystem.Now;
        }
        public void Stop()
        {
            RecalculateDuration();
            RoundDuration();
            IsRunning = false;
        }
        private LiveActivity(string name, LiveActivity previous)
        {
            this.name = name;
            this.timeSystem = previous.timeSystem;
            this.start = previous.start + previous.duration;
        }
        internal static LiveActivity After(LiveActivity previous, string name)
        {
            return new LiveActivity(name, previous);
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
    }
}
