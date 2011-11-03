using NUnit.Framework;

namespace LifeIdea.LazyCure
{
    [TestFixture]
    public class ProgramTests
    {
        static void Main()
        {
            System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA);
        }

        [Test]
        public void ChangeLanguageWithUnsupportedCultureDoNotSwitchTheCulture()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr");
            Program.ChangeLanguage("unsupported");
            Assert.AreEqual("fr", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
    }
}
