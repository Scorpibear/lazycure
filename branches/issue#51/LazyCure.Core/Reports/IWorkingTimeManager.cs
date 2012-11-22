using System;
using System.Data;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time.TimeLogs;

namespace LifeIdea.LazyCure.Core.Reports
{
    public interface IWorkingTimeManager
    {
        bool CalculateAutomatically { set; }
        
        TimeSpan PossibleWorkInterruption { get; set; }

        ITaskCollection TaskCollection { set; }

        ITimeLog TimeLog { set; }

        TimeSpan TimeOnWork { get; }

        TimeSpan WorkingTasksTime { get; }

        DataTable Intervals { get; }
    }
}
