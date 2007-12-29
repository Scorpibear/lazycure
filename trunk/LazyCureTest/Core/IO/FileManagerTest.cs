using System;
using System.IO;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.IO
{
    [TestFixture]
    public class FileManagerTest:Mockery
    {
        private FileManager fileManager;
        private string filename = null;
        private readonly string sContent = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData Date=\"2102-03-12\"><Records>" +
                  "<Activity>changed</Activity><Begin>14:35:02</Begin><Duration>0:00:07</Duration>" +
                  "</Records></LazyCureData>";
        [SetUp]
        public void SetUp()
        {
            fileManager = new FileManager();
        }
        [TearDown]
        public void TearDown()
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }catch(Exception)
                {
                }
                filename = null;
            }
        }
        [Test]
        public void SaveTasksWriteToFile()
        {
            ITaskCollection taskCollection = new TaskCollection();
            fileManager.TasksFileName = "SaveTasksWriteToFile.tmp";
            File.Delete("SaveTasksWriteToFile.tmp");

            fileManager.SaveTasks(taskCollection);

            Assert.IsTrue(File.Exists("SaveTasksWriteToFile.tmp"));
        }
        [Test]
        public void DefaultTasksFileName()
        {
            Assert.AreEqual("tasks.xml",fileManager.TasksFileName);
        }
        [Test]
        public void FileIsClosing()
        {
            ITaskCollection taskCollection = new TaskCollection();
            fileManager.TasksFileName = "FileIsClosing.tmp";
            File.Delete("FileIsClosing.tmp");

            fileManager.SaveTasks(taskCollection);
            fileManager.SaveTasks(taskCollection);
        }
        [Test]
        public void SaveTasksCheckTaskCollectionForNull()
        {
            Assert.IsFalse(fileManager.SaveTasks(null));
        }
        [Test]
        public void SaveTasksSerializeTasks()
        {
            ITaskCollection taskCollection = new TaskCollection();
            taskCollection.Add(new Task("task1"));
            fileManager.TasksFileName = "SaveTasksSerializeTasks.tmp";
            File.Delete("SaveTasksSerializeTasks.tmp");

            fileManager.SaveTasks(taskCollection);

            Assert.IsTrue(File.OpenText(fileManager.TasksFileName).ReadToEnd().Contains("task1"));
        }
        [Test]
        public void SaveTasksIfFileIsOpened()
        {
            ITaskCollection taskCollection = new TaskCollection();
            filename = "SaveTasksIfFileIsOpened.tmp";
            File.WriteAllText(filename,"text");
            File.OpenText(filename);

            fileManager.TasksFileName = filename;
            Assert.IsFalse(fileManager.SaveTasks(taskCollection));
        }
        [Test]
        public void GetNotNullTimeLog()
        {
            filename = "GetNotNullTimeLog.timelog";
            File.WriteAllText(filename, sContent);

            ITimeLog timeLog = fileManager.GetTimeLog(filename);
            Assert.IsNotNull(timeLog);
        }
        [Test]
        public void GetTimeLogForUnexistentFile()
        {
            filename = "UnexistentFile.timelog";

            ITimeLog timeLog = fileManager.GetTimeLog(filename);
            Assert.IsNull(timeLog);
        }
        [Test]
        public void GetTimeLogGetDateFromFileName()
        {
            filename = "2013-12-21.timelog";
            File.WriteAllText(filename,sContent);

            ITimeLog timeLog = fileManager.GetTimeLog(filename);
            Assert.AreEqual("2013-12-21",timeLog.Date.ToString("yyyy-MM-dd"));
        }
        [Test]
        public void GetTimeLogDateFromXmlIfFileNameIsNotDate()
        {
            filename = "TimeLog~1.timelog";
            File.WriteAllText(filename, sContent);

            ITimeLog timeLog = fileManager.GetTimeLog(filename);

            Assert.AreEqual("2102-03-12", timeLog.Date.ToString("yyyy-MM-dd"));
        }
        [Test]
        public void GetTasksReturnsWhatWasSaved()
        {
            ITaskCollection taskCollection = new TaskCollection();
            taskCollection.Add(new Task("task1"));
            taskCollection.Add(new Task("task2"));

            fileManager.SaveTasks(taskCollection);
            Assert.AreEqual(taskCollection, fileManager.GetTasks());
        }
        [Test]
        public void SaveTimeLogCreateFile()
        {
            ITimeLog timeLog = new TimeLog(DateTime.Now);
            fileManager.SaveTimeLog(timeLog, "SaveTimeLog.tmp");
            Assert.IsTrue(File.Exists("SaveTimeLog.tmp"));
        }
    }
}
