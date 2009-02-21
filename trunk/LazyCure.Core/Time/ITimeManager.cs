using System;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    public interface ITimeManager
    {
        IActivity CurrentActivity { get; }

        bool CurrentActivityIsLastingTooLong { get; }

        void FinishActivity(string activity, string nextActivity);

        TimeSpan MaxDuration { get; set; }

        bool SwitchAtMidnight { set; }

        ITimeLog TimeLog { get; set; }

        ITimeSystem TimeSystem { get;}
    }
}
