using System;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Perform different static utility operations
    /// </summary>
    public static class Utilities
    {
        public static DateTime GetDateFromFileName(string filename)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filename);
                string dateString = fileInfo.Name.Split('.')[0];
                return DateTime.Parse(dateString);
            } catch(Exception){
                return DateTime.MinValue;}
        }

        public static bool IsFileNameShort(string fileName)
        {
            if (fileName.Contains("/") || fileName.Contains("\\") || (fileName == string.Empty))
                return false;
            else
                return true;
        }

        public static string BoolToString(bool value)
        {
            return value.ToString().ToLower();
        }

        /// <summary>
        /// Convert string to boolean value. If string does not represent bool, returns true.
        /// </summary>
        /// <param name="value">string representation of boolean</param>
        /// <returns>boolean value</returns>
        public static bool StringToBool(string value)
        {
            bool result;
            if (bool.TryParse(value,out result))
                return result;
            else
                return true;
        }
    }
}
