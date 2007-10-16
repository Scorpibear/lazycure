using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Serialize core objects.
    /// </summary>
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
            records.AppendChild(xml.CreateElement("Activity")).InnerText = activity.Name;
            records.AppendChild(xml.CreateElement("Start")).InnerText = Format.Time(activity.StartTime);
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
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", null, "yes"));
            XmlNode data = xml.AppendChild(xml.CreateElement("LazyCureData"));

            XmlAttribute versionAttribute = data.Attributes.Append(xml.CreateAttribute("LazyCureVersion"));
            string fullname = Assembly.GetExecutingAssembly().FullName;
            string version = Regex.Match(fullname, @"Version=(\d\.\d)").Groups[1].Value;
            versionAttribute.Value = version;

            foreach (IActivity activity in timeLog.Activities)
                data.InnerXml += ActivityToString(activity);
            return xml;
        }
        internal static string TimeLogToString(ITimeLog timeLog)
        {
            return TimeLogToXml(timeLog).OuterXml;
        }
    }
}