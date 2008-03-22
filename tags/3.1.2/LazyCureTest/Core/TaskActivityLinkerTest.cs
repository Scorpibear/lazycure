using LifeIdea.LazyCure.Core.Interfaces;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core
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
            
            linker.LinkActivityAndTask("activity1","task1");

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
            
            string taskName = linker.GetRelatedTask("activity1");

            Assert.AreEqual("task1", taskName);
        }

        [Test]
        public void GetUnexistentTask()
        {
            ITaskCollection tasks = new TaskCollection();
            TaskActivityLinker linker = new TaskActivityLinker(tasks);

            Assert.IsNull(linker.GetRelatedTask("activity1"));
        }
    }
}