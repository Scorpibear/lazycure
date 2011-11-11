using System;
using System.Globalization;
using System.Threading;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.Localization
{
    /// <summary>
    /// Switch current language
    /// </summary>
    public class LanguageSwitcher: ILanguageSwitcher
    {
        string lastApplied;

        public LanguageSwitcher()
        {
        }

        public LanguageSwitcher(ILanguageSettingsProvider languageSettingsProvider)
        {
            this.ApplyLanguageSettings(languageSettingsProvider);
        }

        /// <summary>
        /// Change language of the current thread
        /// </summary>
        /// <param name="languageCode">language code, such as 'en', 'ru', etc.</param>
        public void ChangeLanguage(string languageCode)
        {
            if ((languageCode != null) && (Thread.CurrentThread.CurrentUICulture.Name != languageCode))
            {
                CultureInfo cultureInfo = null;
                try
                {
                    cultureInfo = new CultureInfo(languageCode);
                }
                catch(Exception ex)
                {
                    Log.Exception(ex);
                }
                if (cultureInfo != null)
                {
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                    lastApplied = cultureInfo.TwoLetterISOLanguageName;
                }
            }
        }

        public void ApplyLanguageSettings(ILanguageSettingsProvider languageSettingsProvider)
        {
            if (languageSettingsProvider != null)
                ChangeLanguage(languageSettingsProvider.Language);
        }

        /// <summary>
        /// Returns language code of the last applied settings
        /// </summary>
        public string LastApplied { get { return lastApplied; } }
    }
}
