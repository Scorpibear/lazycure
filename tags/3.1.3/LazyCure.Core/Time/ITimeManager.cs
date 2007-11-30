using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    public interface ITimeManager
    {
        IActivity CurrentActivity { get; }

        ITimeLog TimeLog { get; set; }

        ITimeSystem TimeSystem { get;}

        void FinishActivity(string activity, string nextActivity);
    }
}
