using System;
using System.IO;
using System.Text;
using System.Xml;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.Tasks
{
    [TestFixture]
    public class TaskCollectionSerializerTest:Mockery
    {
        [Test]
        public void Serialize()
        {
            TaskCollection taskCollection = new TaskCollection();
            taskCollection.Add(new Task("task_name"));
            
            XmlNode xml = TaskCollectionSerializer.Serialize(taskCollection);
            Assert.IsTrue(xml.InnerXml.Contains("task_name"));
        }
        [Test]
        public void SerializeToWriter()
        {
            ITaskCollection taskCollection = new TaskCollection();
            taskCollection.Add(new Task("task1"));
            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);
            
            TaskCollectionSerializer.Serialize(taskCollection, writer);
            writer.Close();

            Assert.IsTrue(sb.ToString().Contains("task1"));
        }
        [Test]
        public void Deserialize()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("tasks")).InnerXml =
                TaskSerializer.Serialize(new Task("task1")).OuterXml;

            ITaskCollection taskCollection = TaskCollectionSerializer.Deserialize(doc);
            
            Assert.IsTrue(taskCollection.Contains("task1"));
        }
    }
}
