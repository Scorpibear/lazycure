using System;
using NMock2;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.IO
{
    [TestFixture]
    public class LogTest: Mockery
    {
        [TearDown]
        public void TearDown()
        {
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void LogException()
        {
            Exception ex = new Exception("message");

            Log.Writer = NewMock<IWriter>();
            using (Ordered)
            {
                Expect.Once.On(Log.Writer).Method("WriteLine").With(ex.Message);
                Expect.Once.On(Log.Writer).Method("WriteLine").With(ex.StackTrace);
            }
            Log.Exception(ex);
        }
        [Test]
        public void LogMessage()
        {
            Log.Writer = NewMock<IWriter>();
            Expect.Once.On(Log.Writer).Method("WriteLine").With("Error");
            Log.Error("Error");
        }
    }
}