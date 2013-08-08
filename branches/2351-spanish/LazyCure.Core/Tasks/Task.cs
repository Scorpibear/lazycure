using System.Collections.Generic;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Tasks
{
    /// <summary>
    /// Store information about task
    /// </summary>
    public class Task : TreeNode, ITask
    {
        public readonly List<string> RelatedActivities = new List<string>();
        public bool IsWorking = true;

        public Task(string name)
            : base(name)
        {
            Name = name;
        }

        public Task(string name,bool isWorking):this(name)
        {
            IsWorking = isWorking;
        }

        public new string Text
        {
            get
            {
                return Name;
            }
            set { Name = value; }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Task otherTask = obj as Task;
            return otherTask == null ? false : Name.Equals(otherTask.Name);
        }
    }
}