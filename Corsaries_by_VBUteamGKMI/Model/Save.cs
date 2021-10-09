using System;
using System.Collections.Generic;
using System.Text;
using Corsaries_by_VBUteamGKMI.Model.Ship;
using Corsaries_by_VBUteamGKMI.Model.People_on_ship;
using Corsaries_by_VBUteamGKMI.Model.Products;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using Corsaries_by_VBUteamGKMI.View;
using System.Data.SQLite;
using Dapper;

namespace Corsaries_by_VBUteamGKMI.Model
{
    public class Save
    {
        public string gameTime;
        public string captain;
        public Ship_type ship_type;
        public List<string> products = new List<string>();
        public List<string> sailors = new List<string>();
        public string name;
        public int price;
        public int current_count_sailors;
        public int max_count_sailors;
        public int max_capacity;
        public int current_capacity;
        public int max_hp;
        public int current_hp;
        public float speed;
        public string cannon;
        public int count_cannon;
        public int protection;
        public int dodge_chance;
        public float position_x;
        public float position_y;


        public Save()
        {

        }

        public Save(MyShip ship, DateTime date)
        {
            this.gameTime = date.ToShortDateString();
            this.captain = ship._captain.ToString();
            this.ship_type = ship._ship_type;
            foreach (var item in ship._products)
            {
                this.products.Add(item.ToString());
            }
            foreach (var item in ship._sailors)
            {
                this.sailors.Add(item.ToString());
            }
            this.name = ship._name;
            this.price = ship._price;
            this.current_count_sailors = ship._current_count_sailors;
            this.max_count_sailors = ship._max_count_sailors;
            this.max_capacity = ship._max_capacity;
            this.max_hp = ship._max_hp;
            this.current_hp = ship._current_hp;
            this.speed = ship._speed;
            this.cannon = ship._cannon.ToString();
            this.count_cannon = ship._count_cannon;
            this.protection = ship._protection;
            this.dodge_chance = ship._dodge_chance;
            this.position_x = ship._position.X;
            this.position_y = ship._position.Y;
        }

        public void Save_Progress()
        {
            string save_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Corsairs\\save.sqlite";
            SQLiteConnection.CreateFile(save_path);

            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={save_path};Version=3;"))
            {
                try
                {
                    conn.Query(@"CREATE TABLE Props (
    Id                    INTEGER PRIMARY KEY
                                  NOT NULL
                                  UNIQUE,
    gameTime              VARCHAR NOT NULL,
    captain                       NOT NULL,
    ship_type             VARCHAR NOT NULL,
    name                  VARCHAR NOT NULL,
    price                 INTEGER NOT NULL,
    current_count_sailors INTEGER NOT NULL,
    max_count_sailors     INTEGER NOT NULL,
    max_capacity          INTEGER NOT NULL,
    current_capacity      INTEGER NOT NULL,
    max_hp                INTEGER NOT NULL,
    current_hp            INTEGER NOT NULL,
    speed                 DECIMAL NOT NULL,
    cannon                VARCHAR NOT NULL,
    count_cannon          INTEGER NOT NULL,
    protection            INTEGER NOT NULL,
    dodge_chance          INTEGER NOT NULL,
    position_x            DECIMAL NOT NULL,
    position_y            DECIMAL NOT NULL
);
                    ");
                    conn.Query(@"CREATE TABLE Products (
    Id    INTEGER PRIMARY KEY
                  UNIQUE
                  NOT NULL,
    Value VARCHAR NOT NULL
);
                    ");
                    conn.Query(@"CREATE TABLE Sailors (
    Id    INTEGER PRIMARY KEY
                  UNIQUE
                  NOT NULL,
    Value VARCHAR NOT NULL
);
                    ");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={save_path};Version=3;"))
            {
                try
                {
                    conn.Query("INSERT INTO Props (gameTime,captain,ship_type,name,price,current_count_sailors,max_count_sailors,max_capacity,current_capacity,max_hp,current_hp,speed,cannon,count_cannon,protection,dodge_chance,position_x,position_y) VALUES(@gameTime,@captain,@ship_type,@name,@price,@current_count_sailors,@max_count_sailors,@max_capacity,@current_capacity,@max_hp,@current_hp,@speed,@cannon,@count_cannon,@protection,@dodge_chance,@position_x,@position_y)", this);
                }
                catch (Exception)
                {

                    throw;
                }
            }




            //XmlSerializer serializer = new XmlSerializer(typeof(Save));
            //using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Corsairs\\save.xml"))
            //{
            //    serializer.Serialize(writer, this);
            //}
        }

        public static Save Load_Progress()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Save));
            using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Corsairs\\save.xml"))
            {
                return serializer.Deserialize(reader) as Save;
            }
        }
    }
}
