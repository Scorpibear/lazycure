using System;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.Core.Time
{
    public interface IMidnightCorrector
    {
        void PerformMidnightCorrection(IActivity activity, ITimeLogsManager timeLogsManager);
    }
}
