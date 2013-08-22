using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.IO
{
    /// <summary>
    /// Perform file-related activities
    /// </summary>
    public class FileManager:IFileManager
    {
        private string timeLogsFolder = null;

        public string HistoryFileName { get; set; }

        public string TasksFileName { get; set; }

        public FileManager() : this(null) { }

        public FileManager(ITimeLogsFolderSettingSource timeLogsFolderSettingSource)
        {
            if(timeLogsFolderSettingSource!=null)
                this.timeLogsFolder = timeLogsFolderSettingSource.TimeLogsFolder;
            TasksFileName = GetFullFileName("tasks.xml");
            HistoryFileName = GetFullFileName("history.txt");
        }

        #region IFileManager Members

        public string TimeLogsFolder
        {
            get
            {
                return timeLogsFolder;
            }
            set
            {
                if(Utilities.IsFileNameShort(value))
                    timeLogsFolder = GetFullFileName(value);
                else 
                    timeLogsFolder = value;
            }
        }

        public void LoadHistory(IActivitiesHistory history)
        {
            history.Load(HistoryFileName);
        }

        public void SaveHistory(IActivitiesHistory history)
        {
            history.Save(HistoryFileName);
        }

        public ITaskCollection GetTasks()
        {
            StreamReader reader = null;
            ITaskCollection taskCollection = null;
            if (File.Exists(TasksFileName))
            {
                try
                {
                    reader = File.OpenText(TasksFileName);
                    taskCollection = TaskCollectionSerializer.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
            return taskCollection;        
        }

        public ITimeLog GetTimeLog(string filename)
        {
            if (File.Exists(filename))
            {
                StreamReader reader = File.OpenText(filename);
                ITimeLog timeLog = TimeLogSerializer.Deserialize(reader);
                reader.Close();
                DateTime date = Utilities.GetDateFromFileName(filename);
                if (date != DateTime.MinValue)
                    timeLog.Date = date;
                timeLog.FileName = filename;
                return timeLog;
            }
            else
                return null;
        }

        public bool SaveTasks(ITaskCollection taskCollection)
        {
            if(taskCollection!=null)
            {
                StreamWriter writer;
                try
                {
                    writer = File.CreateText(TasksFileName);
                    TaskCollectionSerializer.Serialize(taskCollection, writer);
                    writer.Close();
                    return true;
                }
                catch(Exception ex)
                {
                    Log.Exception(ex);
                }
            }
            return false;
        }

        public bool SaveTimeLog(ITimeLog timeLog)
        {
            if (timeLogsFolder == "")
            {
                Log.Error("TimeLogsFolder is not specified");
                return false;
            }
            return SaveTimeLog(timeLog, GetTimeLogFileName(timeLog));
        }

        public bool SaveTimeLog(ITimeLog timeLog, string filename)
        {
            StreamWriter stream = null;
            try
            {
                FileInfo fileInfo = new FileInfo(filename);
                fileInfo.Directory.Create();
                stream = fileInfo.CreateText();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
            TimeLogSerializer.Serialize(timeLog, stream);
            stream.Close();
            return true;
        }

        #endregion IFileManager Members

        public string GetTimeLogFileName(ITimeLog timeLog)
        {
            if (timeLog == null)
                return null;
            if (timeLog.FileName != null)
                return timeLog.FileName;
            else
                return GetTimeLogFileName(timeLog.Date);
        }

        public string GetTimeLogFileName(DateTime date)
        {
            string filename = Format.Date(date) + ".timelog";
            if (timeLogsFolder != null)
                return Path.Combine(timeLogsFolder, filename);
            else
                return filename;
        }

        public string GetFullFileName(string shortFileName)
        {
            string path = Assembly.GetCallingAssembly().Location;
            FileInfo fileInfo = new FileInfo(path);
            return Path.Combine(fileInfo.DirectoryName, shortFileName);
        }


        public ITimeLog GetTimeLog(DateTime day)
        {
            return this.GetTimeLog(GetTimeLogFileName(day));
        }


        public List<DateTime> AllTimeLogDates
        {
            get
            {
                var days = new List<DateTime>();
                if (this.TimeLogsFolder != null)
                {
                    foreach (string filename in Directory.EnumerateFiles(this.TimeLogsFolder))
                    {
                        FileInfo fileInfo = new FileInfo(filename);
                        string[] fileparts = fileInfo.Name.Split('.');
                        if (fileparts.Length == 2)
                        {
                            string dateFromTimeLogName = fileparts[0];
                            DateTime day;
                            if (DateTime.TryParse(dateFromTimeLogName, out day))
                                days.Add(day);
                        }
                    }
                }
                return days;
            }
        }
    }
}
