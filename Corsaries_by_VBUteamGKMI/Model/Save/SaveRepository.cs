using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace Corsaries_by_VBUteamGKMI.Model.Save
{
    static class SaveRepository
    {
        static string save_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Corsairs\\save.sqlite";

        public static  void Save_Progress(Save save)
        {

            SQLiteConnection.CreateFile(save_path);

            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={save_path};Version=3;"))
            {
                try
                {
                    PropsRepository.CreateTable();
                    ProductsRepository.CreateTable();
                    SailorsRepository.CreateTable();
                      
                        conn.Query("INSERT INTO Props (gameTime,captain,ship_type,name,price,current_count_sailors,max_count_sailors,max_capacity,current_capacity,max_hp,current_hp,speed,cannon,count_cannon,protection,dodge_chance,position_x,position_y) VALUES(@gameTime,@captain,@ship_type,@name,@price,@current_count_sailors,@max_count_sailors,@max_capacity,@current_capacity,@max_hp,@current_hp,@speed,@cannon,@count_cannon,@protection,@dodge_chance,@position_x,@position_y)", new { save.gameTime, save.captain, save.ship_type, save.name, save.price, save.current_count_sailors, save.max_count_sailors, save.max_capacity, save.current_capacity, save.max_hp, save.current_hp, save.speed, save.cannon, save.count_cannon, save.protection, save.dodge_chance, save.position_x, save.position_y });                                                 
                        foreach (var item in save.products)
                        {
                            conn.Query($"INSERT INTO Products (Value) VALUES(\'{item}\')");
                        }                 
                        foreach (var item in save.sailors)
                        {
                            conn.Query($"INSERT INTO Sailors (Value) VALUES(\'{item}\')");
                        }
                    MessageBox.Show("Успещное сохранение", "Сохранеине", new List<string> { "OK" });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public static Save Load_Progress()
        {
            Save save = new Save();
            if (!File.Exists(save_path))
            {
                return null;
            }
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={save_path};Version=3;"))
            {
                try
                {
                    save = conn.Query<Save>("SELECT * FROM Props").ToList()[0];
                    foreach (var item in conn.Query<Corsaries_by_VBUteamGKMI.Model.Save.Models.Products>($"SELECT * FROM Products"))
                    {
                        save.products.Add(item.Value);
                    }
                    foreach (var item in conn.Query<Corsaries_by_VBUteamGKMI.Model.Save.Models.Sailors>($"SELECT * FROM Sailors"))
                    {
                        save.sailors.Add(item.Value);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return save;
        }

    }
}