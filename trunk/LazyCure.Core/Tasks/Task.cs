using System.Collections.Generic;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.Core.Tasks
{
    /// <summary>
    /// Store information about task
    /// </summary>
    public class Task : TreeNode
    {
        public readonly List<string> RelatedActivities = new List<string>();

        public Task(string name)
            : base(name)
        {
            Name = name;
        }

        public new string Text
        {
            get
            {
                return Name;
            }
            set { Name = value; }
        }
    }
}