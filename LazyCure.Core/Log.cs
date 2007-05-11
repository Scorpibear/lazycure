using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Core
{
    public static class Log
    {
        public static IWriter StreamWriter;
        public static void Exception(Exception ex)
        {
            StreamWriter.WriteLine(ex.Message);
            StreamWriter.WriteLine(ex.Source);
        }
    }
}
