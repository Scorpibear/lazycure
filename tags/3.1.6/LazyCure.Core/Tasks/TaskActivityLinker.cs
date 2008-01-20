namespace LifeIdea.LazyCure.Core.Tasks
{
    /// <summary>
    /// Link task and activities
    /// </summary>
    public class TaskActivityLinker:ITaskActivityLinker
    {
        private ITaskCollection tasks;

        public TaskActivityLinker(ITaskCollection tasks)
        {
            this.tasks = tasks;
        }

        public ITaskCollection TaskCollection
        {
            set { tasks = value; }
        }

        public string GetRelatedTaskName(string activityName)
        {
            foreach(Task task in tasks)
            {
                if(task.RelatedActivities.Contains(activityName))
                    return task.Name;
            }
            return null;
        }

        public bool LinkActivityAndTask(string activityName, string taskName)
        {
            Task task = tasks.GetTask(taskName);
            if (task == null)
                return false;
            task.RelatedActivities.Add(activityName);
            return true;
        }
    }
}