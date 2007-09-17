using System;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface ILazyCureDriver
    {
        IActivity CurrentActivity { get;}
        object ActivitiesSummaryData{get;}
        object TimeLogData { get;}
        string[] LatestActivities { get;}
        string TimeLogDate { get;}
        string TimeLogsFolder { get;}

        TimeSpan AllActivitiesTime { get; }

        void FinishActivity(string finishedActivity, string nextActivity);
        bool Save();
        bool SaveTimeLog(string filename);
        bool LoadTimeLog(string filename);
        
    }
}
