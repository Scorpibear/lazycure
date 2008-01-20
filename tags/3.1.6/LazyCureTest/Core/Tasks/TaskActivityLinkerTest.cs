using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Tasks
{
    [TestFixture]
    public class TaskActivityLinkerTest:Mockery
    {
        [Test]
        public void LinkActivityAndTask()
        {
            ITaskCollection tasks = NewMock<ITaskCollection>();
            TaskActivityLinker linker = new TaskActivityLinker(tasks);
            Task task = new Task("task1");
            Stub.On(tasks).Method("GetTask").With("task1").Will(Return.Value(task));
            
            bool isLinked = linker.LinkActivityAndTask("activity1","task1");

            Assert.IsTrue(isLinked);
            Assert.IsTrue(task.RelatedActivities.Contains("activity1"));
        }

        [Test]
        public void GetRelatedTask()
        {
            TaskCollection tasks = new TaskCollection();
            TaskActivityLinker linker = new TaskActivityLinker(tasks);
            Task task = new Task("task1");
            task.RelatedActivities.Add("activity1");
            tasks.Add(task);
            
            string taskName = linker.GetRelatedTaskName("activity1");

            Assert.AreEqual("task1", taskName);
        }

        [Test]
        public void GetUnexistentTask()
        {
            ITaskCollection tasks = new TaskCollection();
            TaskActivityLinker linker = new TaskActivityLinker(tasks);

            Assert.IsNull(linker.GetRelatedTaskName("activity1"));
        }

        [Test]
        public void LinkUnexistentTask()
        {
            ITaskCollection tasks = NewMock<ITaskCollection>();
            TaskActivityLinker linker = new TaskActivityLinker(tasks);
            Stub.On(tasks).Method("GetTask").With("task1").Will(Return.Value(null));
            
            bool isLinked = linker.LinkActivityAndTask("activity1","task1");

            Assert.IsFalse(isLinked);
        }
    }
}