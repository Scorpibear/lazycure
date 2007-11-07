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
    }
}
