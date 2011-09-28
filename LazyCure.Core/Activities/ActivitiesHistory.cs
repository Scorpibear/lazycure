using System;
using System.Collections.Generic;
using System.IO;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Activities
{
    /// <summary>
    /// Store history of latests activities
    /// </summary>
    public class ActivitiesHistory:IActivitiesHistory
    {
        public const int DEFAULT_SIZE = 30;
        public const int DEFAULT_LATEST_SIZE = 15;

        private readonly List<string> activities = new List<string>();
        private int size = DEFAULT_SIZE;
        private int latestSize = DEFAULT_LATEST_SIZE;

        public string[] Activities { get { return activities.ToArray(); } }
        public string[] LatestActivities { get {
            int actualLatestSize = Math.Min(activities.Count, latestSize);
            string[] array = new string[actualLatestSize];
            activities.CopyTo(0, array, 0, actualLatestSize); 
            return array;
        } }
        public int LatestSize { get { return latestSize; } set { latestSize = value; } }
        public int Size { get { return size; } set { size = value; } }

        public string UniqueName
        {
            get
            {
                int i = 1;
                do
                {
                    string candidate = Constants.GeneratedActivity + i.ToString();
                    if (activities.Contains(candidate))
                        i++;
                    else
                        return candidate;
                } while (true);
            }
        }

        public void AddActivity(string activity)
        {
            activities.Remove(activity);
            activities.Insert(0, activity);
            if (activities.Count > size)
                activities.RemoveAt(size);
        }

        public void AddActivities(List<IActivity> activities)
        {
            foreach (IActivity activity in activities)
                AddActivity(activity.Name);
        }

        public bool ContainsActivity(string activityName)
        {
            return activities.Contains(activityName);
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
                        if (activities.Count == size)
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

        public void RenameActivity(string before, string after)
        {
            activities.Remove(before);
            AddActivity(after);
        }
    }
}
