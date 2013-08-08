using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Constants;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;
using System.Collections.Specialized;

namespace LifeIdea.LazyCure.Core.Tasks
{
    /// <summary>
    /// Represent collection of all tasks
    /// </summary>
    public class TaskCollection : List<Task>, ITaskCollection, ITaskActivityLinker, ITaskViewDataSource
    {
        readonly string NewTaskName = Constants.NewTask;
        public static TaskCollection Default
        {
            get
            {
                TaskCollection defaultCollection = new TaskCollection();
                defaultCollection.Add(new Task(Constants.Work));
                defaultCollection.Add(new Task(Constants.Rest, false));
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
                Log.Error(string.Format("IsWorking method is called for not existent task '{0}'", taskName));
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

        public void UpdateIsWorkingProperty(string taskName, bool working)
        {
            if (taskName == null)
            {
                Log.Error("UpdateIsWorkingProperty method is called with null task");
                return;
            }
            Task task = GetTask(taskName);
            if (task != null)
                task.IsWorking = working;
            else
            {
                Log.Error(string.Format("UpdateIsWorkingProperty method is called for not existent task '{0}'", taskName));
            }
        }

        public string[] GetAllTasksNames()
        {
            var list = new List<string>();
            foreach (Task task in this)
                AddTaskWithSubtasksToList(list, task);
            return list.ToArray();
        }

        private static void AddTaskWithSubtasksToList(List<string> list, Task task)
        {
            list.Add(task.Name);
            foreach (Task subTask in task.Nodes)
                AddTaskWithSubtasksToList(list, subTask);
        }

        public Task GetTask(string taskName)
        {
            foreach (Task task in this)
            {
                if (task.Name == taskName)
                    return task;
                TreeNode[] foundNodes = task.Nodes.Find(taskName, true);
                if (foundNodes.Length > 0)
                {
                    Task found = foundNodes[0] as Task;
                    return found;
                }
            }
            return null;
        }

        public Task GetRelatedTask(string activityName)
        {
            return GetRelatedTask(activityName,this);
        }

        private Task GetRelatedTask(string activityName, IList nodes)
        {
            foreach (Task task in nodes)
            {
                if (task.RelatedActivities.Contains(activityName))
                    return task;
                Task found = GetRelatedTask(activityName, task.Nodes);
                if (found != null)
                    return found;
            }
            return null;
        }

        public string GetRelatedTaskName(string activityName)
        {
            Task task = GetRelatedTask(activityName);
            return (task != null) ? task.Name : null;
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

        public Task AddSubtask(Task parentTask)
        {
            return this.AddSubtask(parentTask as TreeNode) as Task;
        }

        #region ITaskViewDataSource Members

        public TreeNode AddSubtask(TreeNode parentNode)
        {
            Task parent = parentNode as Task;
            if (parent != null)
            {
                Task task = new Task(NewTaskName, parent.IsWorking);
                parent.Nodes.Add(task);
                return task;
            }
            return null;
        }

        public TreeNode AddTaskAfter(TreeNode previous)
        {
            
            TreeNode parentNode = previous.Parent;
            Task parent = parentNode as Task;
            Task task;
            if (parent != null)
            {
                task = new Task(NewTaskName, parent.IsWorking);
                parent.Nodes.Insert(previous.Index + 1, task);
            }
            else
            {
                task = new Task(NewTaskName);
                this.Insert(previous.Index + 1, task);
            }
            return task;
        }

        public TreeNode CreateTask()
        {
            Task task = new Task(NewTaskName);
            Add(task);
            return task;
        }

        public void RemoveNode(TreeNode node)
        {
            Task task = node as Task;
            if (task != null)
                this.Remove(task);
            else
                Log.Error("Could not remove a node '{0}', because it is not a Task object", node.Text);
        }
        
        public void UpdateIsWorking(TreeNode treeNode, bool value)
        {
            Task task = treeNode as Task;
            if (task != null)
                task.IsWorking = value;
            else
                Log.Error("IsWorking property could not be set for node '{0}', because it is not a Task object", treeNode.Text);
        }

        public void Rename(TreeNode treeNode, string newName)
        {
            treeNode.Name = newName;
            treeNode.Text = newName;
        }

        public bool IsWorkingNode(TreeNode treeNode)
        {
            Task task = treeNode as Task;
            if (task != null)
                return task.IsWorking;
            else
                return false;
        }

        public TreeNode GetNode(string taskName)
        {
            return this.GetTask(taskName);
        }

        #endregion
    }
}
