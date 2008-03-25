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
            foreach(string activity in task.RelatedActivities)
            {
                xml.AppendChild(doc.CreateElement("activity")).InnerText = activity;
            }
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
                        Task task = new Task(attribute.Value);
                        foreach(XmlNode node in xml.ChildNodes)
                        {
                            if(node.InnerText!=string.Empty)
                                task.RelatedActivities.Add(node.InnerText);
                        }
                        return task;
                    }
                }
            }
            return null;
        }
    }
}
