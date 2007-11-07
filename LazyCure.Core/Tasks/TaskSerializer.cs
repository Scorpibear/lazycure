using System.Xml;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public static class TaskSerializer
    {
        public static XmlNode Serialize(Task task)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode xml = doc.CreateElement("task");
            xml.Attributes.Append(doc.CreateAttribute("name")).Value = task.Name;
            return xml;
        }
    }
}
