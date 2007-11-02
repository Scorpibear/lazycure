using System;

namespace LifeIdea.LazyCure.Core.Time
{
    /// <summary>
    /// Natural time system
    /// </summary>
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