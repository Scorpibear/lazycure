using System;
using System.Drawing;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public struct LeftClickOnTray
    {
        public const bool ShowsMainWindow = false;
    }

    public interface ISettings: ILanguageSettingsProvider
    {
        int ActivitiesNumberInTray { get; set; }

        string HotKeyToActivate { get; set; }

        string HotKeyToSwitch { get; set; }

        bool LeftClickOnTray { get; set; }

        Point MainWindowLocation { get; set; }

        int MaxActivitiesInHistory { get; set; }

        TimeSpan ReminderTime { get; set; }

        void Save();

        bool SaveAfterDone { get; set; }

        bool SplitByComma { get; set; }

        bool SwitchOnLogOff { get; set; }

        bool SwitchTimeLogAtMidnight { get; set; }

        string TimeLogsFolder { get; set; }

        string TweetingActivity { get; set; }

        string TwitterAccessToken { get; set; }

        string TwitterAccessTokenSecret { get; set; }

        bool TwitterEnabled { get; set; }

        bool UseTweetingActivity { get; set; }
    }
}
