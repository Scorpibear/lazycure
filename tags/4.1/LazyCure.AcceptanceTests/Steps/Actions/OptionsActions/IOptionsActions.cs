using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyCure.AcceptanceTests.Steps.Actions
{
    public interface IOptionsActions
    {
        IOptionsActions RemindAfter(TimeSpan timeSpan);
        void Applied();
    }
}
