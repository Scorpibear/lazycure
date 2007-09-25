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
    }
}