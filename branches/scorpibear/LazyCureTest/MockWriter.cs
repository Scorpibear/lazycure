using System;
using System.Collections.Generic;
using System.Text;

namespace LifeIdea.LazyCure.Core
{
    internal class MockWriter : System.IO.TextWriter
    {
        public string Content = "";
        public override void WriteLine(string s)
        {
            Content += s;
        }
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
