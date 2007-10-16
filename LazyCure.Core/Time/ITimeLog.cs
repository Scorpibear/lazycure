using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    public interface ITimeLog
    {
        List<IActivity> Activities { get;}
        DataTable Data { get;}
    }
}