using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Interfaces;
using LifeIdea.LazyCure.Core.IO;

namespace LifeIdea.LazyCure.Core.Time
{
    /// <summary>
    /// Serialize and Deserialize TimeLog
    /// </summary>
    public class TimeLogSerializer
    {
        public static XmlNode Serialize(ITimeLog timeLog)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", null, "yes"));
            XmlNode data = xml.AppendChild(xml.CreateElement("LazyCureData"));

            XmlAttribute versionAttribute = data.Attributes.Append(xml.CreateAttribute("LazyCureVersion"));
            string fullname = Assembly.GetExecutingAssembly().FullName;
            string version = Regex.Match(fullname, @"Version=(\d\.\d)").Groups[1].Value;
            versionAttribute.Value = version;

            data.Attributes.Append(xml.CreateAttribute("Date")).Value = timeLog.Date.ToString("yyyy-MM-dd");

            foreach (IActivity activity in timeLog.Activities)
                data.InnerXml += ActivitySerializer.SerializeToString(activity);
            return xml;
        }

        public static void Serialize(TimeLog timeLog, TextWriter writer)
        {
            writer.WriteLine(Serialize(timeLog).OuterXml);
        }

        public static ITimeLog Deserialize(XmlNode xml)
        {
            XmlNode data = xml["LazyCureData"];
            ITimeLog timeLog;
            if (data != null)
            {
                XmlAttribute dateAttribute = data.Attributes["Date"];
                DateTime date;
                if (dateAttribute != null)
                    date = DateTime.Parse(dateAttribute.Value);
                else
                    date = DateTime.Now.Date;
                timeLog = new TimeLog(date);

                foreach (XmlNode node in data.ChildNodes)
                {
                    IActivity activity = ActivitySerializer.Deserialize(node);
                    timeLog.AddActivity(activity);
                }
            }
            else
            {
                timeLog = new TimeLog(DateTime.Now.Date);
            }
            return timeLog;
        }

        public static ITimeLog Deserialize(TextReader reader)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(reader);
            return Deserialize(xml);
        }
    }
}
