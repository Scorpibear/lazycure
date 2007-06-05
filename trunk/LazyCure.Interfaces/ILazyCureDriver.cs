using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface ILazyCureDriver
    {
        IActivity SwitchTo(string nextTaskName);
        IActivity CurrentActivity { get;}
        object ActivitiesSummaryData{get;}
        object TimeLogData { get;}
        void FinishActivity(string finishedActivity, string nextActivity);
        bool SaveTimeLog();
        bool SaveTimeLog(string filename);
        string TimeLogDate { get;}

        bool LoadTimeLog(string filename);
        string TimeLogsFolder { get;}
    }
}
