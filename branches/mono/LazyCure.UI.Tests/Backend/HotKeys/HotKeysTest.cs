using NUnit.Framework;

namespace LifeIdea.LazyCure.UI.Backend.HotKeys
{
    [TestFixture]
    public class HotKeysEditorTest
    {
        [Test]
        public void AllClassesOfKeysAreInList()
        {
            string[] allKeys = HotKeys.GetAllNames();
            Assert.Contains("F12", allKeys);
            Assert.Contains("Z", allKeys);
            Assert.Contains("9", allKeys);
        }
    }
}
