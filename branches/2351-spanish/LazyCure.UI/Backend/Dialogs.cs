using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.UI.Backend
{
    public static class Dialogs
    {
        private static OathAuthorize oath = null;
        private static OpenFileDialog open = null;
        private static SaveFileDialog save = null;
        private static ITimeLogView timeLog = null;
        private static ISummaryView summary = null;
        private static AboutBox about = null;
        private static Options options = null;
        private static TaskManager taskManager = null;
        private static ISpentOnDiffDaysView spentOnDiffDays = null;

        public static ILazyCureDriver LazyCureDriver = null;
        internal static IMainForm MainForm = null;
        internal static ISettings Settings;

        private static object[] AllDialogs
        {
            get
            {
                return new object[] { oath, open, save, timeLog, summary, about, options, taskManager, spentOnDiffDays };
            }
        }

        public static OathAuthorize Oath
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

        public static OpenFileDialog Open
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

        public static SaveFileDialog Save
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

        public static ITimeLogView TimeLog
        {
            get
            {
                if (timeLog == null)
                    timeLog = new TimeLogEditor(LazyCureDriver, MainForm);
                return timeLog;
            }
        }

        public static ISummaryView Summary
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

        public static AboutBox About
        {
            get
            {
                if (about == null)
                    about = new AboutBox();
                return about;
            }
        }

        public static TaskManager TaskManager
        {
            get
            {
                if (taskManager == null)
                    taskManager = new TaskManager(LazyCureDriver, MainForm);
                return taskManager;
            }
        }

        public static ISpentOnDiffDaysView SpentOnDiffDays
        {
            get
            {
                if (spentOnDiffDays == null)
                {
                    IHistoryDataProvider dataProvider = (LazyCureDriver != null) ? LazyCureDriver.HistoryDataProvider : null;
                    spentOnDiffDays = new SpentInDiffDaysView(dataProvider, MainForm);
                }
                return spentOnDiffDays;
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

        public static void CancelEditTimeLog()
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

        /// <summary>
        /// Reset all dialogs by closing it, so when next time each of the dialog will be executed, it will be reopened from scratch
        /// </summary>
        public static void Reset()
        {
            var forms = AllDialogs;
            foreach (Form form in forms)
            {
                if (form != null)
                    form.Close();
            }
            oath = null;
            open = null;
            save = null;
            timeLog = null;
            summary = null;
            about = null;
            options = null;
            taskManager = null;
            spentOnDiffDays = null;
        }
    }
}
