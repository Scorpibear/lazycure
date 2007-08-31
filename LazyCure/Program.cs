using System;
using System.IO;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core;
using LifeIdea.LazyCure.UI;
using System.Globalization;



namespace LifeIdea.LazyCure
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            const string askToFix = "\r\n\r\nPlease, fix the error and start LazyCure again. Sorry for inconveniences.";
            try
            {
                string logPath = Application.StartupPath + @"\LazyCure.log";
                System.IO.TextWriter logWriter =
                    new System.IO.StreamWriter(
                        System.IO.File.Open(logPath, System.IO.FileMode.Append, System.IO.FileAccess.Write,
                                            System.IO.FileShare.Write));
                Log.TextWriter = logWriter;
                CultureInfo info = new CultureInfo(Application.CurrentCulture.LCID);
                info.DateTimeFormat.LongTimePattern = "H:mm:ss";
                Application.CurrentCulture = info;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Driver driver = new Driver();
                string historyFilename = Application.StartupPath + @"\history.txt";
                driver.LoadHistory(historyFilename);
                try
                {
                    LazyCureSettings settings = new LazyCureSettings();
                    driver.TimeLogsFolder = settings.TimeLogsFolder;
                    driver.SaveAfterDone = settings.SaveAfterDone;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+askToFix, "Error while reading application settings", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    Log.Exception(ex);
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
                    Application.Run(new Main(driver));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LazyCure error");
                    Log.Exception(ex);
                }
                driver.SaveHistory(historyFilename);
                driver.SaveTimeLog();
                logWriter.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+askToFix, "LazyCure error");
            }
        }
    }
}
