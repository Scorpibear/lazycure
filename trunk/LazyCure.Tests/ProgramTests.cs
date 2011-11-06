using System.Globalization;
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
        [Test]
        public void ChangeLanguage()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
            Program.ChangeLanguage("ru");
            Assert.AreEqual("ru", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
        [Test]
        public void ChangeLanguageWithUnsupportedCultureDoNotSwitchTheCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
            Program.ChangeLanguage("unsupported");
            Assert.AreEqual("fr", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
        
    }
}
