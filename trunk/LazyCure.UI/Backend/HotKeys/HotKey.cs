using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LifeIdea.LazyCure.UI.Backend.HotKeys
{
    public class HotKey : IHotKeyCodeProvider
    {
        public const Keys DefaultKey = Keys.F12;
        public bool Alt;
        public bool Ctrl;
        public bool Shift;
        public bool Win;
        public Keys Key;
        public static HotKey Parse(string str)
        {
            HotKey hotKey = new HotKey();
            hotKey.Ctrl = str.Contains("Ctrl");
            hotKey.Alt = str.Contains("Alt");
            hotKey.Shift = str.Contains("Shift");
            hotKey.Win = str.Contains("Win");
            string[] definitions = str.Split('+');
            string key = definitions[definitions.Length - 1];
            KeysConverter converter = new KeysConverter();
            try
            {
                hotKey.Key = (Keys)converter.ConvertFromString(key);
            }
            catch
            { hotKey.Key = DefaultKey; }
            return hotKey;
        }

        public int Code
        {
            get
            {
                return (int)Key;
            }
        }

        // Alt = 1, Ctrl = 2, Shift = 4, Win = 8
        public int ModifiersCode
        {
            get
            {
                int code = 0;
                if (Alt)
                    code += 1;
                if (Ctrl)
                    code += 2;
                if (Shift)
                    code += 4;
                if (Win)
                    code += 8;
                return code;
            }
        }

        public string JustKeyString
        {
            get
            {
                string keyStr = Key.ToString();
                return (Regex.Match(keyStr, "D[0-9]").Success) ? keyStr[1].ToString() : keyStr;
            }
        }

        public override string ToString()
        {
            string str = "";
            str += (Win) ? "Win+" : "";
            str += (Ctrl) ? "Ctrl+" : "";
            str += (Alt) ? "Alt+" : "";
            str += (Shift) ? "Shift+" : "";
            str += JustKeyString;
            return str;
        }
    }
}
