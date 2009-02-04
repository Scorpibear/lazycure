using System.Collections.Generic;

namespace LifeIdea.TimeLanguage
{
    public interface IInterpreter
    {
        string LastLine { get;}
        IEnumerable<string> LastLines { get;}
        void ProcessLine(string line);
    }
}
