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
        public void Deserialize()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("task")).Attributes.Append(doc.CreateAttribute("name")).Value =
                "deserialized_task";
            Task task = TaskSerializer.Deserialize(doc.FirstChild);
            Assert.AreEqual("deserialized_task",task.Name);
        }
    }
}
