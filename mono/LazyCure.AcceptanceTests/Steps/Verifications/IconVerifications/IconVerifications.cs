using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LazyCure.AcceptanceTests.Steps.Verifications
{
    public class IconVerifications: IIconVerifications
    {
        public IIconVerifications IsBlinking()
        {
            var expectedBlinking = true;
            //TODO: add method for knowing either Icon is blinking or not
            var actualBlinking = false;
            Assert.AreEqual(expectedBlinking, actualBlinking, "icon is blinking");
            return this;
        }
    }
}
