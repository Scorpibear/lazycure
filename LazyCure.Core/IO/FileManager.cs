using System;
using System.IO;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;

namespace LifeIdea.LazyCure.Core.IO
{
    /// <summary>
    /// Perform file-related activities
    /// </summary>
    public class FileManager:IFileManager
    {
        public string TasksFileName = "tasks.xml";
        public string timeLogsFolder = null;

        #region IFileManager Members

        public string TimeLogsFolder
        {
            get
            {
                return timeLogsFolder;
            }
            set
            {
                timeLogsFolder = value;
            }
        }

        public ITaskCollection GetTasks()
        {
            StreamReader reader = null;
            ITaskCollection taskCollection = null;
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
                if(reader!=null)
                    reader.Close();
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
            return SaveTimeLog(timeLog, GetTimeLogFileName(timeLog.Date));
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

        public string GetTimeLogFileName(DateTime date)
        {
            return timeLogsFolder + @"\" + date.ToString("yyyy-MM-dd") + ".timelog";
        }
    }
}
