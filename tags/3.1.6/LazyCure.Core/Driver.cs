using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.IO;
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
        private IFileManager fileManager = new FileManager();
        private readonly ActivitiesSummary activitiesSummary;
        private readonly string historyFileName = "history.txt";
        public readonly ActivitiesHistory History;

        public static string FirstActivityName = "starting LazyCure";
        public ITaskActivityLinker Linker;
        public bool SaveAfterDone=true;
        public ITaskCollection TaskCollection=Tasks.TaskCollection.Default;
        public ITimeManager TimeManager;
        public string TimeLogsFolder { get { return FileManager.TimeLogsFolder; } set { FileManager.TimeLogsFolder = value; } }

        public Driver(ITimeSystem timeSystem)
        {
            TimeManager = new TimeManager(timeSystem);
            TimeManager.TimeLog = new TimeLog(timeSystem.Now.Date);
            Linker = new TaskActivityLinker(TaskCollection);
            activitiesSummary = new ActivitiesSummary(TimeManager.TimeLog,Linker);
            History = new ActivitiesHistory();
        }
        
        public Driver() : this(new NaturalTimeSystem()) { }

        public void LoadHistory(string filename)
        {
            History.Load(filename);
        }
        
        public void SaveHistory(string filename)
        {
            History.Save(filename);
        }

        public bool Load()
        {
            ITaskCollection loadedTasks = fileManager.GetTasks();
            if (loadedTasks != null)
                TaskCollection = loadedTasks;
            Linker.TaskCollection = loadedTasks;
            LoadHistory(historyFileName);
            LoadTimeLog(TimeManager.TimeSystem.Now);
            return true;
        }

        #region ILazyCureDriver Members

        public TimeSpan AllActivitiesTime { get { return activitiesSummary.AllActivitiesTime; } }

        public bool TimeToUpdateTimeLog
        {
            get
            {
                return TimeManager.CurrentActivityIsLastingTooLong;
            }
        }


        public IActivity CurrentActivity { get { return TimeManager.CurrentActivity; } }

        public object ActivitiesSummaryData { get { return activitiesSummary.Data; } }

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

        public void FillTaskNodes(TreeNodeCollection nodes)
        {
            nodes.AddRange(TaskCollection.ToArray());
        }

        public void UpdateTaskNodeText(TreeNode node, string text)
        {
            if(TaskCollection.Contains(node.Name))
                TaskCollection.GetTask(node.Name).Text = text;
            else
                TaskCollection.Add(new Task(text));
        }

        public object TimeLogData { get { return TimeManager.TimeLog.Data; } }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            TimeManager.FinishActivity(finishedActivity, nextActivity);
            History.AddActivity(finishedActivity);
            if(SaveAfterDone)
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

        public IFileManager FileManager
        {
            get { return fileManager; }
            set { fileManager = value; }
        }

        public bool Save()
        {
            SaveHistory("history.txt");
            fileManager.SaveTasks(TaskCollection);
            return fileManager.SaveTimeLog(TimeManager.TimeLog);
        }

        public bool SaveTimeLog(string filename)
        {
            return fileManager.SaveTimeLog(TimeManager.TimeLog, filename);
        }

        #endregion
    }
}