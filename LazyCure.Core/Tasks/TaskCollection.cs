using System.Collections.Generic;

namespace LifeIdea.LazyCure.Core.Tasks
{
    /// <summary>
    /// Represent collection of all tasks
    /// </summary>
    public class TaskCollection : List<Task>, ITaskCollection, ITaskActivityLinker
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

        public string GetRelatedTaskName(string activityName)
        {
            foreach (Task task in this)
            {
                if (task.RelatedActivities.Contains(activityName))
                    return task.Name;
            }
            return null;
        }

        public bool LinkActivityAndTask(string activityName, string taskName)
        {
            Task task = GetTask(taskName);
            if (task == null)
                return false;
            task.RelatedActivities.Add(activityName);
            return true;
        }
    }
}