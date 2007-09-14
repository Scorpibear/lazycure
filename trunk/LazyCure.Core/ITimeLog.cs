using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    public interface ITimeLog
    {
        List<IActivity> Activities { get;}
        DataTable Data { get;}
    }
}
