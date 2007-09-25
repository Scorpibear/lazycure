using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Represent collection of all tasks
    /// </summary>
    public class TaskCollection: List<Task>,ITaskCollection
    {
        public TaskCollection()
        {
            Add(new Task("Work"));
            Add(new Task("Rest"));
        }

        public bool Contains(string taskName)
        {
            return (GetTask(taskName) != null);
        }

        public Task GetTask(string taskName)
        {
            foreach(Task task in this)
            {
                if (task.Name == taskName)
                    return task;
            }
            return null;
        }
    }
}
