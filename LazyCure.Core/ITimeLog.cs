using System;
using System.Collections.Generic;
using System.Text;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    public interface ITimeLog
    {
        List<IActivity> Activities{get;}
    }
}
