using System;
using System.Drawing;

namespace LifeIdea.LazyCure.Interfaces
{
    public struct LeftClickOnTray
    {
        public const bool ShowsMainWindow = false;
    }

    public interface ISettings
    {
        int MaxActivitiesInHistory { get; set;}

        int ActivitiesNumberInTray { get; set;}

        bool LeftClickOnTray { get; set;}

        Point MainWindowLocation { get; set;}

        TimeSpan ReminderTime { get; set;}

        void Save();

        bool SaveAfterDone { get; set;}

        bool SwitchOnLogOff { get; set;}

        string TimeLogsFolder { get; set;}

        bool TwitterEnabled { get; set;}

        string TwitterPassword { get; set;}

        string TwitterUsername { get; set;}
    }
}
