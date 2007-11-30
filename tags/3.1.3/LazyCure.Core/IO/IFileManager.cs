using System;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;

namespace LifeIdea.LazyCure.Core.IO
{
    public interface IFileManager
    {
        ITaskCollection GetTasks();
        ITimeLog GetTimeLog(string filename);
        bool SaveTasks(ITaskCollection taskCollection);
    }
}
