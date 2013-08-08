using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.UI.Backend
{
    public class TreeBinder
    {
        TreeNodeCollection viewNodes;
        ITaskViewDataSource source;

        public TreeBinder(ITaskViewDataSource source)
        {
            if (source != null)
                this.source = source;
            else
                throw new Exception("TreeBinder could not be created with null source");
        }

        public void BindNodes(TreeView treeView)
        {
            foreach (TreeNode node in source)
            {
                try
                {
                    treeView.Nodes.Add(node);
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
            this.viewNodes = treeView.Nodes;
        }

        public TreeNode AddSibling(TreeNode previousNode)
        {
            TreeNode newNode;
            TreeNodeCollection nodes = viewNodes;
            if (previousNode != null)
            {
                newNode = source.AddTaskAfter(previousNode);
                if (previousNode.Parent == null)
                    nodes.Insert(previousNode.Index + 1, newNode);
            }
            else
            {
                newNode = source.CreateTask();
                nodes.Add(newNode);
            }
            return newNode;
        }

        public void Remove(TreeNode node)
        {
            source.RemoveNode(node);
            node.Remove();
        }

        public TreeNode AddSubtask(TreeNode parent)
        {
            TreeNode newNode = null;
            if (parent != null)
            {
                newNode = source.AddSubtask(parent);
                parent.Expand();
            }
            else
                newNode = source.CreateTask();
            return newNode;
        }

        public void UpdateIsWorking(TreeNode treeNode, bool value)
        {
            source.UpdateIsWorking(treeNode, value);
        }

        public void Rename(TreeNode treeNode, string newName)
        {
            source.Rename(treeNode, newName);
        }

        public bool IsWorking(TreeNode treeNode)
        {
            return source.IsWorkingNode(treeNode);
        }

        public TreeNode GetTask(string taskName)
        {
            return source.GetNode(taskName);
        }
    }
}