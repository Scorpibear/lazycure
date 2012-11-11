using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LazyCure.AcceptanceTests.Steps.Verifications;

namespace LazyCure.AcceptanceTests.Steps
{
    public class Then
    {
        public static IIconVerifications Icon { get { return new IconVerifications(); } }
    }
}
