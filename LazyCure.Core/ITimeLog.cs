using System.Collections.Generic;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    public interface ITimeLog
    {
        List<IActivity> Activities{get;}
    }
}
