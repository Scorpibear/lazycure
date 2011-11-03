using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;
using NUnit.Framework;
using NMock2;

namespace LifeIdea.LazyCure.UI.Backend
{
    [TestFixture]
    public class HotKeyManagerTest:Mockery
    {
        HotKeyManager manager;
        IWin32Window window;
        IHotKeyCodeProvider hotKey;
        [SetUp]
        public void SetUp()
        {
            manager = new HotKeyManager();
            Log.Writer = System.Console.Out;
            window = NewMock<IWin32Window>();
            Stub.On(window).GetProperty("Handle").Will(Return.Value(IntPtr.Zero));
            hotKey = NewMock<IHotKeyCodeProvider>();
        }
        [Test]
        public void Register()
        {
            manager.Register(null, 1, HotKey.Parse("Ctrl+F12"));
        }
        [Test]
        public void CodeIsGetFromHotKey()
        {
            Expect.AtLeastOnce.On(hotKey).GetProperty("Code").Will(Return.Value(0));
            Stub.On(hotKey).GetProperty("ModifiersCode").Will(Return.Value(0));
            manager.Register(window, 1, hotKey);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void ModifiersCodeIsGetFromHotKey()
        {
            Stub.On(hotKey).GetProperty("Code").Will(Return.Value(0));
            Expect.AtLeastOnce.On(hotKey).GetProperty("ModifiersCode").Will(Return.Value(0));
            manager.Register(window, 1, hotKey);
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void RegisterHotKeysLogsException()
        {
            manager.Register(null, 0, null);
            Assert.AreEqual("Could not register hot key for null window",Log.LastError);
        }
        [Test]
        public void RegisterWithNullKey()
        {
            manager.Register(window, 1, null);
            Assert.AreEqual("Could not register null hot key", Log.LastError);
        }
    }
}
