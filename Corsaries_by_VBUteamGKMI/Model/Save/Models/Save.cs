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

namespace Corsaries_by_VBUteamGKMI.Model.Save
{
    public class Save
    {
        public int Id;
        public string gameTime;
        public string captain;
        public string ship_type;
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
        public List<string> products = new List<string>();
        public List<string> sailors = new List<string>();

        public Save()
        {

        }

        public Save(MyShip ship, DateTime date)
        {
            this.gameTime = date.ToShortDateString();
            this.captain = ship._captain.ToString();
            this.ship_type = Enum.GetName(typeof(Ship_type),ship._ship_type);
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
    }
}
