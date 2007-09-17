using NUnit.Framework;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class TaskTest
    {
        [Test]
        public void TaskName()
        {
            Task task = new Task("task1");

            Assert.AreEqual("task1", task.Name);
        }
    }
}