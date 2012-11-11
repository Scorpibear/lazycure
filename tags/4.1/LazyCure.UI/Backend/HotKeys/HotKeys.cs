using System;
using System.Collections.Generic;

namespace LifeIdea.LazyCure.UI.Backend.HotKeys
{
    /// <summary>
    /// Represent all possible HotKeys
    /// </summary>
    public class HotKeys
    {
        /// <summary>
        /// Returns all hotkeys names without modificators
        /// </summary>
        /// <returns>hotkeys names</returns>
        public static string[] GetAllNames()
        {
            List<String> keys = new List<string>();
            for (int i = 1; i <= 12; i++)
                keys.Add("F" + i.ToString());
            for (int i = 0; i <= 9; i++)
                keys.Add(i.ToString());
            for (char ch = 'A'; ch <= 'Z'; ch++)
                keys.Add(ch.ToString());
            return keys.ToArray();
        }
    }
}
