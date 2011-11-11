using System;
using System.Globalization;
using System.IO;
using System.Threading;
using NUnit.Framework;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.Localization
{
    [TestFixture]
    public class LanguageSwitcherTest
    {
        LanguageSwitcher languageSwitcher;
        TextWriter previousWriter;
        [SetUp]
        public void SetUp()
        {
            previousWriter = Log.Writer;
            Log.Writer = Console.Error;
            languageSwitcher = new LanguageSwitcher();
        }
        [TearDown]
        public void TearDown()
        {
            Log.Writer = previousWriter;
        }
        [Test]
        public void ChangeLanguageWithUnsupportedCultureDoNotSwitchTheCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
            languageSwitcher.ChangeLanguage("unsupported");
            Assert.AreEqual("fr", Thread.CurrentThread.CurrentCulture.Name);
        }
        [Test]
        public void LastApplied()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
            languageSwitcher.ChangeLanguage("ru");
            Assert.AreEqual("ru", languageSwitcher.LastApplied);
        }
    }
}
