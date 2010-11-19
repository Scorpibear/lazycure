namespace LifeIdea.LazyCure.Core.Time
{
    public interface ITimeLogsManager
    {
        bool Save();
        void UpdateTimeLogReferencies(ITimeLog timeLog);
    }
}
