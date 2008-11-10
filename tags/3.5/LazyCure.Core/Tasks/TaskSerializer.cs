using System;
using System.Xml;
using LifeIdea.LazyCure.Core.IO;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public static class TaskSerializer
    {
        private const string NAME_ATTRIBUTE = "name";
        private const string TASK_ELEMENT = "task";
        private const string ACTIVITY_ELEMENT = "activity";
        private const string WORKING_ATTRIBUTE = "working";

        public static XmlNode Serialize(Task task)
        {
            if (task != null)
            {
                XmlDocument doc = new XmlDocument();
                XmlNode xml = doc.CreateElement(TASK_ELEMENT);
                xml.Attributes.Append(doc.CreateAttribute(NAME_ATTRIBUTE)).Value = task.Name;
                xml.Attributes.Append(doc.CreateAttribute(WORKING_ATTRIBUTE)).Value =
                    Utilities.BoolToString(task.IsWorking);
                foreach (string activity in task.RelatedActivities)
                {
                    xml.AppendChild(doc.CreateElement(ACTIVITY_ELEMENT)).InnerText = activity;
                }
                return xml;
            }
            else
                return null;
        }

        public static Task Deserialize(XmlNode xml)
        {
            if (xml != null)
            {
                string name = null;
                bool isWorking = true;
                foreach (XmlAttribute attribute in xml.Attributes)
                    switch (attribute.Name)
                    {
                        case NAME_ATTRIBUTE:
                            name = attribute.Value;
                            break;
                        case WORKING_ATTRIBUTE:
                            isWorking = Utilities.StringToBool(attribute.Value);
                            break;
                    }
                if (name != null)
                {
                    Task task = new Task(name, isWorking);
                    foreach (XmlNode node in xml.ChildNodes)
                    {
                        if (node.InnerText != string.Empty)
                            task.RelatedActivities.Add(node.InnerText);
                    }
                    return task;
                }
            }
            return null;
        }

        public static Task Deserialize(string xml)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return null;
            }
            return Deserialize(doc.FirstChild);
        }
    }
}
