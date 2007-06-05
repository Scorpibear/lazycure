using System;
using NUnit.Framework;
using NMock2;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class AllTests
    {
        private Mockery mocks;
        static void Main(string[] args)
        {
        }
        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
        }
        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void TaskName()
        {
            Task task = new Task("task1");
            Assert.AreEqual("task1", task.Name);
        }
        [Test]
        public void LogException()
        {
            Exception ex = new Exception("message");
            
            Log.Writer = mocks.NewMock<IWriter>();
            using (mocks.Ordered)
            {
                Expect.Once.On(Log.Writer).Method("WriteLine").With(ex.Message);
                Expect.Once.On(Log.Writer).Method("WriteLine").With(ex.StackTrace);
            }
            Log.Exception(ex);
        }
        [Test]
        public void LogMessage()
        {
            Log.Writer = mocks.NewMock<IWriter>();
            Expect.Once.On(Log.Writer).Method("WriteLine").With("Error");
            Log.Error("Error");
        }
    }
}
