using System;
using System.IO;
using System.Text;
using System.Xml;
using NMock2;
using NUnit.Framework;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

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
        public void SerializeTwoTasks()
        {
            TaskCollection taskCollection = new TaskCollection();
            taskCollection.AddRange(new Task[] {new Task("task1"),new Task("task2")});

            XmlNode xml = TaskCollectionSerializer.Serialize(taskCollection);
            Assert.IsTrue(xml.InnerXml.Contains("task1"));
            Assert.IsTrue(xml.InnerXml.Contains("task2"));
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
        public void SerializeWithNullWriter()
        {
            StringBuilder sb = new StringBuilder();
            Log.Writer = new StringWriter(sb);
            ITaskCollection taskCollection = new TaskCollection();
            
            TaskCollectionSerializer.Serialize(taskCollection, null);
            Assert.That(sb.ToString().Contains("Could not serialize tasks because writer is null"));
        }
        [Test]
        public void SerializeWithNullTaskCollection()
        {
            StringBuilder sb = new StringBuilder();
            Log.Writer = new StringWriter(sb);
            ITaskCollection taskCollection = new TaskCollection();

            TaskCollectionSerializer.Serialize(null, Log.Writer);

            Assert.That(sb.ToString().Contains("Could not serialize null tasks collection"));
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
        [Test]
        public void DeserializeBrokenXml()
        {
            string sXml = "<tasks><task>&</task></tasks>";

            ITaskCollection taskCollection = TaskCollectionSerializer.Deserialize(new StringReader(sXml));

            Assert.IsNull(taskCollection);
        }
        [Test]
        public void DeserializeWrongRoot()
        {
            string sXml = "<projects><task name=\"Name\"></task></projects>";

            ITaskCollection taskCollection = TaskCollectionSerializer.Deserialize(new StringReader(sXml));

            Assert.AreEqual(0,taskCollection.Count);
        }
        
    }
}
