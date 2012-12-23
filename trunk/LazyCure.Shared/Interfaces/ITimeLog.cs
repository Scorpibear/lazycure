using System;
using System.Collections.Generic;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public interface ITimeLog: IDataProvider
    {
        List<IActivity> Activities { get;}
        void AddActivity(IActivity activity);
        DateTime Date { get; set;}
        string FileName { get; set;}
        void RenameActivities(string before, string after);
    }
}