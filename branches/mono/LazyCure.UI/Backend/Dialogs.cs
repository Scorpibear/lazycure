using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    static class Dialogs
    {
        private static OathAuthorize oath = null;
        private static OpenFileDialog open = null;
        private static SaveFileDialog save = null;
        private static ITimeLogView timeLog = null;
        private static ISummaryView summary = null;
        private static AboutBox about = null;
        private static Options options = null;
        private static TaskManager taskManager = null;

        internal static ILazyCureDriver LazyCureDriver = null;
        internal static IMainForm MainForm = null;
        internal static ISettings Settings;

        internal static OathAuthorize Oath
        {
            get
            {
                if (oath == null)
                {
                    oath = new OathAuthorize(LazyCureDriver);
                }
                return oath;
            }

        }

        internal static OpenFileDialog Open
        {
            get
            {
                if (open == null)
                {
                    open = new OpenFileDialog();
                    InitiateFileDialog(open);
                }
                return open;
            }
        }

        internal static SaveFileDialog Save
        {
            get
            {
                if (save == null)
                {
                    save = new SaveFileDialog();
                    InitiateFileDialog(save);
                }
                return save;
            }
        }

        internal static ITimeLogView TimeLog
        {
            get
            {
                if (timeLog == null)
                    timeLog = new TimeLogEditor(LazyCureDriver, MainForm);
                return timeLog;
            }
        }

        internal static ISummaryView Summary
        {
            get
            {
                if (summary == null)
                {
                    summary = new Summary(LazyCureDriver, MainForm);
                }
                return summary;
            }
        }

        internal static AboutBox About
        {
            get
            {
                if (about == null)
                    about = new AboutBox();
                return about;
            }
        }

        internal static TaskManager TaskManager
        {
            get
            {
                if (taskManager == null)
                    taskManager = new TaskManager(LazyCureDriver, MainForm);
                return taskManager;
            }
        }

        public static Options Options
        {
            get
            {
                if (options == null)
                    options = new Options(Settings);
                return options;
            }
        }

        internal static void CancelEditTimeLog()
        {
            if (timeLog != null)
                timeLog.CancelEdit();
        }

        private static void InitiateFileDialog(FileDialog fileDialog)
        {
            if (LazyCureDriver != null)
            {
                fileDialog.InitialDirectory = LazyCureDriver.TimeLogsFolder;
            }
            fileDialog.Filter = "Time Logs (*.timelog)|*.timelog|XML (*.xml)|*.xml|All Files (*.*)|*.*";
        }
    }
}