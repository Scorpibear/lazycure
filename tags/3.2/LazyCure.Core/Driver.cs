using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Core.Reports;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Initialize required for LazyCure work objects. Provide data to UI forms.
    /// </summary>
    public class Driver : ILazyCureDriver
    {
        #region Fields

        private readonly ActivitiesSummary activitiesSummary;
        private IEfficiencyCalculator efficiencyCalculator;
        private IFileManager fileManager = new FileManager();
        private ActivitiesHistory history;
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

        public ActivitiesHistory History
        {
            get { return history; }
            set { history = value; }
        }

        public bool SaveAfterDone = true;
        
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

        public IWorkingTimeManager WorkingTime
        {
            get { return workingTime; }
            set { workingTime = value; }
        }

        #endregion Properties

        public Driver(ITimeSystem timeSystem)
        {
            timeManager = new TimeManager(timeSystem);
            timeManager.TimeLog = new TimeLog(timeSystem.Now.Date);
            activitiesSummary = new ActivitiesSummary(TimeManager.TimeLog, TaskCollection);
            tasksSummary = new TasksSummary(activitiesSummary.Data, TaskCollection);
            history = new ActivitiesHistory();
            workingTime = new WorkingTime(TimeManager.TimeLog, TaskCollection);
            efficiencyCalculator = new EfficiencyCalculator(workingTime);
        }

        public Driver() : this(new NaturalTimeSystem()) { }

        public bool Load()
        {
            ITaskCollection loadedTasks = fileManager.GetTasks();
            if (loadedTasks != null)
                TaskCollection = loadedTasks;
            fileManager.LoadHistory(History);
            LoadTimeLog(TimeManager.TimeSystem.Now);
            return true;
        }

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

        public TreeNode[] TasksNodes
        {
            get
            {
                return TaskCollection.ToArray();
            }
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
                History.MaxActivities = settings.MaxActivitiesInHistory;
                TimeManager.MaxDuration = settings.ReminderTime;
            }
        }

        public bool IsWorkingTask(string SelectedTask)
        {
            return TaskCollection.IsWorking(SelectedTask);
        }

        public void UpdateTaskNodeText(TreeNode node, string text)
        {
            if (TaskCollection.Contains(node.Name))
                TaskCollection.GetTask(node.Name).Text = text;
            else
                TaskCollection.Add(new Task(text));
        }

        public object TimeLogData { get { return TimeManager.TimeLog.Data; } }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            TimeManager.FinishActivity(finishedActivity, nextActivity);
            History.AddActivity(finishedActivity);
            if (SaveAfterDone)
                fileManager.SaveTimeLog(TimeManager.TimeLog);
        }

        public string TimeLogDate { get { return TimeManager.TimeLog.Date.ToString("yyyy-MM-dd"); } }

        public bool LoadTimeLog(string filename)
        {
            ITimeLog loadedTimeLog = fileManager.GetTimeLog(filename);
            if (loadedTimeLog != null)
            {
                TimeManager.TimeLog = loadedTimeLog;
                activitiesSummary.TimeLog = loadedTimeLog;
                activitiesSummary.Update();
                workingTime.TimeLog = loadedTimeLog;
                return true;
            }
            else
                return false;
        }

        public bool LoadTimeLog(DateTime date)
        {
            string filename = fileManager.GetTimeLogFileName(date);
            return LoadTimeLog(filename);
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
    }
}