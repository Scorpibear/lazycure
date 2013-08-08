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
                double efficiency = 0.0;
                if (workingTime != null)
                {
                    double timeOnWork = workingTime.TimeOnWork.Ticks;
                    if (timeOnWork != 0)
                        efficiency = workingTime.WorkingTasksTime.Ticks / timeOnWork;
                }
                return efficiency;
            }
        }
    }
}
