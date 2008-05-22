using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Reports;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    public interface ITimeLog: IDataProvider
    {
        List<IActivity> Activities { get;}
        void AddActivity(IActivity activity);
        DateTime Date { get; set;}
    }
}