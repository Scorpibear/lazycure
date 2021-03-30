using System;
using System.Data;
using System.IO;
using System.Text;
using NMock2;
using NUnit.Framework;
using Is=NMock2.Is;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Core.Plugins;
using LifeIdea.LazyCure.Core.Reports;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Tools;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Structures;

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

        // just in order to have main in the assembly and be console application for being able to write to console
        static void Main() { }

        [SetUp]
        public void SetUp()
        {
            driver = new Driver(null);
            logStringBuilder = new StringBuilder();
            Log.Writer = new StringWriter(logStringBuilder);
        }
        [TearDown]
        public void TearDown()
        {
            VerifyAllExpectationsHaveBeenMet();
            Log.Close();
        }
        [Test]
        public void ApplySettings()
        {
            ISettings settings = NewMock<ISettings>();
            driver.HistoryDataProvider = NewMock<IHistoryDataProvider>();
            Expect.Once.On(driver.HistoryDataProvider).Method("ApplySettings").With(settings);
            Stub.On(settings).GetProperty("SaveAfterDone").Will(Return.Value(false));
            Stub.On(settings).GetProperty("ReminderTime").Will(Return.Value(TimeSpan.Parse("0:59:48")));
            Stub.On(settings).GetProperty("SwitchTimeLogAtMidnight").Will(Return.Value(false));
            Stub.On(settings).GetProperty("SplitByComma").Will(Return.Value(false));

            driver.ApplySettings(settings);

            Assert.IsFalse(driver.SaveAfterDone);
            Assert.AreEqual(TimeSpan.Parse("0:59:48"), driver.TimeManager.MaxDuration);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void HistoryActivityIsTrimmed()
        {
            driver.FinishActivity(" act ", "new one");
            Assert.Contains("act", driver.HistoryDataProvider.HistoryActivities);
        }
        [Test]
        public void SwitchTimeLogAtMidnightSettingIsApplied()
        {
            ISettings settings = NewMock<ISettings>();
            driver.TimeManager = NewMock<ITimeManager>();
            Stub.On(settings).GetProperty("SwitchTimeLogAtMidnight").Will(Return.Value(false));
            Stub.On(settings).GetProperty("SaveAfterDone").Will(Return.Value(false));
            Stub.On(settings).GetProperty("SplitByComma").Will(Return.Value(false));
            Stub.On(settings).GetProperty("ActivitiesNumberInTray").Will(Return.Value(0));
            Stub.On(settings).GetProperty("MaxActivitiesInHistory").Will(Return.Value(0));
            Stub.On(settings).GetProperty("ReminderTime").Will(Return.Value(TimeSpan.Zero));
            Stub.On(settings).Method(Is.Anything);
            Expect.Once.On(driver.TimeManager).SetProperty("SwitchAtMidnight").To(false);
            Stub.On(driver.TimeManager).Method(Is.Anything);
            driver.ApplySettings(settings);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SplitByCommaSettingIsApplied()
        {
            ISettings settings = NewMock<ISettings>();
            driver.TimeManager = NewMock<ITimeManager>();
            Stub.On(settings).GetProperty("SwitchTimeLogAtMidnight").Will(Return.Value(false));
            Stub.On(settings).GetProperty("SaveAfterDone").Will(Return.Value(false));
            Stub.On(settings).GetProperty("SplitByComma").Will(Return.Value(true));
            Stub.On(settings).GetProperty("ActivitiesNumberInTray").Will(Return.Value(0));
            Stub.On(settings).GetProperty("MaxActivitiesInHistory").Will(Return.Value(0));
            Stub.On(settings).GetProperty("ReminderTime").Will(Return.Value(TimeSpan.Zero));
            Stub.On(settings).Method(Is.Anything);
            Expect.Once.On(driver.TimeManager).SetProperty("SplitByComma").To(true);
            Stub.On(driver.TimeManager).Method(Is.Anything);
            driver.ApplySettings(settings);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void CalculateAutomaticallyWorkingIntervals()
        {
            driver.WorkingTime = NewMock<IWorkingTimeManager>();
            Expect.Once.On(driver.WorkingTime).SetProperty("CalculateAutomatically").To(false);
            driver.CalculateAutomaticallyWorkingIntervals = false;
        }
        [Test]
        public void DefaultTaskCollection()
        {
            Assert.AreEqual(TaskCollection.Default,driver.TaskCollection);
        }
        [Test]
        public void GetEfficiency()
        {
            driver.EfficiencyCalculator = NewMock<IEfficiencyCalculator>();
            Expect.Once.On(driver.EfficiencyCalculator).GetProperty("Efficiency").Will(Return.Value(0.76));
            Assert.AreEqual(0.76,driver.Efficiency);
        }
        [Test]
        public void RenameActivitiesInTimeLog()
        {
            driver.TimeManager.TimeLog.Data.Clear();
            driver.FinishActivity("before", "next");
            driver.RenameActivity("before", "after");
            Assert.AreEqual("after", driver.TimeManager.TimeLog.Activities[0].Name);
        }
        [Test]
        public void RenameActivitiesInHistory()
        {
            driver.FinishActivity("before", "next");
            driver.RenameActivity("before", "after");
            Assert.AreEqual("after", driver.HistoryDataProvider.HistoryActivities[0]);
        }
        [Test]
        public void GetPossibleWorkInterruptionDuration()
        {
            driver.WorkingTime = NewMock<IWorkingTimeManager>();
            Expect.Once.On(driver.WorkingTime).GetProperty("PossibleWorkInterruption").Will(Return.Value(TimeSpan.MaxValue));
            Assert.AreEqual(TimeSpan.MaxValue, driver.PossibleWorkInterruptionDuration);
        }
        [Test]
        public void SetPossibleWorkInterruptionDuration()
        {
            driver.WorkingTime = NewMock<IWorkingTimeManager>();
            Expect.Once.On(driver.WorkingTime).SetProperty("PossibleWorkInterruption").To(TimeSpan.MaxValue);
            driver.PossibleWorkInterruptionDuration = TimeSpan.MaxValue;
        }
        [Test]
        public void SaveTasksAreLoaded()
        {
            driver.TaskCollection.Add(new Task("new task"));
            driver.Save();
            driver = new Driver(null);
            driver.Load();
            Assert.IsTrue(driver.TaskCollection.Contains("new task"));
        }
        [Test]
        public void NullTimeLogs()
        {
            driver.TimeLogsFolder = "";
            Assert.IsFalse(driver.Save());
            Assert.AreEqual("TimeLogsFolder is not specified", Log.LastError);
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
        public void TaskCollectionIsUpdatedInEfficiencyCalculator()
        {
            TaskCollection updatedTasks = new TaskCollection();
            driver.WorkingTime = NewMock<IWorkingTimeManager>();
            Expect.Once.On(driver.WorkingTime).SetProperty("WorkDefiner").To(updatedTasks);

            driver.TaskCollection = updatedTasks;
        }
        [Test]
        public void TimeLogDate()
        {
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), driver.TimeLogDate);
        }
        [Test]
        public void LatestActivities()
        {
            driver.FinishActivity("latest", "second");
            Assert.Contains("latest", driver.HistoryDataProvider.LatestActivities);
        }
        [Test]
        public void HistoryActivities()
        {
            driver.FinishActivity("latest", "second");
            Assert.Contains("latest", driver.HistoryDataProvider.HistoryActivities);
        }
        [Test]
        public void BothActivitiesAfterSplitAppearInHistory()
        {
            driver.TimeManager.SplitByComma = true;
            driver.FinishActivity("first, second", "third");
            string[] historyActivities = driver.HistoryDataProvider.HistoryActivities;
            Assert.Contains("first", historyActivities);
            Assert.Contains("second", historyActivities);
        }
        [Test]
        public void LatestActivitiesAreReloaded()
        {
            driver.FinishActivity("saved", "second");
            driver.Save();
            driver = new Driver(null);
            driver.Load();
            Assert.Contains("saved", driver.HistoryDataProvider.LatestActivities);
        }
        [Test]
        public void SaveAfterDone()
        {
            driver.FileManager = NewMock<IFileManager>();
            Expect.AtLeastOnce.On(driver.FileManager).Method("SaveTimeLog").Will(Return.Value(true));
            Stub.On(driver.FileManager).SetProperty("TimeLogsFolder");
            driver.TimeLogsFolder = folder;
            driver.SaveAfterDone = true;
            
            driver.FinishActivity("should be saved", "second");
            
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void FileManagerUpdatesReferenceInTimeLogsManager()
        {
            var fileManager = new FileManager();
            driver.FileManager = fileManager;
            Assert.AreSame(fileManager, driver.TimeLogsManager.FileManager);
        }
        [Test]
        public void TimeLogIsNotSavedIfSaveAfterDoneFalse()
        {
            driver.FileManager = NewMock<IFileManager>();
            Expect.Never.On(driver.FileManager).Method("SaveTimeLog");
            Stub.On(driver.FileManager).SetProperty("TimeLogsFolder");
            driver.TimeLogsFolder = folder;
            driver.SaveAfterDone = false;

            driver.FinishActivity("should not be saved", "second");

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void Save()
        {
            driver.FileManager = NewMock<IFileManager>();
            Expect.AtLeastOnce.On(driver.FileManager).Method("SaveTasks").With(driver.TaskCollection).Will(Return.Value(true));
            Expect.AtLeastOnce.On(driver.FileManager).Method("SaveTimeLog").Will(Return.Value(true));
            Expect.AtLeastOnce.On(driver.FileManager).Method("SaveHistory").With(driver.HistoryDataProvider.ActivitiesHistory);

            bool isSaved = driver.Save();
            
            Assert.IsTrue(isSaved);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void Load()
        {
            var fileManager = NewMock<IFileManager>();
            driver.FileManager = fileManager;
            var timeLogsManager = NewMock<ITimeLogsManager>();
            driver.TimeLogsManager = timeLogsManager;
            Expect.AtLeastOnce.On(fileManager).Method("GetTasks").Will(Return.Value(null));
            Expect.AtLeastOnce.On(fileManager).Method("LoadHistory").With(driver.HistoryDataProvider.ActivitiesHistory);
            Expect.AtLeastOnce.On(timeLogsManager).Method("ActivateTimeLog").WithAnyArguments();

            bool isLoaded = driver.Load();

            Assert.IsTrue(isLoaded);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void TimeOnWork()
        {
            driver.WorkingTime = NewMock<IWorkingTimeManager>();
            Expect.AtLeastOnce.On(driver.WorkingTime).GetProperty("TimeOnWork").Will(Return.Value(TimeSpan.MaxValue));
            
            Assert.AreEqual(TimeSpan.MaxValue, driver.TimeOnWork);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void TimeToUpdateTimeLogUsesTimeManager()
        {
            driver.TimeManager = NewMock<ITimeManager>();
            
            Expect.AtLeastOnce.On(driver.TimeManager).GetProperty("CurrentActivityIsLastingTooLong").Will(Return.Value(true));

            Assert.IsTrue(driver.TimeToUpdateTimeLog);

            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void WorkingActivitiesTime()
        {
            driver.WorkingTime = NewMock<IWorkingTimeManager>();
            Expect.Once.On(driver.WorkingTime).GetProperty("WorkingTasksTime").Will(Return.Value(TimeSpan.Parse("0:15")));
            Assert.AreEqual(TimeSpan.Parse("0:15"),driver.WorkingActivitiesTime);
        }
        [Test]
        public void WorkingTimeIntervalsDataUsesWorkingTimeIntervals()
        {
            driver.WorkingTime = NewMock<IWorkingTimeManager>();
            DataTable test = new DataTable("test");
            Expect.Once.On(driver.WorkingTime).GetProperty("Intervals").Will(Return.Value(test));
            
            Assert.AreEqual(test, driver.WorkingTimeIntervalsData);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void UpdateIsWorkingTaskProperty()
        {
            driver.TaskCollection = NewMock<ITaskCollection>();
            Expect.Once.On(driver.TaskCollection).Method("UpdateIsWorkingProperty").With("updated task", true);
            driver.UpdateIsWorkingTaskProperty("updated task",true);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void DriverConstructorSetsTimeLogsManagerToHistoryDataProvider()
        {
            Assert.IsNotNull(driver.HistoryDataProvider.TimeLogsManager);
        }
        [Test]
        public void TaskCollectionIsSetToHistoryDataProvider()
        {
            Assert.AreSame(driver.TaskCollection, (driver.HistoryDataProvider as HistoryDataProvider).TaskCollection);
        }
        [Test]
        public void ChangedTaskCollectionIsChangedInHistoryDataProvider()
        {
            driver.TaskCollection = NewMock<ITaskCollection>();
            Assert.AreSame(driver.TaskCollection, (driver.HistoryDataProvider as HistoryDataProvider).TaskCollection);
        }
    }
}
