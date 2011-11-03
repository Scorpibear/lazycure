using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;
using NMock2;
using LifeIdea.LazyCure.Shared.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    [TestFixture]
    public class MainBaseTests:Mockery
    {
        MainBase main;
        IActivity activity;
        [SetUp]
        public void SetUp()
        {
            main = new MainBase();
            activity = NewMock<IActivity>();
            Stub.On(activity).GetProperty("Start").Will(Return.Value(DateTime.Parse("7:00:00")));
            Stub.On(activity).GetProperty("Duration").Will(Return.Value(TimeSpan.Parse("0:30:00")));
        }
        [Test]
        public void GetPopupText()
        {
            Assert.AreEqual("working from 7:00:00 for 0:30:00",main.GetPopupText("working",activity));
        }
        [Test]
        public void GetPopupText64()
        {
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyz78901234567… from 7:00:00 for 0:30:00", main.GetPopupText("abcdefghijklmnopqrstuvwxyz7890123456789", activity));
        }
        [Test]
        public void GetPopupText63()
        {
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyz789012345678 from 7:00:00 for 0:30:00", main.GetPopupText("abcdefghijklmnopqrstuvwxyz789012345678", activity));
        }
        [Test]
        public void SetLocation()
        {
            main.SetLocation(new Point(5,10));
            Assert.AreEqual(new Point(5,10), main.Location);
        }
        [Test]
        public void XLocationCorrectedInOrderToBeVisible()
        {
            main.SetLocation(new Point(-5, 10));
            Assert.AreEqual(new Point(0, 10), main.Location);
        }
        [Test]
        public void NegativeYLocation()
        {
            main.SetLocation(new Point(5, -5));
            Assert.AreEqual(new Point(5, 0), main.Location);
        }
        [Test]
        public void BottomIsOutOfScreen()
        {
            main.SetLocation(new Point(5, 5000));
            Assert.AreEqual(Screen.PrimaryScreen.WorkingArea.Bottom-main.Height,main.Location.Y);
        }
        [Test]
        public void RightSizeIsOutOfScreen()
        {
            main.SetLocation(new Point(5000, 5));
            Assert.AreEqual(Screen.PrimaryScreen.WorkingArea.Right-main.Width,main.Location.X);
        }
    }
}
