using NUnit.Framework;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class TaskTest
    {
        private readonly Task task = new Task("task1");

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
    }
}