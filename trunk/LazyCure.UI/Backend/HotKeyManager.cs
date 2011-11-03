using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.UI.Backend
{
    public class HotKeyManager
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd,
          int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public bool Register(IWin32Window window, int id, IHotKeyCodeProvider hotKey)
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
                return RegisterHotKey(window.Handle, id, hotKey.ModifiersCode, hotKey.Code);
			}
			catch(Exception ex)
			{
                Log.Error(String.Format("Could not register hot key '{0}'", hotKey));
                Log.Exception(ex);
				return false;
			}
        }

        public bool Unregister(IWin32Window window, int id)
        {
            try
            {
                return UnregisterHotKey(window.Handle, id);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }
    }
}
