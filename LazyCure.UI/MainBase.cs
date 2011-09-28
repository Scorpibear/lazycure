using System;
using System.Globalization;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI
{
    public class MainBase: Form
    {
        public void Display()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        public string GetPopupText(string activityName, IActivity activity)
        {
            int maxLengthAllowed = 63;
            string timePart = String.Format(" "+Constants.FromAndFor, Format.Time(activity.Start),
                            Format.Duration(activity.Duration));
            int activityMaxLength = maxLengthAllowed - timePart.Length;
            string activityPart = activityName;
            if(activityName.Length>activityMaxLength)
                activityPart = activityName.Substring(0, activityMaxLength - 1) + "…";
            return activityPart + timePart;
        }

        public void SetLocation(Point location)
        {
            Size desktopSize = Screen.PrimaryScreen.WorkingArea.Size;
            if (location.X < 0)
                location.X = 0;
            if (location.X > desktopSize.Width - Width)
                location.X = desktopSize.Width - Width;
            if (location.Y < 0)
                location.Y = 0;
            if (location.Y > desktopSize.Height - Height)
                location.Y = desktopSize.Height - Height;
            Location = location;
        }
    }
}
