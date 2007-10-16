using System;

namespace LifeIdea.LazyCure.Core.Time
{
    class RunTimeSystem: ITimeSystem
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}