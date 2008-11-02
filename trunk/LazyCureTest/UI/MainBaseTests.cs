using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using LifeIdea.LazyCure.Properties;
using System.Drawing;
using LifeIdea.LazyCure.Core;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI
{
    [TestFixture]
    public class MainBaseTests
    {
        MainBase main;
        [SetUp]
        public void SetUp()
        {
            main = new MainBase();
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
