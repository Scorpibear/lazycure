using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Interfaces;
using LifeIdea.LazyCure.Properties;
using LifeIdea.LazyCure.UI;


namespace LifeIdea.LazyCure
{
    /// <summary>
    /// Represent LazyCure Program
    /// </summary>
    public class Program
    {
        private const string askToFix = "\r\n\r\nPlease, fix the error and start LazyCure again. Sorry for inconveniences.";
        [STAThread]
        static void Main()
        {
            try
            {
                Log.Writer = GetLogWriter("LazyCure.log");
                SetApplicationProperties();
                ISettings settings = GetSettings();
                ChangeLanguage(settings.Language);
                Driver driver = new Driver();
                driver.ApplySettings(settings);
                try
                {
                    driver.Load();
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

        public static void ChangeLanguage(string lang)
        {
            if ((lang != null) && (Thread.CurrentThread.CurrentUICulture.Name != lang))
            {
                CultureInfo cultureInfo = null;
                try
                {
                    cultureInfo = new CultureInfo(lang);
                }
                catch { }
                if (cultureInfo != null)
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
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
            info.DateTimeFormat.ShortTimePattern = "H:mm";
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
