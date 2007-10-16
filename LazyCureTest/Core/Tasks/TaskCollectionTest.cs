using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Tasks
{
    [TestFixture]
    public class TaskCollectionTest
    {
        private readonly TaskCollection tasks = new TaskCollection();

        [Test]
        public void DefaultTasks()
        {
            Assert.IsTrue(tasks.Contains("Work"), "contains Work");
            Assert.IsTrue(tasks.Contains("Rest"), "contains Rest");
        }

        [Test]
        public void GetTask()
        {
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
    }
}