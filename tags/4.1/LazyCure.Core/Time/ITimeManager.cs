using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    public interface ITimeManager
    {
        IActivity CurrentActivity { get; }

        bool CurrentActivityIsLastingTooLong { get; }

        List<IActivity> FinishActivity(string activity, string nextActivity);

        TimeSpan MaxDuration { get; set; }

        bool SplitByComma { set; }

        bool SwitchAtMidnight { set; }

        ITimeLog TimeLog { get; set; }

        ITimeSystem TimeSystem { get;}

        string TweetingActivity { get; set; }
    }
}
