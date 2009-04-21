using NUnit.Framework;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI.Backend
{
    [TestFixture]
    public class HotKeyTest
    {
        HotKey key;
        [Test]
        public void ParseCtrl()
        {
            key = HotKey.Parse("Ctrl+F11");
            Assert.IsTrue(key.Ctrl);
            Assert.IsFalse(key.Alt);
            Assert.IsFalse(key.Shift);
            Assert.IsFalse(key.Win);
        }
        [Test]
        public void ParseAllModifiers()
        {
            key = HotKey.Parse("Ctrl+Alt+Shift+Win+A");
            Assert.IsTrue(key.Ctrl);
            Assert.IsTrue(key.Alt);
            Assert.IsTrue(key.Shift);
            Assert.IsTrue(key.Win);
        }
        [Test]
        public void ParseKey()
        {
            key = HotKey.Parse("Shift+1");
            Assert.AreEqual(Keys.D1, key.Key);
        }
        [Test]
        public void BadKey()
        {
            key = HotKey.Parse("BadKey");
            Assert.AreEqual(Keys.F12, key.Key);
        }
        [Test]
        public void KeyCode()
        {
            key = HotKey.Parse("5");
            Assert.AreEqual((int)Keys.D5, key.Code);
        }
        [Test]
        public void ToStringSimple()
        {
            key = HotKey.Parse("F5");
            Assert.AreEqual("F5", key.ToString());
        }
        [Test]
        public void ToStringWithModifiers()
        {
            key = HotKey.Parse("Win+Ctrl+Alt+Shift+R");
            Assert.AreEqual("Win+Ctrl+Alt+Shift+R", key.ToString());
        }
        [Test]
        public void ToStringWithNumKey()
        {
            key = HotKey.Parse("Ctrl+1");
            Assert.AreEqual("Ctrl+1", key.ToString());
        }
    }
}
