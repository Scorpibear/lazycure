using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Structures;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public interface ILazyCureDriver: IHistoryDataProvider
    {
        object ActivitiesSummaryData { get; }

        TimeSpan AllActivitiesTime { get; }

        bool CalculateAutomaticallyWorkingIntervals { set; }

        IActivity CurrentActivity { get; }

        double Efficiency { get; }

        string[] LatestActivities { get; }

        TimeSpan PossibleWorkInterruptionDuration { get; set; }

        ITaskViewDataSource TaskViewDataSource { get;}

        object TasksSummaryData { get; }

        object TimeLogData { get; }

        string TimeLogDate { get; }

        string TimeLogsFolder { get; }

        TimeSpan TimeOnWork { get; }

        bool TimeToUpdateTimeLog { get; }

        TimeSpan WorkingActivitiesTime { get; }

        Object WorkingTimeIntervalsData { get; }

        void ApplySettings(ISettings settings);

        void FinishActivity(string finishedActivity, string nextActivity, bool postToExternals);

        string GetUniqueActivityName();

        bool LoadTimeLog(string filename);

        void RenameActivity(string before, string after);

        void PostToTwitter(string activity);

        bool Save();

        bool SaveTimeLog(string filename);

        void AuthorizeInExternalPoster();

        TokensPair SetExternalPosterAuthorizationPin(string p);
    }
}
