using System.Drawing;
using System.Windows.Forms;

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
