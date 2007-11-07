using System.Collections.Generic;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public interface ITaskCollection:IEnumerable<Task>
    {
        void Add(Task task);

        bool Contains(string taskName);

        Task GetTask(string name);

        Task[] ToArray();
    }
}