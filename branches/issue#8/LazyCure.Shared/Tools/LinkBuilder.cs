using System.IO;
using System.Threading;

namespace LifeIdea.LazyCure.Shared.Tools
{
    public class LinkBuilder
    {
        /// <summary>
        /// Return link to how to use file, applying localization. Does not check if file actually exist
        /// </summary>
        /// <returns>link</returns>
        public static string GetHowToUseLink()
        {
            string link = Constants.Constants.HelpFileName;
            string shortLanguageCode = LocalizationFolder;
            if (shortLanguageCode != Constants.Constants.DefaultLocale)
                link = Path.Combine(shortLanguageCode, link);
            link = Path.Combine(Directory.GetCurrentDirectory(), link);
            return link;
        }

        /// <summary>
        /// Return folder where localization files are stored. Does not check either it's default locale or not. Default locale should not have a folder
        /// </summary>
        public static string LocalizationFolder
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            }
        }

    }
}
