using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    public interface ITimeManager
    {
        IActivity CurrentActivity { get; }

        ITimeLog TimeLog { get; set; }

        void FinishActivity(string activity, string nextActivity);
    }
}
