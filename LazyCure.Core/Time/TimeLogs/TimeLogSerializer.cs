using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.Time.TimeLogs
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

            data.Attributes.Append(xml.CreateAttribute("Date")).Value = Format.Date(timeLog.Date);

            foreach (IActivity activity in timeLog.Activities)
                data.InnerXml += ActivitySerializer.SerializeToString(activity);
            return xml;
        }

        public static void Serialize(ITimeLog timeLog, TextWriter writer)
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
                    date = DateTime.Today;
                timeLog = new TimeLog(date);

                foreach (XmlNode node in data.ChildNodes)
                {
                    if (node.Name == "Records")
                    {
                        IActivity activity = ActivitySerializer.Deserialize(node);
                        timeLog.AddActivity(activity);
                    }
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
