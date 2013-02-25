using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Reports
{
    public interface IActivitiesSummary
    {
        TimeSpan AllActivitiesTime { get; }
        DataTable Data { get; }
        ITaskActivityLinker Linker { set; }
        ITimeLog TimeLog { set; }
        List<ITimeLog> TimeLogs { set; }
        void Update();
    }
}
