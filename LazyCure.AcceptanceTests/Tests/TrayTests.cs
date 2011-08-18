using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LazyCure.AcceptanceTests.Data;
using LazyCure.AcceptanceTests.Steps;

namespace LazyCure.AcceptanceTests.Tests
{
    [TestClass]
    public class TrayTests
    {
        [TestMethod]
        public void ICanSeeBlinkingIconAfterRemindTimeFinished()
        {
            var interval = TestData.OneSecond;
            Given.Options.RemindAfter(interval).Applied();
            When.Passed(interval);
            Then.Icon.IsBlinking();
        }
    }
}
