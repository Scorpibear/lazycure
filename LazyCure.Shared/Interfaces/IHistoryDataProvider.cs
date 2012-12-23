using System.Data;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public interface IHistoryDataProvider: IDataProvider
    {
        IActivitiesHistory ActivitiesHistory { get; }

        void ApplySettings(ISettings settings);

        DataTable Data { get; }

        string[] HistoryActivities { get; }

        string[] LatestActivities { get; }

        string[] Tasks { get; }

        ITimeLogsManager TimeLogsManager { get; set; }

        string UniqueActivityName { get; }

        void UpdateDataTableForActivity(string activityName);

        void UpdateDataTableForTask(string taskName);
    }
}
