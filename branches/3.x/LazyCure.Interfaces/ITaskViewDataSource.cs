using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface ITaskViewDataSource:IEnumerable
    {
        TreeNode AddSubtask(TreeNode parent);

        TreeNode AddTaskAfter(TreeNode previous);
        
        TreeNode CreateTask();

        TreeNode GetNode(string taskName);

        bool IsWorkingNode(TreeNode treeNode);

        void RemoveNode(TreeNode node);

        void Rename(TreeNode treeNode, string newName);

        void UpdateIsWorking(TreeNode treeNode, bool value);
    }
}
