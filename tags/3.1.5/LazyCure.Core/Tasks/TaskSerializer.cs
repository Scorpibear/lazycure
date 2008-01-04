using System.Xml;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public static class TaskSerializer
    {
        private const string NAME = "name";

        public static XmlNode Serialize(Task task)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode xml = doc.CreateElement("task");
            xml.Attributes.Append(doc.CreateAttribute(NAME)).Value = task.Name;
            return xml;
        }

        public static Task Deserialize(XmlNode xml)
        {
            if (xml != null)
            {
                foreach (XmlAttribute attribute in xml.Attributes)
                {
                    if (attribute.Name == NAME)
                    {
                        return new Task(attribute.Value);
                    }
                }
            }
            return null;
        }
    }
}
