using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Core.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Link task and activities
    /// </summary>
    public class TaskActivityLinker:ITaskActivityLinker
    {
        private readonly ITaskCollection tasks;

        public TaskActivityLinker(ITaskCollection tasks)
        {
            this.tasks = tasks;
        }

        public string GetRelatedTask(string activityName)
        {
            foreach(Task task in tasks)
            {
                if(task.RelatedActivities.Contains(activityName))
                    return task.Name;
            }
            return null;
        }

        public void LinkActivityAndTask(string activityName, string taskName)
        {
            Task task = tasks.GetTask(taskName);
            task.RelatedActivities.Add(activityName);
        }
    }
}
