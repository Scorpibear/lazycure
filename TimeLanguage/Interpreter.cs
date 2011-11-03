using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.TimeLanguage
{
    public class Interpreter: IInterpreter
    {
        private ITimeSystem timeSystem;
        private RunningActivity currentActivity;
        private IActivity lastActivity = null;
        private FileManager fileManager = new FileManager();

        public IActivity CurrentActivity
        {
            get { return currentActivity; }
        }

        public string LastLine
        {
            get
            {
                if (lastActivity != null)
                    return String.Format("{0}\t{1}\t{2}\t{3}",
                        Format.Time(lastActivity.Start), lastActivity.Name, Format.Duration(lastActivity.Duration), Format.Time(lastActivity.End));
                else
                    return "";
            }
        }

        public IEnumerable<string> LastLines
        {
            get
            {
                return new string[] { LastLine };
            }
        }

        public Interpreter(ITimeSystem timeSystem) {
            Load();
            this.timeSystem = timeSystem;
            currentActivity = new RunningActivity("", timeSystem);
        }

        public Interpreter() : this(new NaturalTimeSystem()) { }

        public void ProcessLine(string line) {
            currentActivity.Name = line;
            currentActivity.Stop();
            lastActivity = currentActivity;
            currentActivity = RunningActivity.After(currentActivity, "");
            Save();
        }

        private void Load()
        {
            lastActivity = fileManager.Load();
        }

        private void Save()
        {
            fileManager.Save(lastActivity);
        }
    }
}
