using System;

namespace LifeIdea.LazyCure.Interfaces
{
    public class Format
    {
        /// <summary>
        /// Returns string representing timeSpan in format [dd:]hh:mm:ss
        /// </summary>
        /// <param name="timeSpan">TimeSpan object</param>
        /// <returns>string representation of TimeSpan</returns>
        public static string Duration(TimeSpan timeSpan)
        {
            string str = timeSpan.ToString();
            if (str.StartsWith("0"))
                str=str.Remove(0,1);
            if(str.Contains("."))
                return str.Substring(0, str.LastIndexOf('.'));
            else
                return str;
        }
        public static string Time(DateTime dateTime)
        {
            return dateTime.ToString("H:mm:ss");
        }
        public static TimeSpan Duration(object obj)
        {
            return TimeSpan.Parse(obj.ToString());
        }
        public static DateTime Time(object obj)
        {
            return DateTime.Parse(obj.ToString());
        }
    }
}
