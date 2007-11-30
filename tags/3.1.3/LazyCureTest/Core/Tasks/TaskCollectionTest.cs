using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Tasks
{
    [TestFixture]
    public class TaskCollectionTest
    {
        private readonly TaskCollection tasks = new TaskCollection();

        [Test]
        public void Empty()
        {
            Assert.AreEqual(0,tasks.Count);
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
        }
    }
}