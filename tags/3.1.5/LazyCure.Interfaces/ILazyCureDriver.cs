using System;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface ILazyCureDriver
    {
        object ActivitiesSummaryData { get;}

        TimeSpan AllActivitiesTime { get; }

        IActivity CurrentActivity { get;}

        string[] LatestActivities { get;}

        object TimeLogData { get;}

        string TimeLogDate { get;}

        string TimeLogsFolder { get;}

        bool TimeToUpdateTimeLog { get; }

        void ApplySettings(ISettings settings);
        
        void FillTaskNodes(TreeNodeCollection nodes);

        void FinishActivity(string finishedActivity, string nextActivity);

        bool LoadTimeLog(string filename);

        bool Save();

        bool SaveTimeLog(string filename);

        void UpdateTaskNodeText(TreeNode treeNode, string text);
    }
}
