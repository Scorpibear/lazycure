using System;
using System.Collections.Generic;
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
    }
}
