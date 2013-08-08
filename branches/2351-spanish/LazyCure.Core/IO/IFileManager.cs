using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.IO
{
    public interface IFileManager: ITimeLogsFileManager
    {
        ITaskCollection GetTasks();

        ITimeLog GetTimeLog(string filename);

        string GetTimeLogFileName(DateTime date);

        void LoadHistory(IActivitiesHistory history);

        void SaveHistory(IActivitiesHistory History);
        
        bool SaveTasks(ITaskCollection taskCollection);

        bool SaveTimeLog(ITimeLog timeLog, string filename);

        string TimeLogsFolder { get; set;}
    }
}
