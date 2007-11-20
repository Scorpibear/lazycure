using System;
using System.Text;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using NUnit.Framework;
using NMock2;
using System.IO;
using System.Data;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class DriverTest:Mockery
    {
        Driver driver;
        StringBuilder logStringBuilder;
        const string folder = @"c:\temp\LazyCure\test\TimeLogs";
        static readonly string strDate = "2126-11-18";
        readonly string filename = folder + "\\"+strDate+".timelog";
        DateTime date = DateTime.Parse(strDate);

        [SetUp]
        public void SetUp()
        {
            driver = new Driver();
            logStringBuilder = new StringBuilder();
            Log.Writer = new StringWriter(logStringBuilder);
        }
        [TearDown]
        public void TearDown()
        {
            Log.Close();
        }
        [Test]
        public void DefaultTaskCollection()
        {
            Assert.AreEqual(TaskCollection.Default,driver.TaskCollection);
        }
        [Test]
        public void SaveTimeLog()
        {
            PrepareFolder();
            driver.TimeLogsFolder = folder;
        
            if (driver.Save())
                Assert.Contains(filename, Directory.GetFiles(folder));
            else
                Assert.Fail(logStringBuilder.ToString());
        }
        [Test]
        public void SaveTimeLogSpecifyFileName()
        {
            PrepareFolder();

            if (driver.SaveTimeLog(filename))
                Assert.Contains(filename, Directory.GetFiles(folder));
            else
                Assert.Fail(logStringBuilder.ToString());
        }
        [Test]
        public void SaveTasksAreLoaded()
        {
            driver.TaskCollection.Add(new Task("new task"));
            driver.Save();
            driver = new Driver();
            driver.Load();
            Assert.IsTrue(driver.TaskCollection.Contains("new task"));
        }
        [Test]
        public void NullTimeLogs()
        {
            driver.TimeLogsFolder = "";
            Assert.IsFalse(driver.Save());
            Assert.AreEqual("TimeLogsFolder is not specified\r\n", logStringBuilder.ToString());
        }
        [Test]
        public void LoadTimeLog()
        {
            string sContent = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData><Records>"+
                              "<Activity>changed</Activity><Begin>14:35:02</Begin><Duration>0:00:07</Duration>"+
                              "</Records></LazyCureData>";
            if (File.Exists(filename))
                File.Delete(filename);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            StreamWriter writer = File.CreateText(filename);
            writer.Write(sContent);
            writer.Close();
            driver.TimeLogsFolder = folder;
            Assert.IsTrue(driver.LoadTimeLog(date),"log loaded");
            DataRow row = (driver.TimeLogData as DataTable).Rows[0];
            Assert.AreEqual("changed", row["Activity"],"activity name match");
            Assert.AreEqual(DateTime.Parse("14:35:02"), row["Start"], "start match");
            Assert.AreEqual(TimeSpan.Parse("0:00:07"), row["Duration"], "duration match");
            Assert.AreEqual(date.ToString("yyyy-MM-dd"), driver.TimeLogDate, "current day changed");
        }
        [Test]
        public void LoadSpecifiedTimeLog()
        {
            string sContent = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData><Records>" +
                              "<Activity>changed</Activity><Begin>14:35:02</Begin><Duration>0:00:07</Duration>" +
                              "</Records></LazyCureData>";
                        
            FileInfo fileInfo = new FileInfo(filename);
            fileInfo.Directory.Create();
            StreamWriter writer = fileInfo.CreateText();
            writer.Write(sContent);
            writer.Close();
            driver.FinishActivity("should be removed after load","next");
            if (driver.LoadTimeLog(fileInfo.FullName))
            {
                DataRow row = (driver.TimeLogData as DataTable).Rows[0];
                Assert.AreEqual("changed", row["Activity"], "activity name match");
                Assert.AreEqual(DateTime.Parse("14:35:02"), row["Start"], "start match");
                Assert.AreEqual(TimeSpan.Parse("0:00:07"), row["Duration"], "duration match");
                Assert.AreEqual(strDate, driver.TimeLogDate, "current day changed");
            }
            else
                Assert.Fail(logStringBuilder.ToString());
        }
        [Test]
        public void LoadTwoActivites()
        {
            string sContent = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData>"+
                              "<Records>"+
                              "<Activity>sleep</Activity><Begin>0:00:00</Begin><Duration>5:00:00</Duration>" +
                              "</Records>" +
                              "<Records>" +
                              "<Activity>clean</Activity><Begin>5:00:00</Begin><Duration>0:07:00</Duration>" +
                              "</Records>"+
                              "</LazyCureData>";
            if (File.Exists(filename))
                File.Delete(filename);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            StreamWriter writer = File.CreateText(filename);
            writer.Write(sContent);
            writer.Close();
            driver.TimeLogsFolder = folder;
            driver.LoadTimeLog(date);
            DataRow row1 = (driver.TimeLogData as DataTable).Rows[0];
            DataRow row2 = (driver.TimeLogData as DataTable).Rows[1];
            Assert.AreEqual("sleep", row1["Activity"], "activity name match");
            Assert.AreEqual(DateTime.Parse("0:00:00"), row1["Start"], "start match");
            Assert.AreEqual(TimeSpan.Parse("5:00:00"), row1["Duration"], "duration match");
            Assert.AreEqual("clean", row2["Activity"], "activity name match");
            Assert.AreEqual(DateTime.Parse("5:00:00"), row2["Start"], "start match");
            Assert.AreEqual(TimeSpan.Parse("0:07:00"), row2["Duration"], "duration match");
        }
        [Test]
        public void LoadUnexistingLog()
        {
            Assert.IsFalse(driver.LoadTimeLog(DateTime.Parse("2012-03-16")));
        }
        [Test]
        public void LoadBrokenXml()
        {
            File.AppendAllText("broken.timelog", "incorrect xml content");
            try
            {
                driver.LoadTimeLog("broken.timelog");
            }
            catch (Exception)
            {
                return;
            }
            Assert.Fail("LoadTimeLog does not raise an exception for broken timelog");
        }
        [Test]
        public void TimeLogDate()
        {
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), driver.TimeLogDate);
        }
        [Test]
        public void CurrentActivityDuration()
        {
            DateTime startTime = DateTime.Parse("5:00:00");
            TimeSpan duration = TimeSpan.FromMinutes(15);
            DateTime endTime = startTime + duration;

            ITimeSystem mockTimeSystem = NewMock<ITimeSystem>();

            using (Ordered)
            {
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(startTime));
                Expect.Once.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(endTime));
            }
            driver = new Driver(mockTimeSystem);
            Assert.AreEqual(duration, driver.CurrentActivity.Duration);
        }
        [Test]
        public void LatestActivities()
        {
            driver.FinishActivity("latest", "next");

            Assert.Contains("latest", driver.LatestActivities);
        }
        [Test]
        public void HistoryReused()
        {
            driver.FinishActivity("saved", "next");
            File.Delete("HistoryReused.txt");
            driver.SaveHistory("HistoryReused.txt");
            driver = new Driver();
            driver.LoadHistory("HistoryReused.txt");
            File.Delete("HistoryReused.txt");
            Assert.Contains("saved", driver.LatestActivities);
        }
        [Test]
        public void SaveAfterDoneDefault()
        {
            Assert.AreEqual(true,driver.SaveAfterDone,"Default setting for SaveAfterDone");
            
        }
        [Test]
        public void SaveAfterDone()
        {
            PrepareFolder();
            driver.TimeLogsFolder = folder;
        
            driver.SaveAfterDone = true;
            driver.FinishActivity("should be saved", "next");
            Assert.Contains(filename, Directory.GetFiles(folder));
        }
        [Test]
        public void TimeLogIsNotSavedIfSaveAfterDoneFalse()
        {
            PrepareFolder();
            driver.TimeLogsFolder = folder;
            
            driver.SaveAfterDone = false;
            driver.FinishActivity("should not be saved", "next");

            Assert.AreEqual(false,Directory.Exists(folder),"Existence of folder with timelog");
        }
        [Test]
        public void SaveTasks()
        {
            IFileManager fileManager = NewMock<IFileManager>();
            Expect.AtLeastOnce.On(fileManager).Method("SaveTasks").With(driver.TaskCollection).Will(Return.Value(true));
            driver.FileManager = fileManager;

            bool isSaved = driver.Save();

            Assert.IsTrue(isSaved);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void Load()
        {
            IFileManager fileManager = NewMock<IFileManager>();
            Expect.AtLeastOnce.On(fileManager).Method("GetTasks").Will(Return.Value(null));
            Expect.AtLeastOnce.On(fileManager).Method("GetTimeLog").Will(Return.Value(null));

            bool isLoaded = driver.Load();

            Assert.IsTrue(isLoaded);
        }

        private void PrepareFolder()
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }

            ITimeSystem mockTimeSystem = NewMock<ITimeSystem>();
            Stub.On(mockTimeSystem).GetProperty("Now").Will(Return.Value(DateTime.Parse(strDate + " 5:00:00")));

            driver = new Driver(mockTimeSystem);
        }
    }
}