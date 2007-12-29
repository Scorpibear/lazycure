using System;
using System.IO;

namespace LifeIdea.LazyCure.Core.IO
{
    /// <summary>
    /// Log errors and exceptions to specified writer (file or string)
    /// </summary>
    public static class Log
    {
        public static TextWriter Writer = null;

        public static void Exception(Exception ex)
        {
            Error(ex.Message);
            Error(ex.StackTrace);
        }

        public static void Error(string text)
        {
            try{
                Writer.WriteLine(text);
            }catch(Exception)
            {
            }
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