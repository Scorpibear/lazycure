using System;

namespace LifeIdea.LazyCure.Core.Time
{
    /// <summary>
    /// Natural time system
    /// </summary>
    public class NaturalTimeSystem: ITimeSystem
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