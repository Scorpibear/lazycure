using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace LifeIdea.LazyCure.Shared.Tools
{
    /// <summary>
    /// Format values
    /// </summary>
    public class Format
    {
        public const string LongTimePattern = "H:mm:ss";
        public const string ShortTimePattern = "H:mm";

        /// <summary>
        /// Apply time patterns to provided culture info
        /// </summary>
        /// <param name="info">CultureInfo object</param>
        /// <returns>updated CultureInfo</returns>
        public static CultureInfo ApplyTimePatterns(CultureInfo info)
        {
            info.DateTimeFormat.LongTimePattern = Format.LongTimePattern;
            info.DateTimeFormat.ShortTimePattern = Format.ShortTimePattern;
            return info;
        }

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

        public static string Percent(double value)
        {
            int percent = (value.Equals(double.NaN)) ? 0 : (int) Math.Round(value*100);
            return String.Format("{0}%",percent);
        }

        public static string ShortDuration(TimeSpan timeSpan)
        {
            TimeSpan roundedTime;
            if(timeSpan.Seconds>=30)
                roundedTime = TimeSpan.FromMinutes(timeSpan.TotalMinutes + 1);
            else
                roundedTime = TimeSpan.FromMinutes(timeSpan.TotalMinutes);
            return Regex.Match(roundedTime.ToString(), "[1-9]*[0-9]:[0-9][0-9]").Groups[0].Value;
        }
        public static string Time(DateTime dateTime)
        {
            return dateTime.ToString(LongTimePattern);
        }
        public static TimeSpan Duration(object obj)
        {
            TimeSpan result = TimeSpan.Zero;
            TimeSpan.TryParse(obj.ToString(), out result);
            return result;
        }
        public static DateTime Time(object obj)
        {
            return DateTime.Parse(obj.ToString());
        }

        public static string MaskedText(TimeSpan timeSpan)
        {
            return timeSpan.Hours.ToString()+timeSpan.Minutes.ToString("00");
        }
    }
}
