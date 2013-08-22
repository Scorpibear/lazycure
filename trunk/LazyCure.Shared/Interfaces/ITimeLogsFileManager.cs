using System;
using System.Collections.Generic;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public interface ITimeLogsFileManager
    {
        List<DateTime> AllTimeLogDates { get; }

        ITimeLog GetTimeLog(DateTime day);

        bool SaveTimeLog(ITimeLog timeLog);
    }
}
