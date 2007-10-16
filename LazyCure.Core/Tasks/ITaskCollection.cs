using System.Collections.Generic;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public interface ITaskCollection:IEnumerable<Task>
    {
        Task GetTask(string name);
    }
}