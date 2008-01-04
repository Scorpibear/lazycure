using System;
using System.Collections.Generic;
using System.IO;
using LifeIdea.LazyCure.Core.IO;

namespace LifeIdea.LazyCure.Core.Activities
{
    /// <summary>
    /// Store history of latests activities
    /// </summary>
    public class ActivitiesHistory
    {
        private readonly List<string> activities = new List<string>();
        
        public int MaxActivities = 30;
        public string[] LatestActivities { get { return activities.ToArray(); } }
        
        public void AddActivity(string activity)
        {
            activities.Remove(activity);
            activities.Insert(0, activity);
            if (activities.Count > MaxActivities)
                activities.RemoveAt(MaxActivities);
        }
        
        public void Load(TextReader reader)
        {
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                else
                {
                    if (!activities.Contains(line))
                    {
                        activities.Add(line);
                        if (activities.Count == MaxActivities)
                            break;
                    }
                }
            }
        }

        public bool Load(string filename)
        {
            if (File.Exists(filename))
            {
                TextReader reader = File.OpenText(filename);
                Load(reader);
                reader.Close();
                return true;
            }
            return false;
        }

        public void Save(TextWriter writer)
        {
            foreach (string activity in activities)
            {
                writer.WriteLine(activity);
            }
        }

        public bool Save(string filename)
        {
            StreamWriter writer = null;
            try
            {
                writer = File.CreateText(filename);
                Save(writer);
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
        public bool ContainsActivity(string activityName)
        {
            return activities.Contains(activityName);
        }
    }
}