using System;
using System.Windows.Forms;
using Corsaries_by_VBUteamGKMI.View;

namespace Corsaries_by_VBUteamGKMI
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Menu menu = new Menu();
            Application.Run(menu);
            
            if(menu.DialogResult == DialogResult.Abort)
            {
                new Game1().Run();
            }
        }

        public static void Run_Menu()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Menu menu = new Menu();
            Application.Run(menu);

            if (menu.DialogResult == DialogResult.None)
            {
                new Game1().Run();
            }
        }
    }
}
