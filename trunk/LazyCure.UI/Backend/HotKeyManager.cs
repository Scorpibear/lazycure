using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI
{
    public class HotKeyManager
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd,
          int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public bool Register(Form form)
        {
            // Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            return RegisterHotKey(form.Handle,form.GetType().GetHashCode(), 2, (int)Keys.F12);
        }
        public bool Unregister(Form form)
        {
            return UnregisterHotKey(form.Handle, form.GetType().GetHashCode());
        }
    }
}
