using System.Collections.Generic;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Store information about task
    /// </summary>
    public class Task:TreeNode
    {
        public readonly List<string> RelatedActivities = new List<string>();

        public Task(string name):base(name)
        {
            Name = name;
        }
    }
}
