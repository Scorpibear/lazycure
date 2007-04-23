using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core;
using LifeIdea.LazyCure.UI;



namespace LifeIdea.LazyCure
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(new Driver()));
        }
    }
}
