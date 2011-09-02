using System.Xml;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Tasks
{
    [TestFixture]
    public class TaskSerializerTest
    {
        [Test]
        public void Serialize()
        {
            Task task = new Task("task1");
            XmlNode xml = TaskSerializer.Serialize(task);
            Assert.IsTrue(xml.OuterXml.Contains("task1"));
        }
        [Test]
        public void SerializeWithNull()
        {
            Assert.IsNull(TaskSerializer.Serialize(null));
        }
        [Test]
        public void SerializeLinkToActivity()
        {
            Task task = new Task("task1");
            task.RelatedActivities.Add("activity1");

            XmlNode xml = TaskSerializer.Serialize(task);

            Assert.IsTrue(xml.InnerText.Contains("activity1"));
        }
        [Test]
        public void SerializeLinkToActivityWithSpecialSymbols()
        {
            Task task = new Task("task1");
            task.RelatedActivities.Add("a&b>c");

            XmlNode xml = TaskSerializer.Serialize(task);

            Assert.IsTrue(xml.InnerText.Contains("a&b>c"));
        }
        [Test]
        public void SerializeWorkingProperty()
        {
            Task task = new Task("rest", false);

            XmlNode xml = TaskSerializer.Serialize(task);

            Assert.AreEqual("false", xml.Attributes["working"].Value);
        }
        [Test]
        public void SerializeSubtask()
        {
            Task task = new Task("parent");
            task.Nodes.Add(new Task("sub"));
            XmlNode xml = TaskSerializer.Serialize(task);
            Assert.AreEqual("sub", xml.ChildNodes[0].Attributes["name"].Value);
        }
        [Test]
        public void Deserialize()
        {
            Task task = TaskSerializer.Deserialize("<task name=\"deserialized_task\"></task>");
            Assert.AreEqual("deserialized_task", task.Name);
        }
        [Test]
        public void DeserializeLinkToActivity()
        {
            Task task = TaskSerializer.Deserialize("<task name=\"task1\"><activity>activity1</activity></task>");

            Assert.AreEqual("activity1", task.RelatedActivities[0], "related activity");
            Assert.AreEqual(1, task.RelatedActivities.Count, "related activities count");
        }
        [Test]
        public void DeserializeWorkingProperty()
        {
            Task task = TaskSerializer.Deserialize("<task name=\"task1\" working=\"false\"/>");
            Assert.IsFalse(task.IsWorking);
        }
        [Test]
        public void DeserializeBrokenTask()
        {
            Assert.IsNull(TaskSerializer.Deserialize("<task>&</task>"));
        }
        [Test]
        public void DeserializeTaskWithIncorrectWorking()
        {
            Task task = TaskSerializer.Deserialize("<task name=\"task1\" working=\"aaa\" />");
            Assert.IsTrue(task.IsWorking);
        }
        [Test]
        public void DeserializeSubtask()
        {
            Task task = TaskSerializer.Deserialize("<task name=\"parent\"><task name=\"sub\"/></task>");
            Assert.AreEqual("sub", task.Nodes[0].Name);
        }
    }
}
