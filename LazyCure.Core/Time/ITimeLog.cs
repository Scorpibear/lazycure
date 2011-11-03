using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Reports;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
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