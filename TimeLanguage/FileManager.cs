using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Interfaces;
using System.Xml;
using System.IO;

namespace LifeIdea.TimeLanguage
{
    public class FileManager
    {
        public const string FILENAME = "today.timelog";
        public IActivity Load()
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(FILENAME);
                XmlNode xml = document.FirstChild;
                return ActivitySerializer.Deserialize(xml);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public void Save(IActivity activity)
        {
            File.WriteAllText(FILENAME,ActivitySerializer.SerializeToString(activity));
        }
    }
}
