using System.Collections.Generic;
using LifeIdea.LazyCure.Interfaces;

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
                defaultCollection.Add(new Task("Rest",false));
                return defaultCollection;
            }
        }

        public bool Contains(string taskName)
        {
            return (GetTask(taskName) != null);
        }

        public bool IsWorking(string taskName)
        {
            if (taskName == null)
            {
                Log.Error("IsWorking method is called with null");
                return false;
            }
            Task task = GetTask(taskName);
            if (task != null)
                return task.IsWorking;
            else
            {
                Log.Error(string.Format("IsWorking method is called for not existent task '{0}'",taskName));
                return false;
            }
        }

        public bool IsWorkingActivity(string activity)
        {
            string task = GetRelatedTaskName(activity);
            if (task != null)
                return IsWorking(task);
            else
                return false;
        }

        public void Remove(string taskName)
        {
            Remove(GetTask(taskName));
        }

        public void UpdateIsWorkingProperty(string taskName, bool working)
        {
            if (taskName == null)
            {
                Log.Error("UpdateIsWorkingProperty method is called with null task");
                return;
            }
            Task task = GetTask(taskName);
            if(task!=null)
                task.IsWorking = working;
            else
            {
                Log.Error(string.Format("UpdateIsWorkingProperty method is called for not existent task '{0}'",taskName));
            }
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
            Task previousTask = GetTask(GetRelatedTaskName(activityName));
            if (previousTask != null)
                previousTask.RelatedActivities.Remove(activityName);
            task.RelatedActivities.Add(activityName);
            return true;
        }
    }
}