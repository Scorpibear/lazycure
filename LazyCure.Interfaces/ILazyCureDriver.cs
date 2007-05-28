using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface ILazyCureDriver
    {
        IActivity SwitchTo(string nextTaskName);
        IActivity CurrentActivity { get;}
        IActivity PreviousActivity { get;}
        object ActivitiesSummaryData{get;}
        object TimeLogData { get;}
        void FinishActivity(string finishedActivity, string nextActivity);
        bool SaveTimeLog();
        string TimeLogDate { get;}
    }
}
