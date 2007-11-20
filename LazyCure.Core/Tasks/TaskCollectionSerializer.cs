using System.IO;
using System.Xml;

namespace LifeIdea.LazyCure.Core.Tasks
{
    public class TaskCollectionSerializer
    {
        private const string ROOT_NODE = "tasks";

        public static XmlNode Serialize(ITaskCollection taskCollection)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.AppendChild(doc.CreateElement(ROOT_NODE));
            foreach (Task task in taskCollection)
            {
                root.InnerXml += TaskSerializer.Serialize(task).OuterXml;
            }
            return doc;
        }

        public static void Serialize(ITaskCollection taskCollection, TextWriter writer)
        {
            writer.WriteLine(Serialize(taskCollection).InnerXml);
        }

        public static ITaskCollection Deserialize(XmlNode xml)
        {
            ITaskCollection taskCollection = new TaskCollection();
            foreach (XmlNode root in xml.ChildNodes)
            {
                if (root.Name == ROOT_NODE)
                {
                    foreach (XmlNode taskXml in root.ChildNodes)
                    {
                        Task task = TaskSerializer.Deserialize(taskXml);
                        if (task != null)
                            taskCollection.Add(task);
                    }
                }
            }
            return taskCollection;
        }

        public static ITaskCollection Deserialize(TextReader reader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            return Deserialize(doc);
        }
    }
}
