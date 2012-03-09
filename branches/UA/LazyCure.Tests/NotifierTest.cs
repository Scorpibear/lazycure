using System;
using LifeIdea.LazyCure.Shared.Constants;
using NUnit.Framework;

namespace LifeIdea.LazyCure
{
    [TestFixture]
    public class NotifierTest
    {
        [Test]
        public void ExceptionToStringIncludesMessage()
        {
            Notifier notifier = new Notifier();
            Exception ex = new Exception("my message");
            string result = Notifier.ExceptionToString(ex);
            Assert.IsTrue(result.Contains("my message"));
        }
        [Test]
        public void ExceptionToStringExcuses()
        {
            Notifier notifier = new Notifier();
            Exception ex = new Exception();
            string result = Notifier.ExceptionToString(ex);
            Assert.IsTrue(result.Contains(Constants.AskToFixAndExcuse));
        }
    }
}
