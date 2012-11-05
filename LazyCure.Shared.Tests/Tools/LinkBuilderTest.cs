using System.IO;
using NUnit.Framework;


namespace LifeIdea.LazyCure.Shared.Tools
{
    [TestFixture]
    public class LinkBuilderTest
    {
        [Test]
        public void LocalizationFolderNameHas2Letters()
        {
            Assert.AreEqual(2, LinkBuilder.LocalizationFolder.Length);
        }
    }
}
