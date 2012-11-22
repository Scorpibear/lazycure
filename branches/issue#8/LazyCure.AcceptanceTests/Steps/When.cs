using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LazyCure.AcceptanceTests.Steps
{
    public class When
    {
        public static void Passed(TimeSpan timeSpan) { Thread.Sleep(timeSpan); }
    }
}
