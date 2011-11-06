using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Properties;
using LifeIdea.LazyCure.Shared.Constants;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;
using LifeIdea.LazyCure.UI;

namespace LifeIdea.LazyCure
{
    /// <summary>
    /// Represent LazyCure Program
    /// </summary>
    public class Program
    {
        private const string logFilename = "LazyCure.log";
        private static Notifier notifier = new Notifier();

        [STAThread]
        static void Main()
        {
            try
            {
                Log.Writer = GetLogWriter(logFilename);
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
                    notifier.DisplayError(ex, Constants.TimeLogError, Constants.TimeLogCouldNotBeLoaded);
                    return;
                }
                try
                {
                    Application.Run(new Main(driver,settings));
                }
                catch (Exception ex)
                {
                    notifier.DisplayError(ex, Constants.LazyCureError);
                }
                driver.Save();
                Log.Close();
            }
            catch(Exception ex)
            {
                notifier.DisplayError(ex, Constants.LazyCureError);
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
                notifier.DisplayError(ex, Constants.SettingsReadError);
            }
            return settings;
        }

        private static void SetApplicationProperties()
        {
            CultureInfo info = new CultureInfo(Application.CurrentCulture.LCID);
            Format.ApplyTimePatterns(info);
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
