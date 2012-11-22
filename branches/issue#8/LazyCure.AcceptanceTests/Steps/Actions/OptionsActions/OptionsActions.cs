using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyCure.AcceptanceTests.Steps.Actions
{
    public class OptionsActions: IOptionsActions
    {
        public IOptionsActions RemindAfter(TimeSpan timeSpan)
        {
            //TODO: implement setting remindAfter setting
            return this;
        }

        public void Applied()
        {
            //TODO: implement settings applience
        }
    }
}
