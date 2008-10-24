using System;
using System.Drawing;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface ISettings
    {
        int MaxActivitiesInHistory{ get; set;}

        Point MainWindowLocation { get; set;}

        TimeSpan ReminderTime { get; set;}

        void Save();

        bool SaveAfterDone{ get; set;}
        
        string TimeLogsFolder{ get; set;}
    }
}
