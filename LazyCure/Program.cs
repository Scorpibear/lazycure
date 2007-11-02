using System;
using System.IO;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Interfaces;
using LifeIdea.LazyCure.Properties;
using LifeIdea.LazyCure.UI;
using System.Globalization;



namespace LifeIdea.LazyCure
{
    public class Program
    {
        private const string askToFix = "\r\n\r\nPlease, fix the error and start LazyCure again. Sorry for inconveniences.";
        [STAThread]
        static void Main()
        {
            try
            {
                Log.TextWriter = GetLogWriter("LazyCure.log");

                SetApplicationProperties();

                ISettings settings = GetSettings();

                Driver driver = new Driver();
                if (settings != null)
                {
                    driver.TimeLogsFolder = settings.TimeLogsFolder;
                    driver.SaveAfterDone = settings.SaveAfterDone;
                    ActivitiesHistory.MaxActivities = settings.MaxActivitiesInHistory;
                }
                try
                {
                    driver.LoadTimeLog(DateTime.Now);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(
                        "TimeLog could not be loaded, because of the following error:\r\n" + ex.Message + askToFix,
                        "TimeLog error");
                    Log.Exception(ex);
                    return;
                }
                try
                {
                    Application.Run(new Main(driver,settings));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LazyCure error");
                    Log.Exception(ex);
                }
                driver.Save();
                Log.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+askToFix, "LazyCure error");
            }
        }

        private static ISettings GetSettings()
        {
            ISettings settings = null;
            try
            {
                settings = Settings.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+askToFix, "Error while reading application settings", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                Log.Exception(ex);
            }
            return settings;
        }

        private static void SetApplicationProperties()
        {
            CultureInfo info = new CultureInfo(Application.CurrentCulture.LCID);
            info.DateTimeFormat.LongTimePattern = "H:mm:ss";
            Application.CurrentCulture = info;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private static TextWriter GetLogWriter(string logPath)
        {
            TextWriter logWriter =
                new StreamWriter(
                    File.Open(logPath, FileMode.Append, FileAccess.Write, FileShare.Write));
            return logWriter;
        }
    }
}
