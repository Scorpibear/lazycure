using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LifeIdea.TimeLanguage
{
    public class TimeLanguageConsole
    {
        public static IInterpreter Interpreter = new Interpreter();
        public static TextWriter Writer = Console.Out;

        public static void Main(params string[] args)
        {
            if (args.Length > 0)
            {
                string activity = string.Join(" ", args);
                Interpreter.ProcessLine(activity);
            }
            foreach(string line in Interpreter.LastLines)
                Writer.WriteLine(line);
        }
    }
}
