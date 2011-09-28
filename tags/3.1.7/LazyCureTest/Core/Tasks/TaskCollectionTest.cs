using System;
using System.IO;
using LifeIdea.LazyCure.Core.IO;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Tasks
{
    [TestFixture]
    public class TaskCollectionTest:Mockery
    {
        private readonly TaskCollection tasks = new TaskCollection();

        [Test]
        public void Empty()
        {
            Assert.AreEqual(0,tasks.Count);
        }
        [Test]
        public void IsWorkingTaskTrue()
        {
            tasks.Add(new Task("working task"));
            Assert.IsTrue(tasks.IsWorkingTask("working task"));
        }
        [Test]
        public void IsWorkingTaskFalse()
        {
            Task task = new Task("resting task",false);
            tasks.Add(task);
            Assert.IsFalse(tasks.IsWorkingTask("resting task"));
        }
        [Test]
        public void IsWorkingTaskUnexistent()
        {
            StringWriter sw = new StringWriter();
            Log.Writer = sw;
            string expected = "IsWorking method is called for not existent task 'unexisted task'";
            
            Assert.IsFalse(tasks.IsWorkingTask("unexisted task"));
            Assert.That(sw.GetStringBuilder().ToString().Contains(expected));
        }
        [Test]
        public void IsWorkingTaskNull()
        {
            StringWriter sw = new StringWriter();
            Log.Writer = sw;

            Assert.IsFalse(tasks.IsWorkingTask(null));
            Assert.That(sw.GetStringBuilder().ToString().Contains("IsWorking method is called with null"));
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
            Assert.IsFalse(TaskCollection.Default.IsWorkingTask("Rest"));
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

            bool isLinked = linker.LinkActivityAndTask("activity1", "task1");

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
    }
}