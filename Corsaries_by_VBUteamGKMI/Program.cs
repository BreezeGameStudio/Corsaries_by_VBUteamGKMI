using System;
using System.Windows.Forms;
using Corsaries_by_VBUteamGKMI.Model;
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
            while (true)
            {
                Menu menu = new Menu();
                Application.Run(menu);

                if (menu.DialogResult == DialogResult.OK)
                {
                    new Game1().Run();
                }
                else if(menu.DialogResult == DialogResult.Yes)
                {
                    new Game1(Save.Load_Progress()).Run();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
