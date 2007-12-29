using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Interfaces;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Initialize required for LazyCure work objects. Provide data to UI forms.
    /// </summary>
    public class Driver : ILazyCureDriver
    {
        private IFileManager fileManager = new FileManager();
        private readonly ITimeManager timeManager;
        private ActivitiesSummary activitiesSummary;
        private readonly ITaskActivityLinker linker;
        private string timeLogsFolder;
        private readonly string historyFileName = "history.txt";
        private readonly ActivitiesHistory history;

        public static string FirstActivityName = "starting LazyCure";
        public bool SaveAfterDone=true;
        public ITaskCollection TaskCollection=null;
        public ITimeLog timeLog;
        public string TimeLogsFolder { get { return timeLogsFolder; } set { timeLogsFolder = value; } }

        public Driver(ITimeSystem timeSystem)
        {
            timeLog = new TimeLog(timeSystem.Now.Date);
            timeManager = new TimeManager(timeSystem);
            timeManager.TimeLog = timeLog;
            TaskCollection = Tasks.TaskCollection.Default;
            linker = new TaskActivityLinker(TaskCollection);
            activitiesSummary = new ActivitiesSummary(timeLog,linker);
            history = new ActivitiesHistory();
        }
        
        public Driver() : this(new NaturalTimeSystem()) { }

        public void LoadHistory(string filename)
        {
            history.Load(filename);
        }
        
        public void SaveHistory(string filename)
        {
            history.Save(filename);
        }

        public bool Load()
        {
            ITaskCollection loadedTasks = fileManager.GetTasks();
            if (loadedTasks != null)
                TaskCollection = loadedTasks;
            LoadHistory(historyFileName);
            LoadTimeLog(timeManager.TimeSystem.Now);
            return true;
        }

        #region ILazyCureDriver Members

        public TimeSpan AllActivitiesTime { get { return activitiesSummary.AllActivitiesTime; } }

        public IActivity CurrentActivity { get { return timeManager.CurrentActivity; } }

        public object ActivitiesSummaryData { get { return activitiesSummary.Data; } }

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

        public object TimeLogData { get { return timeLog.Data; } }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            timeManager.FinishActivity(finishedActivity, nextActivity);
            history.AddActivity(finishedActivity);
            if(SaveAfterDone)
                SaveTimeLog();
        }

        public string TimeLogDate { get { return timeLog.Date.ToString("yyyy-MM-dd"); } }

        public bool LoadTimeLog(string filename)
        {
            ITimeLog loadedTimeLog = fileManager.GetTimeLog(filename);
            if (loadedTimeLog != null)
            {
                timeManager.TimeLog = loadedTimeLog;
                timeLog = loadedTimeLog;
                activitiesSummary.TimeLog = loadedTimeLog;
                activitiesSummary.Update();
                return true;
            }
            else
                return false;
        }

        public bool LoadTimeLog(DateTime date)
        {
            string filename = GetTimeLogFileNameByDate(date);
            return LoadTimeLog(filename);
        }

        public string[] LatestActivities
        {
            get { return history.LatestActivities; }
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
            if (timeLogsFolder == "")
            {
                Log.Error("TimeLogsFolder is not specified");
                return false;
            }
            return SaveTimeLog();
        }

        public bool SaveTimeLog(string filename)
        {
            return fileManager.SaveTimeLog(timeLog, filename);
        }

        #endregion

        private string GetTimeLogFileNameByDate(DateTime date)
        {
            return TimeLogsFolder + @"\" + date.ToString("yyyy-MM-dd") + ".timelog";
        }
        private bool SaveTimeLog()
        {
            return SaveTimeLog(GetTimeLogFileNameByDate(timeLog.Date));
        }
    }
}