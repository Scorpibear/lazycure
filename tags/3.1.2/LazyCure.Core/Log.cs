using System;
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
            public void Close()
            {
                textWriter.Close();
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

        public static void Close()
        {
            Writer.Close();
        }
    }
    
}
