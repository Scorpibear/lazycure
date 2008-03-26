using System;
using System.Data;
using System.IO;
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
                        start = ParseDateTime(node.InnerText);
                        break;
                    case "Duration":
                        duration = ParseTimeSpan(node.InnerText);
                        break;
                    case "Activity":
                        name = node.InnerText;
                        break;
                }
            }
            IActivity activity = new Activity(name, start, duration);
            return activity;
        }

        public static DateTime ParseDateTime(string s)
        {
            DateTime parsedValue;
            bool isParsed = DateTime.TryParse(s, out parsedValue);
            if(!isParsed)
            {
                parsedValue = DateTime.Now.Date + ParseTimeSpanOldWay(s);
            }
            return parsedValue;
        }

        public static TimeSpan ParseTimeSpan(string s)
        {
            TimeSpan parsedValue;
            bool isParsed = TimeSpan.TryParse(s, out parsedValue);
            if(!isParsed)
            {
                parsedValue = ParseTimeSpanOldWay(s);
            }
            return parsedValue;
        }

        public static TimeSpan ParseTimeSpanOldWay(string s)
        {
            TimeSpan parsedValue;
            string xml = "<root><activity><time>" + s + "</time></activity></root>";
            DataTable data = new DataTable("activity");
            data.Columns.Add("time", TimeSpan.MinValue.GetType());
            try
            {
                data.ReadXml(new StringReader(xml));
                parsedValue = (TimeSpan) data.Rows[0]["time"];
            }
            catch(Exception)
            {
                parsedValue = TimeSpan.Zero;
            }
            return parsedValue;
        }
    }
}