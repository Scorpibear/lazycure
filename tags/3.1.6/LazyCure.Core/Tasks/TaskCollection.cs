using System.Collections.Generic;

namespace LifeIdea.LazyCure.Core.Tasks
{
    /// <summary>
    /// Represent collection of all tasks
    /// </summary>
    public class TaskCollection : List<Task>, ITaskCollection
    {
        public static TaskCollection Default
        {
            get
            {
                TaskCollection defaultCollection = new TaskCollection();
                defaultCollection.Add(new Task("Work"));
                defaultCollection.Add(new Task("Rest"));
                return defaultCollection;
            }
        }

        public bool Contains(string taskName)
        {
            return (GetTask(taskName) != null);
        }

        public Task GetTask(string taskName)
        {
            foreach (Task task in this)
            {
                if (task.Name == taskName)
                    return task;
            }
            return null;
        }
    }
}