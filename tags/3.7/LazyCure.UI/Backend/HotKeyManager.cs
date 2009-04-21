using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;

namespace LifeIdea.LazyCure.UI.Backend
{
    public class HotKeyManager
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd,
          int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public bool Register(IWin32Window window, IHotKeyCodeProvider hotKey)
        {
			try
			{
                if (window == null)
                {
                    Log.Error("Could not register hot key for null window");
                    return false;
                }
                if (hotKey == null)
                {
                    Log.Error("Could not register null hot key");
                    return false;
                }
                return RegisterHotKey(window.Handle, GetID(window), hotKey.ModifiersCode, hotKey.Code);
			}
			catch(Exception ex)
			{
                Log.Error(String.Format("Could not register hot key '{0}'", hotKey));
                Log.Exception(ex);
				return false;
			}
        }

        private static int GetID(IWin32Window window)
        {
            return window.GetType().GetHashCode();
        }
        public bool Unregister(IWin32Window window)
        {
            return UnregisterHotKey(window.Handle, GetID(window));
        }
    }
}
