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
                Menu menu = new Menu();
                Application.Run(menu);
                Game1 New_Game;
                Game1 Game_fo_Save;

                if (menu.DialogResult == DialogResult.OK)
                {
                    using (New_Game = new Game1())
                    { New_Game.Run(); }
                                                          
                }
                else if (menu.DialogResult == DialogResult.Yes)
                {
                    using (Game_fo_Save = new Game1(SaveRepository.Load_Progress()))
                    { Game_fo_Save.Run(); }                                                    
                }
                else
                {
                    break;
                }                            
            }
        }
    }
}
