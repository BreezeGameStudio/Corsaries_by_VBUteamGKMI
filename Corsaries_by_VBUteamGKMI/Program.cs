using System;
using System.Windows.Forms;
using Corsaries_by_VBUteamGKMI.Model;
using Corsaries_by_VBUteamGKMI.Model.Save;
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
                using (Menu menu = new Menu())
                {
                    Application.Run(menu);
                    if (menu.DialogResult == DialogResult.OK)
                    {
                        using (Game1 New_Game = new Game1())
                        { New_Game.Run(); }
                    }
                    else if (menu.DialogResult == DialogResult.Yes)
                    {
                        using (Game1 Game_fo_Save = new Game1(SaveRepository.Load_Progress()))
                        { Game_fo_Save.Run(); }
                    }
                    else  {  break; }
                }
            }
        }
    }
}
