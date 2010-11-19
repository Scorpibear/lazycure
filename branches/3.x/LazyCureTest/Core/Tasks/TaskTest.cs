using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Tasks
{
    [TestFixture]
    public class TaskTest
    {
        private Task task;

        [SetUp]
        public void SetUp()
        {
            task = new Task("task1");
        }
        [Test]
        public void TaskName()
        {
            Assert.AreEqual("task1", task.Name);
        }
        [Test]
        public void TaskText()
        {
            Assert.AreEqual("task1",task.Text);
        }
        [Test]
        public void ReleatedActivities()
        {
            task.RelatedActivities.Add("activity1");
            Assert.IsTrue(task.RelatedActivities.Contains("activity1"));
        }
        [Test]
        public void NameIsChangedWithText()
        {
            task.Text = "new text";
            Assert.AreEqual("new text",task.Text);
            Assert.AreEqual("new text", task.Name);
        }
        [Test]
        public void TextIsChangedWithName()
        {
            task.Name = "new name";
            Assert.AreEqual("new name", task.Name);
            Assert.AreEqual("new name", task.Text);
        }
        [Test]
        public void TwoEqualTasks()
        {
            task = new Task("task1");
            Task task2 = new Task("task1");
            Assert.AreEqual(task, task2);
        }
        [Test]
        public void DefaultTaskIsWorking()
        {
            Assert.That(task.IsWorking);
        }

        [Test]
        public void TasksShouldNotBeEqualEvenIfTheirHashCodesAre()
        {
            string name1 = "699391";
            string name2 = "1241308";

            // just to make sure that hash codes are equal
            Assert.IsTrue(name1.GetHashCode() == name2.GetHashCode());

            task = new Task(name1);
            Task task2 = new Task(name2);
            
            Assert.IsFalse(task.Equals(task2));
            Assert.IsFalse(task2.Equals(task));
        }

        [Test]
        public void TaskShouldBeEqualOnlyToTask()
        {
            // Task can't be equal to String
            Assert.IsFalse(task.Equals("task1"));
        }
    }
}