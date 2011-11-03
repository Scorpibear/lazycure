using System;
using System.Xml;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public static class TaskSerializer
    {
        private const string NAME_ATTRIBUTE = "name";
        private const string TASK_ELEMENT = "task";
        private const string ACTIVITY_ELEMENT = "activity";
        private const string WORKING_ATTRIBUTE = "working";
        private static XmlDocument doc = new XmlDocument();

        public static XmlNode Serialize(Task task)
        {
            if (task != null)
            {
                XmlNode xml = doc.CreateElement(TASK_ELEMENT);
                xml.Attributes.Append(doc.CreateAttribute(NAME_ATTRIBUTE)).Value = task.Name;
                xml.Attributes.Append(doc.CreateAttribute(WORKING_ATTRIBUTE)).Value =
                    Utilities.BoolToString(task.IsWorking);
                foreach (string activity in task.RelatedActivities)
                {
                    xml.AppendChild(doc.CreateElement(ACTIVITY_ELEMENT)).InnerText = activity;
                }
                foreach (Task subtask in task.Nodes)
                    xml.AppendChild(Serialize(subtask));
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
                        switch (node.Name)
                        {
                            case TASK_ELEMENT:
                                task.Nodes.Add(Deserialize(node));
                                break;
                            case ACTIVITY_ELEMENT:
                                if (node.InnerText != string.Empty)
                                    task.RelatedActivities.Add(node.InnerText);
                                break;
                            default:
                                Log.Error("'{0}' is unsupported element name. Supported elements are: {1},{2}",
                                    node.Name, TASK_ELEMENT, ACTIVITY_ELEMENT);
                                break;
                        }
                        
                    }
                    return task;
                }
            }
            return null;
        }

        public static Task Deserialize(string xml)
        {
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
