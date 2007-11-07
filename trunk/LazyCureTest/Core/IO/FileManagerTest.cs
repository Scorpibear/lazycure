using System.IO;
using LifeIdea.LazyCure.Core.Tasks;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.IO
{
    [TestFixture]
    public class FileManagerTest:Mockery
    {
        private FileManager fileManager;
        [SetUp]
        public void SetUp()
        {
            fileManager = new FileManager();
        }
        [Test]
        public void SaveTasksWriteToFile()
        {
            ITaskCollection mockTaskCollection = NewMock<ITaskCollection>();
            fileManager.TasksFileName = "SaveTasksWriteToFile.tmp";
            File.Delete("SaveTasksWriteToFile.tmp");

            fileManager.SaveTasks(mockTaskCollection);

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
            ITaskCollection mockTaskCollection = NewMock<ITaskCollection>();
            fileManager.TasksFileName = "SaveTasksWriteToFile.tmp";
            File.Delete("FileIsClosing.tmp");

            fileManager.SaveTasks(mockTaskCollection);
            fileManager.SaveTasks(mockTaskCollection);
        }
        [Test]
        public void SaveTasksCheckTaskCollectionForNull()
        {
            Assert.IsFalse(fileManager.SaveTasks(null));
        }
    }
}
