using System;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface ILazyCureDriver
    {
        object ActivitiesSummaryData { get; }

        TimeSpan AllActivitiesTime { get; }

        bool CalculateAutomaticallyWorkingIntervals { set; }

        IActivity CurrentActivity { get; }

        double Efficiency { get; }

        string[] HistoryActivities { get; }

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

        void FinishActivity(string finishedActivity, string nextActivity);

        string GetUniqueActivityName();

        bool LoadTimeLog(string filename);

        void PostToTwitter(string activity);

        bool Save();

        bool SaveTimeLog(string filename);
    }
}
