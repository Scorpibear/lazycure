using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    public class Serializer
    {
        
        public static ITimeLog XmlToTimeLog(XmlNode xml)
        {
            return null;
        }

        public static XmlNode ActivityToXml(IActivity activity)
        {
            XmlDocument xml = new XmlDocument();
            XmlNode records = xml.AppendChild(xml.CreateElement("Records"));
            records.AppendChild(xml.CreateElement("Activity")).InnerText=activity.Name;
            records.AppendChild(xml.CreateElement("Begin")).InnerText = Format.Time(activity.StartTime);
            records.AppendChild(xml.CreateElement("Duration")).InnerText = Format.Duration(activity.Duration);
            return xml;
        }
        public static string ActivityToString(IActivity activity)
        {
            return ActivityToXml(activity).OuterXml;
        }
        public static XmlNode TimeLogToXml(ITimeLog timeLog)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", null,"yes"));
            XmlNode data = xml.AppendChild(xml.CreateElement("LazyCureData"));
            foreach (IActivity activity in timeLog.Activities)
                data.InnerXml += ActivityToString(activity);
            return xml;
        }

        internal static object TimeLogToString(ITimeLog timeLog)
        {
            return TimeLogToXml(timeLog).OuterXml;
        }
    }
}
