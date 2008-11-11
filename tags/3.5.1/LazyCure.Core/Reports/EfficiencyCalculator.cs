using System;

namespace LifeIdea.LazyCure.Core.Reports
{
    public class EfficiencyCalculator: IEfficiencyCalculator
    {
        private IWorkingTimeManager workingTime;
        
        public EfficiencyCalculator(IWorkingTimeManager workingTime)
        {
            WorkingTime = workingTime;
        }

        public IWorkingTimeManager WorkingTime{
            get { return workingTime; }
            set { workingTime=value; }
        }

        public double Efficiency
        {
            get
            {
                double timeOnWork = workingTime.TimeOnWork.Ticks;
                if (timeOnWork != 0)
                    return workingTime.WorkingTasksTime.Ticks / timeOnWork;
                else
                    return 0.0;
            }
        }
    }
}
