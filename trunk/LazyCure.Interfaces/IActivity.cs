using System;

namespace LifeIdea.LazyCure.Interfaces
{
    public interface IActivity
    {
        string Name { get;set;}
        DateTime StartTime { get;set;}
        TimeSpan Duration { get;set;}
    }
}
