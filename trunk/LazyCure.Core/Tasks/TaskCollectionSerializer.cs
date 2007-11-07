using System.Xml;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public class TaskCollectionSerializer
    {
        public static XmlNode Serialize(ITaskCollection taskCollection)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.AppendChild(doc.CreateElement("tasks"));
            foreach(Task task in taskCollection)
            {
                root.InnerXml+= TaskSerializer.Serialize(task).OuterXml;
            }
            return doc;
        }
    }
}
