using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    public class TimeLog
    {
        private Activity currentActivity, previousActivity = null;
        public IActivity CurrentActivity { get { return currentActivity; } }
        public IActivity PreviousActivity { get { return previousActivity; } }
        public TimeLog(ITimeSystem timeSystem,string firstActivityName)
        {
            currentActivity = new Activity(firstActivityName, timeSystem);
        }
        public IActivity SwitchTo(string nextActivity)
        {
            currentActivity.Stop();
            previousActivity = currentActivity;
            currentActivity = Activity.After(previousActivity, nextActivity);
            return currentActivity;
        }
        public void FinishActivity(string finishedActivity, string nextActivity)
        {
            SwitchTo(nextActivity);
            previousActivity.Name = finishedActivity;
        }
        public void Save(TextWriter writer)
        {
            writer.WriteLine("<?xml version=\"1.0\" standalone=\"yes\"?>");
            writer.WriteLine("<LazyCureData>");
            writer.WriteLine(currentActivity.ToString());
            writer.WriteLine("</LazyCureData>");
        }
    }
}
