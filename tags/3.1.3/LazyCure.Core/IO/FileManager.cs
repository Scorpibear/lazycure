using System;
using System.IO;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;

namespace LifeIdea.LazyCure.Core.IO
{
    /// <summary>
    /// Create streams for working with files
    /// </summary>
    public class FileManager:IFileManager
    {
        public string TasksFileName = "tasks.xml";

        #region IFileManager Members

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

        #endregion IFileManager Members
    }
}
