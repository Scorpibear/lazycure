using System;
using NUnit.Framework;

namespace LifeIdea.LazyCure.Core
{
    [TestFixture]
    public class UtilitiesTest
    {
        [Test]
        public void GetDateFromFileNameSimple()
        {
            Assert.AreEqual(DateTime.Parse("2103-07-15"), Utilities.GetDateFromFileName(@"c:\2103-07-15.timelog"));
        }
        [Test]
        public void GetDateFromFileNameWithoutDate()
        {
            Assert.AreEqual(DateTime.MinValue, Utilities.GetDateFromFileName(@"c:\test.timelog"));
        }
        [Test]
        public void GetDateFromNotValidFileName()
        {
            Assert.AreEqual(DateTime.MinValue, Utilities.GetDateFromFileName("!@#$%^&*()_+{}|:<>?"));
        }
        [Test]
        public void IsShortFileNameShort()
        {
            Assert.IsTrue(Utilities.IsFileNameShort("temp.xml"));
        }
        [Test]
        public void IsShortFileNameLong()
        {
            Assert.IsFalse(Utilities.IsFileNameShort(@"c:\temp.xml"));
        }
        [Test]
        public void IsShortFileNameEmpty()
        {
            Assert.IsFalse(Utilities.IsFileNameShort(String.Empty));
        }
        [Test]
        public void IsShortFileNameWithNull()
        {
            Assert.IsFalse(Utilities.IsFileNameShort(null));
        }
        [Test]
        public void TrueToString()
        {
            Assert.AreEqual("true",Utilities.BoolToString(true));
        }
        [Test]
        public void FalseToString()
        {
            Assert.AreEqual("false", Utilities.BoolToString(false));
        }
        [Test]
        public void StringToBoolCorrect()
        {
            Assert.AreEqual(false, Utilities.StringToBool("false"));
        }
        [Test]
        public void InvalidStringToBool()
        {
            Assert.AreEqual(true, Utilities.StringToBool("qwerty"));
        }
    }
}
