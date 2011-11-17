using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LazyCure.AcceptanceTests.Steps.Actions;

namespace LazyCure.AcceptanceTests.Steps
{
    public class Given
    {
        public static IOptionsActions Options { get { return new OptionsActions(); } }
    }
}
