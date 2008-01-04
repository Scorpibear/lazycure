using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    public interface ITimeLog
    {
        List<IActivity> Activities { get;}
        void AddActivity(IActivity activity);
        DataTable Data { get;}
        DateTime Date { get; set;}
    }
}