using System.Collections.Generic;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public interface ITaskCollection:IEnumerable<Task>,ITaskActivityLinker
    {
        int Count{ get;}

        void Add(Task task);

        bool Contains(string taskName);

        Task GetTask(string name);

        bool IsWorking(string taskName);

        bool IsWorkingActivity(string activity);

        void UpdateIsWorkingProperty(string taskName, bool working);
    }
}
