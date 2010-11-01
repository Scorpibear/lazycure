using System.IO;
using System.Windows.Forms;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Tasks
{
    [TestFixture]
    public class TaskCollectionTest:Mockery
    {
        private TaskCollection tasks;

        [SetUp]
        public void SetUp()
        {
            tasks = new TaskCollection();
        }
        [Test]
        public void Empty()
        {
            Assert.AreEqual(0,tasks.Count);
        }
        [Test]
        public void IsWorkingTaskTrue()
        {
            tasks.Add(new Task("working task"));
            Assert.IsTrue(tasks.IsWorking("working task"));
        }
        [Test]
        public void IsWorkingTaskFalse()
        {
            Task task = new Task("resting task",false);
            tasks.Add(task);
            Assert.IsFalse(tasks.IsWorking("resting task"));
        }
        [Test]
        public void IsWorkingTaskUnexistent()
        {
            StringWriter sw = new StringWriter();
            Log.Writer = sw;
            string expected = "IsWorking method is called for not existent task 'unexisted task'";
            
            Assert.IsFalse(tasks.IsWorking("unexisted task"));
            Assert.That(sw.GetStringBuilder().ToString().Contains(expected));
        }
        [Test]
        public void IsWorkingTaskNull()
        {
            StringWriter sw = new StringWriter();
            Log.Writer = sw;

            Assert.IsFalse(tasks.IsWorking(null));
            Assert.That(sw.GetStringBuilder().ToString().Contains("IsWorking method is called with null"));
        }
        [Test]
        public void IsWorkingActivityWithUnknownActivityIsSilent()
        {
            Log.Writer = new StringWriter();
            tasks.IsWorkingActivity("unknown");
            Assert.AreEqual("", Log.Writer.ToString());
        }
        [Test]
        public void GetTask()
        {
            tasks.Add(new Task("Work"));
            Task task = tasks.GetTask("Work");
            Assert.IsNotNull(task);
            Assert.AreEqual("Work",task.Name);
            Assert.AreEqual("Work", task.Text);
        }
        [Test]
        public void GetTaskSearchInSubNodes()
        {
            Task root = new Task("root");
            Task sub = new Task("sub");
            root.Nodes.Add(sub);
            tasks.Add(root);
            Assert.AreEqual(sub, tasks.GetTask("sub"));
        }
        [Test]
        public void GetTaskReturnsExistentTask()
        {
            Task task1 = new Task("task1");
            tasks.Add(task1);
            Assert.AreSame(task1,tasks.GetTask("task1"));
        }
        [Test]
        public void DefaultCollection()
        {
            Assert.IsTrue(TaskCollection.Default.Contains("Work"), "contains Work");
            Assert.IsTrue(TaskCollection.Default.Contains("Rest"), "contains Rest");
            Assert.IsFalse(TaskCollection.Default.IsWorking("Rest"));
        }
        [Test]
        public void LinkActivityAndTask()
        {
            Task task = new Task("linked task");
            tasks.Add(task);

            bool isLinked = tasks.LinkActivityAndTask("activity1", "linked task");

            Assert.IsTrue(isLinked);
            Assert.IsTrue(task.RelatedActivities.Contains("activity1"));
        }
        [Test]
        public void UpdateLink()
        {
            tasks = TaskCollection.Default;
            tasks.LinkActivityAndTask("thinking", "Work");
            tasks.LinkActivityAndTask("thinking", "Rest");

            Assert.AreEqual("Rest", tasks.GetRelatedTaskName("thinking"));
        }
        [Test]
        public void GetRelatedTask()
        {
            Task task = new Task("task1");
            task.RelatedActivities.Add("activity1");
            tasks.Add(task);

            string taskName = tasks.GetRelatedTaskName("activity1");

            Assert.AreEqual("task1", taskName);
        }
        [Test]
        public void GetUnexistentTask()
        {
            Assert.IsNull(tasks.GetRelatedTaskName("activity1"));
        }
        [Test]
        public void GetRelatedSubtask()
        {
            Task root = new Task("root");
            Task subTask = new Task("sub");
            subTask.RelatedActivities.Add("activity1");
            root.Nodes.Add(subTask);
            tasks.Add(root);
            Assert.AreEqual("sub", tasks.GetRelatedTaskName("activity1"));
        }
        [Test]
        public void LinkUnexistentTask()
        {
            bool isLinked = tasks.LinkActivityAndTask("activity1", "task1");

            Assert.IsFalse(isLinked);
        }
        [Test]
        public void UpdateIsWorkingProperty()
        {
            Task task = new Task("tested");
            tasks.Add(task);
            tasks.UpdateIsWorkingProperty("tested",false);
            Assert.IsFalse(task.IsWorking);
        }
        [Test]
        public void UpdateIsWorkingPropertyNotexistent()
        {
            StringWriter sw = new StringWriter();
            Log.Writer = sw;
            
            tasks.UpdateIsWorkingProperty("not existent", false);

            Assert.That(sw.GetStringBuilder().ToString().Contains("UpdateIsWorkingProperty method is called for not existent task 'not existent'"));
        }
        [Test]
        public void UpdateIsWorkingPropertyForNull()
        {
            StringWriter sw = new StringWriter();
            Log.Writer = sw;

            tasks.UpdateIsWorkingProperty(null, false);

            Assert.That(sw.GetStringBuilder().ToString().Contains("UpdateIsWorkingProperty method is called with null task"));
        }
        [Test]
        public void UpdateIsWorking()
        {
            Task task = new Task("original", false);
            tasks.UpdateIsWorking(task, true);
            Assert.IsTrue(task.IsWorking);
        }
        [Test]
        public void AddTaskAfter()
        {
            Task previous = new Task("previous");
            tasks.Add(previous);
            TreeNode node = tasks.AddTaskAfter(previous);
            Task task = node as Task;
            Assert.AreEqual(2, tasks.Count, "Count");
            Assert.AreEqual(task, tasks[1], "Task[1]");
        }
        [Test]
        public void AddTaskAfterSubtask()
        {
            Task root = new Task("root");
            Task sub = new Task("sub");
            root.Nodes.Add(sub);
            tasks.Add(root);
            TreeNode node = tasks.AddTaskAfter(sub);
            Task task = node as Task;
            Assert.AreEqual(1, tasks.Count, "Count on root level");
            Assert.AreEqual(2, tasks[0].Nodes.Count, "Count of subnodes");
            Assert.AreEqual(node, tasks[0].Nodes[1], "new node");
        }
        [Test]
        public void UpdateIsWorkingWithNotTask()
        {
            TreeNode node = new TreeNode("just a node");
            tasks.UpdateIsWorking(node, true);
            Assert.AreEqual("IsWorking property could not be set for node 'just a node', because it is not a Task object",
                Log.LastError);
        }
        [Test]
        public void Rename()
        {
            Task task = new Task("original");
            tasks.Rename(task, "new name");
            Assert.AreEqual("new name", task.Name);
            Assert.AreEqual("new name", task.Text);
        }
        [Test]
        public void RenameWithNotTask()
        {
            TreeNode node = new TreeNode("ordinal");
            tasks.Rename(node, "new name");
            Assert.AreEqual("new name", node.Name,"Name");
            Assert.AreEqual("new name", node.Text,"Text");
        }
        [Test]
        public void RemoveTreeNode()
        {
            TreeNode node = new TreeNode("just a node");
            Log.Writer = new StringWriter();
            tasks.RemoveNode(node);
            Assert.AreEqual("Could not remove a node 'just a node', because it is not a Task object", Log.LastError);
        }
        [Test]
        public void IsWorkingNodeTrue()
        {
            Task task = new Task("original",true);
            Assert.IsTrue(tasks.IsWorkingNode(task));
        }
        [Test]
        public void IsWorkingNodeFalse()
        {
            Task task = new Task("original", false);
            Assert.IsFalse(tasks.IsWorkingNode(task));
        }
        [Test]
        public void IsWorkingNodeWithNotTask()
        {
            TreeNode node = new TreeNode("zasada");
            Assert.IsFalse(tasks.IsWorkingNode(node));
        }
        [Test]
        public void SubTaskHasTheSameWorkingProperty()
        {
            Task parent = new Task("parent",false);
            TreeNode newNode = tasks.AddSubtask(parent);
            Task task = newNode as Task;
            Assert.IsFalse(task.IsWorking);
        }
        [Test]
        public void AddSiblingSetIsWorkingFromParent()
        {
            Task parent = new Task("parent", false);
            Task previous = new Task("previous");
            parent.Nodes.Add(previous);
            TreeNode newNode = tasks.AddTaskAfter(previous);
            Task task = newNode as Task;
            Assert.IsFalse(task.IsWorking);
        }
    }
}
