using System;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;

namespace LifeIdea.LazyCure.Core.IO
{
    public interface IFileManager
    {
        ITaskCollection GetTasks();

        ITimeLog GetTimeLog(string filename);

        string GetTimeLogFileName(DateTime date);

        void LoadHistory(IActivitiesHistory history);

        void SaveHistory(IActivitiesHistory History);
        
        bool SaveTasks(ITaskCollection taskCollection);

        bool SaveTimeLog(ITimeLog timeLog);

        bool SaveTimeLog(ITimeLog timeLog, string filename);

        string TimeLogsFolder { get; set;}
    }
}
