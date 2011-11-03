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
            driver = new Driver();
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
            driver.ExternalPoster = NewMock<IExternalPoster>();
            Stub.On(settings).GetProperty("TimeLogsFolder").Will(Return.Value(@"c:\test"));
            Stub.On(settings).GetProperty("SaveAfterDone").Will(Return.Value(false));
            Stub.On(settings).GetProperty("MaxActivitiesInHistory").Will(Return.Value(13));
            Stub.On(settings).GetProperty("ActivitiesNumberInTray").Will(Return.Value(5));
            Stub.On(settings).GetProperty("ReminderTime").Will(Return.Value(TimeSpan.Parse("0:59:48")));
            Stub.On(settings).GetProperty("UseTweetingActivity").Will(Return.Value(true));
            Stub.On(settings).GetProperty("TweetingActivity").Will(Return.Value("tweeting activity"));
            Stub.On(settings).GetProperty("TwitterAccessToken").Will(Return.Value("token"));
            Stub.On(settings).GetProperty("TwitterAccessTokenSecret").Will(Return.Value("tokenSecret"));
            Stub.On(settings).GetProperty("SwitchTimeLogAtMidnight").Will(Return.Value(false));
            Stub.On(settings).GetProperty("SplitByComma").Will(Return.Value(false));

            Expect.Once.On(driver.ExternalPoster).SetProperty("AccessTokens").To(new TokensPair("token", "tokenSecret"));

            driver.ApplySettings(settings);

            Assert.AreEqual(@"c:\test", driver.TimeLogsFolder);
            Assert.IsFalse(driver.SaveAfterDone);
            Assert.AreEqual(5, driver.History.LatestSize);
            Assert.AreEqual(13, driver.History.Size);
            Assert.AreEqual(TimeSpan.Parse("0:59:48"), driver.TimeManager.MaxDuration);
            Assert.AreEqual("tweeting activity", driver.TimeManager.TweetingActivity);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SetExternalPosterAuthorizationPinUseExternalPoster()
        {
            var externalPoster = NewMock<IExternalPoster>();
            driver.ExternalPoster = externalPoster;
            Expect.Once.On(externalPoster).Method("SetPin").With("testpin").Will(Return.Value(TokensPair.Empty));
            driver.SetExternalPosterAuthorizationPin("testpin");
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void SetExternalPosterAuthorizationWithNullExternalPoster()
        {
            driver.ExternalPoster = null;
            var tokenPair = driver.SetExternalPosterAuthorizationPin("nobody-can-recieve-this");
            Assert.AreEqual(TokensPair.Empty, tokenPair);
        }
        [Test]
        public void TweetingActivityIsSetToNullIfUseTweetingActivityIsFalse()
        {
            ISettings settings = NewMock<ISettings>();
            Stub.On(settings).GetProperty("TimeLogsFolder").Will(Return.Value(@"c:\test"));
            Stub.On(settings).GetProperty("SaveAfterDone").Will(Return.Value(false));
            Stub.On(settings).GetProperty("MaxActivitiesInHistory").Will(Return.Value(13));
            Stub.On(settings).GetProperty("ActivitiesNumberInTray").Will(Return.Value(5));
            Stub.On(settings).GetProperty("ReminderTime").Will(Return.Value(TimeSpan.Parse("0:59:48")));
            Stub.On(settings).GetProperty("UseTweetingActivity").Will(Return.Value(false));
            Stub.On(settings).GetProperty("TweetingActivity").Will(Return.Value("tweeting activity"));
            Stub.On(settings).GetProperty("TwitterAccessToken").Will(Return.Value(string.Empty));
            Stub.On(settings).GetProperty("TwitterAccessTokenSecret").Will(Return.Value(string.Empty));
            Stub.On(settings).GetProperty("SwitchTimeLogAtMidnight").Will(Return.Value(false));
            Stub.On(settings).GetProperty("SplitByComma").Will(Return.Value(false));

            driver.ApplySettings(settings);

            Assert.IsNull(driver.TimeManager.TweetingActivity);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void HistoryActivityIsTrimmed()
        {
            driver.FinishActivity(" act ", "new one");
            Assert.Contains("act", driver.HistoryActivities);
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
            Stub.On(settings).GetProperty("UseTweetingActivity").Will(Return.Value(false));
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
            Stub.On(settings).GetProperty("UseTweetingActivity").Will(Return.Value(false));
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
            driver.FinishActivity("before", "next");
            driver.RenameActivity("before", "after");
            Assert.AreEqual("after", driver.TimeManager.TimeLog.Activities[0].Name);
        }
        [Test]
        public void RenameActivitiesInHistory()
        {
            driver.FinishActivity("before", "next");
            driver.RenameActivity("before", "after");
            Assert.AreEqual("after", driver.HistoryActivities[0]);
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
            driver = new Driver();
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
            driver.FinishActivity("should be removed after load","second");
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
        public void TaskCollectionIsUpdatedInEfficiencyCalculator()
        {
            TaskCollection updatedTasks = new TaskCollection();
            driver.WorkingTime = NewMock<IWorkingTimeManager>();
            Expect.Once.On(driver.WorkingTime).SetProperty("TaskCollection").To(updatedTasks);

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
            Assert.Contains("latest", driver.LatestActivities);
        }
        [Test]
        public void HistoryActivities()
        {
            driver.FinishActivity("latest", "second");
            Assert.Contains("latest", driver.HistoryActivities);
        }
        [Test]
        public void BothActivitiesAfterSplitAppearInHistory()
        {
            driver.TimeManager.SplitByComma = true;
            driver.FinishActivity("first, second", "third");
            string[] historyActivities = driver.HistoryActivities;
            Assert.Contains("first", historyActivities);
            Assert.Contains("second", historyActivities);
        }
        [Test]
        public void LatestActivitiesCallsHistory()
        {
            driver.History = NewMock<IActivitiesHistory>();
            Expect.Once.On(driver.History).GetProperty("LatestActivities").
                Will(Return.Value(new string[] { "test" }));
            Assert.AreEqual(new string[] { "test" }, driver.LatestActivities);
        }
        [Test]
        public void HistoryActivitiesCallsHistory()
        {
            driver.History = NewMock<IActivitiesHistory>();
            Expect.Once.On(driver.History).GetProperty("Activities").
                Will(Return.Value(new string[] { "test" }));
            Assert.AreEqual(new string[]{"test"},driver.HistoryActivities);
        }
        [Test]
        public void LatestActivitiesAreReloaded()
        {
            driver.FinishActivity("saved", "second");
            driver.Save();
            driver = new Driver();
            driver.Load();
            Assert.Contains("saved", driver.LatestActivities);
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
            Expect.AtLeastOnce.On(driver.FileManager).Method("SaveHistory").With(driver.History);

            bool isSaved = driver.Save();
            
            Assert.IsTrue(isSaved);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void Load()
        {
            driver.FileManager = NewMock<IFileManager>();
            Expect.AtLeastOnce.On(driver.FileManager).Method("GetTasks").Will(Return.Value(null));
            Expect.AtLeastOnce.On(driver.FileManager).Method("GetTimeLog").Will(Return.Value(null));
            Expect.AtLeastOnce.On(driver.FileManager).Method("LoadHistory").With(driver.History);
            Stub.On(driver.FileManager).Method("GetTimeLogFileName").Will(Return.Value(@"c:\temp\test.txt"));

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
        public void TweetingIsPosted()
        {
            driver.TimeManager.TweetingActivity = "tweeting";
            driver.FinishActivity("twitter message", "(empty)", true);
            IActivity lastActivity = driver.TimeManager.TimeLog.Activities[0];
            Assert.AreEqual("tweeting", lastActivity.Name);
        }
        [Test]
        public void TheSameIsPosted()
        {
            driver.TimeManager.TweetingActivity = null;
            driver.FinishActivity("twitter message", "(empty)", true);
            IActivity lastActivity = driver.TimeManager.TimeLog.Activities[0];
            Assert.AreEqual("twitter message", lastActivity.Name);
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
        public void TasksSummaryData()
        {
            driver.TasksSummary = NewMock<ITasksSummary>();
            DataTable test = new DataTable("test");
            Expect.Once.On(driver.TasksSummary).GetProperty("Data").Will(Return.Value(test));

            object data = driver.TasksSummaryData;

            Assert.AreEqual(test,data);
            VerifyAllExpectationsHaveBeenMet();
        }
    }
}
