using System;
using System.IO;

namespace LifeIdea.LazyCure.Shared.Tools
{
    /// <summary>
    /// Log errors and exceptions to specified writer (file or string)
    /// </summary>
    public static class Log
    {
        public static TextWriter Writer = null;

        public static string LastError;

        public static void Exception(Exception ex)
        {
            Error(ex.Message+"\r\n"+ex.StackTrace);
        }

        public static void Error(string text)
        {
            LastError = text;
            string fullText = AppendTimeStamp(text);
            try{
                if(Writer!=null)
                    Writer.WriteLine(fullText);
            }catch(Exception)
            {
            }
        }

        public static string AppendTimeStamp(string str)
        {
            return String.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), str);
        }

        public static void Error(string format, params object[] args)
        {
            Error(String.Format(format, args));
        }

        public static void Close()
        {
            if (Writer != null)
            {
                Writer.Close();
            }
        }
    }
}