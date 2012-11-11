using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Constants;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure
{
    public class Notifier
    {
        public void DisplayError(Exception ex, string shortDescription)
        {
            string fullDescription = ExceptionToString(ex);
            DisplayErrorAndLogException(ex, shortDescription, fullDescription);
        }

        public void DisplayError(Exception ex, string shortDescription, string longDescription)
        {
            string fullDescrpition = string.Format("{0}:\r\n{1}", longDescription, ExceptionToString(ex));
            DisplayErrorAndLogException(ex, shortDescription, fullDescrpition);
        }

        public static string ExceptionToString(Exception ex)
        {
            return string.Format("{0}\r\n\r\n{1}", ex.Message, Constants.AskToFixAndExcuse);
        }

        private static void DisplayErrorAndLogException(Exception ex, string shortDescription, string fullDescription)
        {
            MessageBox.Show(fullDescription, shortDescription, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log.Exception(ex);
        }
    }
}
