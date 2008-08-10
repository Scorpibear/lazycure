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

        string[] LatestActivities { get; }

        TimeSpan PossibleWorkInterruptionDuration { get; set; }

        TreeNode[] TasksNodes { get; }

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

        bool IsWorkingTask(string SelectedTask);

        bool LoadTimeLog(string filename);

        bool Save();

        bool SaveTimeLog(string filename);

        void UpdateIsWorkingTaskProperty(string task, bool isWorking);

        void UpdateTaskNodeText(TreeNode treeNode, string text);
    }
}
