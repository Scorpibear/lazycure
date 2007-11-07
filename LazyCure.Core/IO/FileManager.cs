using System.IO;
using LifeIdea.LazyCure.Core.Tasks;

namespace LifeIdea.LazyCure.Core.IO
{
    /// <summary>
    /// Create streams for working with files
    /// </summary>
    public class FileManager:IFileManager
    {
        public string TasksFileName = "tasks.xml";

        public bool SaveTasks(ITaskCollection taskCollection)
        {
            if(taskCollection!=null)
            {
                File.CreateText(TasksFileName).Close();
                return true;
            }
            else 
                return false;
        }
    }
}
