using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface IActivity
    {
        string Name { get;}
        DateTime StartTime { get;set;}
        TimeSpan Duration { get;}
    }
}
