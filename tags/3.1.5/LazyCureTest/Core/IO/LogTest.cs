using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core.IO
{
    [TestFixture]
    public class LogTest
    {
        private StringBuilder sb;
        [SetUp]
        public void SetUp()
        {
            sb = new StringBuilder();
            Log.Writer = new StringWriter(sb);
        }
        [Test]
        public void LogException()
        {
            Exception ex = new Exception("message");

            Log.Exception(ex);

            Assert.IsTrue(sb.ToString().Contains("message"));
        }
        [Test]
        public void LogMessage()
        {
            Log.Error("Error");

            Assert.IsTrue(sb.ToString().Contains("Error"));
        }
        [Test]
        public void EmptyWriter()
        {
            Log.Writer = null;

            try
            {
                Log.Error("Error");
            }
            catch (Exception)
            {
                Assert.Fail("Exception occured");
            }
        }
        [Test]
        public void WriteAfterClose()
        {
            Log.Close();
            try
            {
                Log.Error("Error");
            }
            catch (Exception) { Assert.Fail("Exception occured"); }
        }
    }
}