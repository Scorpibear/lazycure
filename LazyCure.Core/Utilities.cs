using System;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Perform different static operations
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
    }
}
