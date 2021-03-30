using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Shared.Interfaces;
using System.Windows.Forms;
using System;

namespace LifeIdea.LazyCure.UI.Backend
{
    [TestFixture]
    public class DialogsTests: Mockery
    {
        [Test]
        public void SpentOnDiffDaysReturnsAnObject()
        {
            Assert.IsNotNull(Dialogs.SpentOnDiffDays);
        }
        [Test]
        public void SpentOnDiffDaysUsesHistoryDataProvider()
        {
            Dialogs.Reset();
            Dialogs.LazyCureDriver = NewMock<ILazyCureDriver>();
            Expect.Once.On(Dialogs.LazyCureDriver).GetProperty("HistoryDataProvider").Will(Return.Value(null));
            object form = Dialogs.SpentOnDiffDays;
            VerifyAllExpectationsHaveBeenMet();
        }
        [Test]
        public void ResetDialogsClosesAllOfTheDialogs()
        {
            Form form = (Form)Dialogs.SpentOnDiffDays;
            Assert.False(form.IsDisposed);
            Dialogs.Reset();
            Assert.True(form.IsDisposed);
        }
        [Test]
        public void ResetDialogDisposeTimeLogEditor()
        {
            Form form = (Form)Dialogs.TimeLog;
            Assert.False(form.IsDisposed);
            Dialogs.Reset();
            Assert.True(form.IsDisposed);
        }
    }
}
