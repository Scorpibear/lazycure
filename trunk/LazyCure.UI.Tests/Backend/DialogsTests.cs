using NUnit.Framework;

namespace LifeIdea.LazyCure.UI.Backend
{
    [TestFixture]
    public class DialogsTests
    {
        [Test]
        public void SpentOnDiffDaysReturnsAnObject()
        {
            Assert.IsNotNull(Dialogs.SpentOnDiffDays);
        }
    }
}
