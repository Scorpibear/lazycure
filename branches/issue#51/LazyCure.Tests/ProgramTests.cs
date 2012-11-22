using System.Globalization;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace LifeIdea.LazyCure
{
    [TestFixture]
    public class ProgramTests
    {
        static void Main()
        {
            Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA);
        }
        TextWriter writer;
        [Test]
        public void GetLogWriterCreatesWriter()
        {
            writer = Program.GetLogWriter("test.txt");
            Assert.IsNotNull(writer);
        }
        [TearDown]
        public void TearDown()
        {
            if (writer != null)
                writer.Close();
        }
    }
}
