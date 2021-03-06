using System;
using System.Data;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Reports
{
    public interface IWorkingTimeManager
    {
        bool CalculateAutomatically { set; }
        
        TimeSpan PossibleWorkInterruption { get; set; }

        IWorkDefiner WorkDefiner { set; }

        ITimeLog TimeLog { set; }

        TimeSpan TimeOnWork { get; }

        TimeSpan WorkingTasksTime { get; }

        DataTable Intervals { get; }
    }
}
