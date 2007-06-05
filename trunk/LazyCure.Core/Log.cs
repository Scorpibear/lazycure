using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LifeIdea.LazyCure.Core
{
    public static class Log
    {
        class LazyCureTextWriter : IWriter
        {
            private TextWriter textWriter;
            public LazyCureTextWriter(TextWriter textWriter)
            {
                this.textWriter = textWriter;
            }
            public void WriteLine(string str)
            {
                textWriter.WriteLine(str);
            }
        }
        public static IWriter Writer;
        public static TextWriter TextWriter { set { Writer = new LazyCureTextWriter(value); } }
        public static void Exception(Exception ex)
        {
            Writer.WriteLine(ex.Message);
            Writer.WriteLine(ex.StackTrace);
        }
        public static void Error(string text)
        {
            Writer.WriteLine(text);
        }
    }
    
}
