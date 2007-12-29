using System;
using System.Xml;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Activities
{
    /// <summary>
    /// Serialize and deserialize activities
    /// </summary>
    public class ActivitySerializer
    {
        public static XmlNode SerializeToXml(IActivity activity)
        {
            XmlDocument xml = new XmlDocument();
            XmlNode records = xml.AppendChild(xml.CreateElement("Records"));
            records.AppendChild(xml.CreateElement("Activity")).InnerText = activity.Name;
            records.AppendChild(xml.CreateElement("Start")).InnerText = Format.Time(activity.StartTime);
            records.AppendChild(xml.CreateElement("Duration")).InnerText = Format.Duration(activity.Duration);
            return records;
        }

        public static string SerializeToString(IActivity activity)
        {
            return SerializeToXml(activity).OuterXml;
        }

        public static IActivity Deserialize(XmlNode xml)
        {
            string name="";
            DateTime start=DateTime.Now;
            TimeSpan duration=new TimeSpan();
            foreach(XmlNode node in xml.ChildNodes)
            {
                switch(node.Name)
                {
                    case "Begin":
                    case "Start":
                        start = DateTime.Parse(node.InnerText);
                        break;
                    case "Duration":
                        duration = TimeSpan.Parse(node.InnerText);
                        break;
                    case "Activity":
                        name = node.InnerText;
                        break;
                }
            }
            IActivity activity = new Activity(name, start, duration);
            return activity;
        }
    }
}