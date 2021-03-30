using System;
using System.Data;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public interface IHistoryDataProvider: IDataProvider
    {
        IActivitiesHistory ActivitiesHistory { get; }

        TimeSpan AllActivitiesTime { get; }

        DataTable ActivitiesSummaryData { get; }

        void ApplySettings(ISettings settings);

        void CreateSummaries(ITimeLog timeLog);

        string[] HistoryActivities { get; }

        string[] LatestActivities { get; }

        void SetSummaryPeriod(DateTime from, DateTime to);

        string[] Tasks { get; }

        object TasksSummaryData { get; }

        ITimeLogsManager TimeLogsManager { get; set; }

        string UniqueActivityName { get; }

        void UpdateDataTableForActivity(string activityName);

        void UpdateDataTableForTask(string taskName);

        void UpdateTimeLog(ITimeLog timeLog);
    }
}
