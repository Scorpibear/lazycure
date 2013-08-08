using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    [TestFixture]
    public class SpentOnDiffDaysViewTests: Mockery
    {
        SpentInDiffDaysView form;
        IHistoryDataProvider provider;

        [SetUp]
        public void SetUp()
        {
            provider = NewMock<IHistoryDataProvider>();
            Stub.On(provider).GetProperty("Data").Will(Return.Value(null));
            form = new SpentInDiffDaysView(provider, null);
        }

        [Test]
        public void LoadActivitiesSetItemsInActivitiesComboBox()
        {
            Stub.On(provider).GetProperty("HistoryActivities").Will(Return.Value(new string[] { "test-it" }));
            
            form.LoadActivitiesOrTasksList();

            Assert.AreEqual(1, form.ComboBoxItems.Count);
            Assert.AreEqual("test-it", form.ComboBoxItems[0]);
        }
        [Test]
        public void LastActivityIsSelectedAfterLoadActivities()
        {
            Stub.On(provider).GetProperty("HistoryActivities").Will(Return.Value(new string[] { "latest", "first" }));
            
            form.LoadActivitiesOrTasksList();

            Assert.AreEqual("latest", form.SelectedValue);
        }
    }
}
