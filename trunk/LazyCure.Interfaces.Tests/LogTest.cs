using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Shared
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
        [Test]
        public void LastError()
        {
            Log.Error("the first");
            Log.Error("the last");
            Assert.AreEqual("the last", Log.LastError);
        }
    }
}