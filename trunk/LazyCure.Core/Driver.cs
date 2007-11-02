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
        private TimeLog timeLog;
        private ITimeManager timeManager;
        private ActivitiesSummary activitiesSummary;
        private readonly ITaskActivityLinker linker;
        private string timeLogsFolder;
        private readonly string historyFileName = "history.txt";
        private readonly ActivitiesHistory history;
        private readonly TaskCollection tasks;

        public static string FirstActivityName = "starting LazyCure";
        public bool SaveAfterDone=true;
        public string TimeLogsFolder { get { return timeLogsFolder; } set { timeLogsFolder = value; } }

        public Driver(ITimeSystem timeSystem)
        {
            timeLog = new TimeLog(timeSystem.Now.Date);
            timeManager = new TimeManager(timeSystem);
            timeManager.TimeLog = timeLog;
            tasks = new TaskCollection();
            linker = new TaskActivityLinker(tasks);
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
        #region ILazyCureDriver Members

        public TimeSpan AllActivitiesTime { get { return activitiesSummary.AllActivitiesTime; } }

        public IActivity CurrentActivity { get { return timeManager.CurrentActivity; } }

        public object ActivitiesSummaryData { get { return activitiesSummary.Data; } }

        public void FillTaskNodes(TreeNodeCollection nodes)
        {
            nodes.AddRange(tasks.ToArray());
        }

        public void UpdateTaskNodeText(TreeNode node, string text)
        {
            if(tasks.Contains(node.Name))
                tasks.GetTask(node.Name).Text = text;
            else
                tasks.Add(new Task(text));
        }

        public object TimeLogData { get { return timeLog.Data; } }

        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            timeManager.FinishActivity(finishedActivity, nextActivity);
            history.AddActivity(finishedActivity);
            if(SaveAfterDone)
                Save();
        }

        public string TimeLogDate { get { return timeLog.Date.ToString("yyyy-MM-dd"); } }

        public bool LoadTimeLog(string filename)
        {
            if (File.Exists(filename))
            {
                StreamReader reader = File.OpenText(filename);
                timeLog = (TimeLog)TimeLogSerializer.Deserialize(reader);
                reader.Close();
                DateTime date = Utilities.GetDateFromFileName(filename);
                if (date!= DateTime.MinValue)
                    timeLog.Date = date;
                timeManager.TimeLog = timeLog;
                activitiesSummary = new ActivitiesSummary(timeLog,linker);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoadTimeLog(DateTime date)
        {
            history.Load(historyFileName);
            string filename = GetTimeLogFileNameByDate(date);
            return LoadTimeLog(filename);
        }

        public string[] LatestActivities
        {
            get { return history.LatestActivities; }
        }

        public bool Save()
        {
            if (history != null)
                history.Save("history.txt");
            if (timeLogsFolder == "")
            {
                Log.Error("TimeLogsFolder is not specified");
                return false;
            }
            return SaveTimeLog(GetTimeLogFileNameByDate(timeLog.Date));
        }

        public bool SaveTimeLog(string filename)
        {
            StreamWriter stream = null;
            try
            {
                FileInfo fileInfo = new FileInfo(filename);
                fileInfo.Directory.Create();
                stream = fileInfo.CreateText();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
            TimeLogSerializer.Serialize(timeLog, stream);
            stream.Close();
            return true;
        }

        #endregion

        private string GetTimeLogFileNameByDate(DateTime date)
        {
            return TimeLogsFolder + @"\" + date.ToString("yyyy-MM-dd") + ".timelog";
        }

    }
}