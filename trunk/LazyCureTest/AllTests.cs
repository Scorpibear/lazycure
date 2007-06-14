using System;
using NUnit.Framework;
using NMock2;
using System.Xml;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class AllTests: Mockery
    {
        static void Main(string[] args)
        {
        }
        [TearDown]
        public void TearDown()
        {
            this.VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void TaskName()
        {
            Task task = new Task("task1");
            
            Assert.AreEqual("task1", task.Name);
        }
    }
}
