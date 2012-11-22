using System.Data;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public interface IHistoryDataProvider
    {
        string[] HistoryActivities { get; }

        DataTable SpentOnDiffDaysDataTable { get; }

        void UpdateDataTableForActivity(string activityName);
    }
}
