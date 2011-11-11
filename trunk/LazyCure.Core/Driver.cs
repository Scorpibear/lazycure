using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Core.Localization;
using LifeIdea.LazyCure.Core.Plugins;
using LifeIdea.LazyCure.Core.Reports;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Structures;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Initialize required for LazyCure work objects. Provide data to UI forms.
    /// </summary>
    public class Driver : ILazyCureDriver, ITimeLogsManager
    {
        #region Fields

        private readonly ActivitiesSummary activitiesSummary;
        private IEfficiencyCalculator efficiencyCalculator;
        private IFileManager fileManager = new FileManager();
        private IActivitiesHistory history;
        private ILanguageSwitcher languageSwitcher;
        private ITaskCollection taskCollection = Tasks.TaskCollection.Default;
        private ITimeManager timeManager;
        private ITasksSummary tasksSummary;
        private IWorkingTimeManager workingTime;

        #endregion Fields

        #region Properties

        public IEfficiencyCalculator EfficiencyCalculator
        {
            get { return efficiencyCalculator; }
            set { efficiencyCalculator = value; }
        }

        public IFileManager FileManager
        {
            get { return fileManager; }
            set { fileManager = value; }
        }

        public static string FirstActivityName = "starting LazyCure";

        public IActivitiesHistory History
        {
            get { return history; }
            set { history = value; }
        }

        public ILanguageSwitcher LanguageSwitcher
        {
            get { return languageSwitcher; }
        }

        public bool SaveAfterDone = false;
        
        public ITaskCollection TaskCollection
        {
            get { return taskCollection; }
            set
            {
                taskCollection = value;
                if (activitiesSummary != null)
                    activitiesSummary.Linker = taskCollection;
                if (TasksSummary != null)
                    TasksSummary.TaskCollection = taskCollection;
                if (WorkingTime != null)
                    WorkingTime.TaskCollection = taskCollection;
            }
        }

        public ITaskViewDataSource TaskViewDataSource
        {
            get { return taskCollection as ITaskViewDataSource; }
        }

        public ITasksSummary TasksSummary
        {
            get { return tasksSummary; }
            set{ tasksSummary = value;}
        }

        public ITimeManager TimeManager
        {
            get{ return timeManager;}
            set{ timeManager = value;}
        }

        public string TimeLogsFolder { get { return FileManager.TimeLogsFolder; } set { FileManager.TimeLogsFolder = value; } }

        public IExternalPoster ExternalPoster = new Twitter();

        public IWorkingTimeManager WorkingTime
        {
            get { return workingTime; }
            set { workingTime = value; }
        }

        #endregion Properties

        #region Constructors

        public Driver(ITimeSystem timeSystem, ISettings settings)
        {

            languageSwitcher = new LanguageSwitcher(settings);
            timeManager = new TimeManager(timeSystem, this);
            activitiesSummary = new ActivitiesSummary(TimeManager.TimeLog, TaskCollection);
            tasksSummary = new TasksSummary(activitiesSummary.Data, TaskCollection);
            history = new ActivitiesHistory();
            workingTime = new WorkingTime(TimeManager.TimeLog, TaskCollection);
            efficiencyCalculator = new EfficiencyCalculator(workingTime);
            ApplySettings(settings);
        }

        public Driver(ISettings settings) : this(new NaturalTimeSystem(), settings) { }

        #endregion Constructors

        #region ILazyCureDriver Members

        public object ActivitiesSummaryData { get { return activitiesSummary.Data; } }

        public TimeSpan AllActivitiesTime { get { return activitiesSummary.AllActivitiesTime; } }

        public bool CalculateAutomaticallyWorkingIntervals
        {
            set { WorkingTime.CalculateAutomatically = value; }
        }

        public IActivity CurrentActivity { get { return TimeManager.CurrentActivity; } }

        public double Efficiency { get { return EfficiencyCalculator.Efficiency; } }

        public TimeSpan PossibleWorkInterruptionDuration
        {
            get { return WorkingTime.PossibleWorkInterruption; }
            set { workingTime.PossibleWorkInterruption = value;}
        }

        public object TasksSummaryData
        {
            get { return TasksSummary.Data; }
        }

        public bool TimeToUpdateTimeLog
        {
            get
            {
                return TimeManager.CurrentActivityIsLastingTooLong;
            }
        }

        public TimeSpan TimeOnWork { get { return WorkingTime.TimeOnWork; } }

        public TimeSpan WorkingActivitiesTime
        {
            get { return WorkingTime.WorkingTasksTime; }
        }

        public object WorkingTimeIntervalsData
        {
            get { return WorkingTime.Intervals; }
        }

        public void ApplySettings(ISettings settings)
        {
            if (settings != null)
            {
                TimeLogsFolder = settings.TimeLogsFolder;
                SaveAfterDone = settings.SaveAfterDone;
                History.LatestSize = settings.ActivitiesNumberInTray;
                History.Size = settings.MaxActivitiesInHistory;
                TimeManager.MaxDuration = settings.ReminderTime;
                TimeManager.SplitByComma = settings.SplitByComma;
                TimeManager.SwitchAtMidnight = settings.SwitchTimeLogAtMidnight;
                TimeManager.TweetingActivity = (settings.UseTweetingActivity) ? settings.TweetingActivity :
                                                                                null;
                ExternalPoster.AccessTokens = new TokensPair(settings.TwitterAccessToken, settings.TwitterAccessTokenSecret);
            }
        }

        public void AuthorizeInExternalPoster()
        {
            ExternalPoster.ShowAuthorizationPage();
        }

        public string GetUniqueActivityName()
        {
            return History.UniqueName;
        }

        public void RenameActivity(string before, string after)
        {
            TimeManager.TimeLog.RenameActivities(before, after);
            History.RenameActivity(before, after);
        }

        public void PostToTwitter(string activity)
        {
            ExternalPoster.PostAsync(activity);
        }

        public object TimeLogData { get { return TimeManager.TimeLog.Data; } }

        public void FinishActivity(string finishedActivityName, string nextActivityName, bool postToExternals)
        {
            if (postToExternals)
            {
                string tweet = finishedActivityName;
                string tweetingActivity = TimeManager.TweetingActivity;
                if(tweetingActivity!=null)
                    finishedActivityName = tweetingActivity;
                this.PostToTwitter(tweet);
            }
            List<IActivity> finishedActivities = TimeManager.FinishActivity(finishedActivityName, nextActivityName);
            History.AddActivities(finishedActivities);
            if (SaveAfterDone)
                fileManager.SaveTimeLog(TimeManager.TimeLog);
        }

        public void FinishActivity(string finishedActivityName, string nextActivityName)
        {
            FinishActivity(finishedActivityName, nextActivityName, false);
        }

        public string TimeLogDate { get { return TimeManager.TimeLog.Date.ToString("yyyy-MM-dd"); } }

        public bool LoadTimeLog(string filename)
        {
            ITimeLog loadedTimeLog = fileManager.GetTimeLog(filename);
            if (loadedTimeLog != null)
            {
                UpdateTimeLogReferencies(loadedTimeLog);
                return true;
            }
            else
                return false;
        }

        public TokensPair SetExternalPosterAuthorizationPin(string pin)
        {
            if (ExternalPoster != null)
                return ExternalPoster.SetPin(pin);
            return TokensPair.Empty;
        }

        public void UpdateTimeLogReferencies(ITimeLog timeLog)
        {
            TimeManager.TimeLog = timeLog;
            activitiesSummary.TimeLog = TimeManager.TimeLog;
            activitiesSummary.Update();
            workingTime.TimeLog = TimeManager.TimeLog;
        }

        public bool LoadTimeLog(DateTime date)
        {
            string filename = fileManager.GetTimeLogFileName(date);
            return LoadTimeLog(filename);
        }

        public string[] HistoryActivities
        {
            get { return History.Activities; }
        }

        public string[] LatestActivities
        {
            get { return History.LatestActivities; }
        }

        public bool Save()
        {
            fileManager.SaveHistory(History);
            fileManager.SaveTasks(TaskCollection);
            return fileManager.SaveTimeLog(TimeManager.TimeLog);
        }

        public bool SaveTimeLog(string filename)
        {
            return fileManager.SaveTimeLog(TimeManager.TimeLog, filename);
        }

        public void UpdateIsWorkingTaskProperty(string task, bool isWorking)
        {
            TaskCollection.UpdateIsWorkingProperty(task, isWorking);
        }

        #endregion

        #region Private Members

        public bool Load()
        {
            ITaskCollection loadedTasks = fileManager.GetTasks();
            if (loadedTasks != null)
                TaskCollection = loadedTasks;
            fileManager.LoadHistory(History);
            LoadTimeLog(TimeManager.TimeSystem.Now);
            return true;
        }

        #endregion Private Members
    }
}
