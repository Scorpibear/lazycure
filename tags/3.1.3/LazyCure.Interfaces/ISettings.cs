namespace LifeIdea.LazyCure.Interfaces
{
    public interface ISettings
    {
        int MaxActivitiesInHistory{ get; set;}
        bool SaveAfterDone{ get; set;}
        string TimeLogsFolder{ get; set;}
        void Save();
    }
}
