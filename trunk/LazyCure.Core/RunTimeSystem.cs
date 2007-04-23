using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Core
{
    class RunTimeSystem: ITimeSystem
    {
        #region ITimeSystem Members

        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        #endregion
    }
}
