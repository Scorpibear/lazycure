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
        private readonly TimeLog timeLog;
        private readonly ActivitiesSummary activitiesSummary;
        private readonly ITaskActivityLinker linker;
        private string timeLogsFolder;
        private readonly string historyFileName = "history.txt";
        private readonly History history;
        private readonly TaskCollection tasks;

        public static string FirstActivityName = "starting LazyCure";
        public bool SaveAfterDone=true;
        public string TimeLogsFolder { get { return timeLogsFolder; } set { timeLogsFolder = value; } }

        public Driver(ITimeSystem timeSystem)
        {
            timeLog = new TimeLog(timeSystem, FirstActivityName);
            tasks = new TaskCollection();
            linker = new TaskActivityLinker(tasks);
            activitiesSummary = new ActivitiesSummary(timeLog,linker);
            history = new History();
        }
        public Driver() : this(new RunTimeSystem()) { }
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

        public IActivity CurrentActivity { get { return timeLog.CurrentActivity; } }

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
            timeLog.FinishActivity(finishedActivity, nextActivity);
            history.AddActivity(finishedActivity);
            if(SaveAfterDone)
                Save();
        }

        public string TimeLogDate { get { return timeLog.Day.ToString("yyyy-MM-dd"); } }

        public bool LoadTimeLog(string filename)
        {
            if (File.Exists(filename))
            {
                timeLog.Load(filename);
                return true;
            }
            else
            {
                return false;
            }
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
            return SaveTimeLog(GetTimeLogFileNameByDate(timeLog.Day));
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
            timeLog.Save(stream);
            stream.Close();
            return true;
        }

        public bool LoadTimeLog(DateTime date)
        {
            history.Load(historyFileName);
            string filename = GetTimeLogFileNameByDate(date);
            return LoadTimeLog(filename);
        }

        #endregion

        private string GetTimeLogFileNameByDate(DateTime date)
        {
            return TimeLogsFolder + @"\" + date.ToString("yyyy-MM-dd") + ".timelog";
        }

    }
}