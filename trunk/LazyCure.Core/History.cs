using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using LifeIdea.LazyCure.Interfaces;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    public class History
    {
        private List<string> activities = new List<string>();

        public string[] LatestActivities { get { return activities.ToArray(); } }

        public void AddActivity(string activity)
        {
            activities.Remove(activity);
            activities.Insert(0, activity);
        }

        public bool Load(string filename)
        {
            if (File.Exists(filename))
            {
                StreamReader reader = File.OpenText(filename);
                string line;
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;
                    else
                    {
                        if (!activities.Contains(line))
                            activities.Add(line);
                    }
                }
                reader.Close();
                return true;
            }
            return false;

        }

        public bool Save(string filename)
        {
            StreamWriter writer = null;
            try
            {
                writer = File.CreateText(filename);
                foreach (string activity in activities)
                {
                    writer.WriteLine(activity);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return true;
        }
    }
}
