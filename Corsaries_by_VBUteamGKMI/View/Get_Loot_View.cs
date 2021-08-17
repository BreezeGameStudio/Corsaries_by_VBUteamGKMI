using Corsaries_by_VBUteamGKMI.Model.Products;
using Corsaries_by_VBUteamGKMI.Model.Ship;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Corsaries_by_VBUteamGKMI.View
{
    public partial class Get_Loot_View : Form
    {
        public Ship _my_ship;
        public Ship _enemy_ship;
        public int _new_current_capacity;
        public Get_Loot_View(Ship my_ship, Ship enemy_ship)
        {
            InitializeComponent();
            _my_ship = my_ship;
            _enemy_ship = enemy_ship;
            _new_current_capacity = _my_ship._current_capacity;
            _capacity_txt.Text = $"{_my_ship._current_capacity}/{_new_current_capacity}";

            // money
            Money_value.Text = "0";
            Money_count.Text = _enemy_ship._captain._money.ToString();
            Money_bar.Maximum = _enemy_ship._captain._money;
            Money_bar.Value = 0;
            Money_bar.Scroll += Money_bar_Scroll;
            //Rum
            Rum_value.Text = "0";
            Rum_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._count.ToString();
            Rum_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._count;
            Rum_bar.Value = 0;
            Rum_bar.Scroll += Rum_bar_Scroll;
            //Silk
            //Water
            //Food
            //Leather
            //Wood
            //Tobacco
            //Coffee

        }

        private void Money_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Money_value.Text = Money_bar.Value.ToString();
        }

        private void Rum_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Rum_value.Text = Rum_bar.Value.ToString();
            if (e.OldValue > e.NewValue)
            { _new_current_capacity += Rum_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight; }
            else
            { _new_current_capacity -= Rum_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight; }
            _capacity_txt.Text = $"{_my_ship._current_capacity}/{_new_current_capacity}";
        }



    }
}
