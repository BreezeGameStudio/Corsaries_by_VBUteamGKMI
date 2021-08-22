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
        public Ship _my_ship;// наша лодка
        public Ship _enemy_ship; // лодка врага
        public int _new_current_capacity; // текущее зачение загружености
        public int _loot_capacity =0; // текущее значение награбленого
        Product my_rum;
        Product enemy_rum;
        Product my_Silk;
        Product enemy_Silk;
        Product my_Water;
        Product enemy_Water;
        Product my_Food;
        Product enemy_Food;
        Product my_Leather;
        Product enemy_Leather;
        Product my_Wood;
        Product enemy_Wood;
        Product my_Tobacco;
        Product enemy_Tobacco;
        Product my_Coffee;
        Product enemy_Coffee;
        public Get_Loot_View(Ship my_ship, Ship enemy_ship)
        {
            InitializeComponent();
            _my_ship = my_ship;
            _enemy_ship = enemy_ship;
            _new_current_capacity = _my_ship._current_capacity;
            _capacity_txt.Text = $"{_new_current_capacity+ _loot_capacity}/{_my_ship._max_capacity}";

            // кнопака забрать лут
            btn_get_loot.Click += Btn_get_loot_Click;
            select_all_btn.Click += Select_all_btn_Click;

            // money
            Money_value.Text = "0";
            Money_count.Text = _enemy_ship._captain._money.ToString();
            Money_bar.Maximum = _enemy_ship._captain._money;
            Money_bar.Value = 0;
            Money_bar.LargeChange = 1;
            Money_bar.Scroll += Money_bar_Scroll;
            my_rum = _my_ship._products.Find(i => i._product_Type == Product_type.Rum);
            enemy_rum =_enemy_ship._products.Find(i => i._product_Type == Product_type.Rum);
            my_Silk = _my_ship._products.Find(i => i._product_Type == Product_type.Silk);
            enemy_Silk =_enemy_ship._products.Find(i => i._product_Type == Product_type.Silk);
            my_Water = _my_ship._products.Find(i => i._product_Type == Product_type.Water);
            enemy_Water =_enemy_ship._products.Find(i => i._product_Type == Product_type.Water);
            my_Food = _my_ship._products.Find(i => i._product_Type == Product_type.Food);
            enemy_Food =_enemy_ship._products.Find(i => i._product_Type == Product_type.Food);
            my_Leather = _my_ship._products.Find(i => i._product_Type == Product_type.Leather);
            enemy_Leather =_enemy_ship._products.Find(i => i._product_Type == Product_type.Leather);
            my_Wood = _my_ship._products.Find(i => i._product_Type == Product_type.Wood);
            enemy_Wood =_enemy_ship._products.Find(i => i._product_Type == Product_type.Wood);
            my_Tobacco = _my_ship._products.Find(i => i._product_Type == Product_type.Tobacco);
            enemy_Tobacco =_enemy_ship._products.Find(i => i._product_Type == Product_type.Tobacco);
            my_Coffee = _my_ship._products.Find(i => i._product_Type == Product_type.Coffee);
            enemy_Coffee =_enemy_ship._products.Find(i => i._product_Type == Product_type.Coffee);
            //Rum
            rum.Text = my_rum._count.ToString();
            Rum_value.Text = "0";
            Rum_count.Text = enemy_rum._count.ToString();
            Rum_bar.Maximum = enemy_rum._count;
            Rum_bar.Minimum = my_rum._count - (my_rum._count * 2);
            Rum_bar.Value = 0;
            
            Rum_bar.LargeChange = 1;
            Rum_bar.Scroll += bar_Scroll;
            //Silk
            shilk.Text = my_Silk._count.ToString();
            Silk_value.Text = "0";
            Silk_count.Text = enemy_Silk._count.ToString();
            Silk_bar.Maximum = enemy_Silk._count;
            Silk_bar.Minimum = my_Silk._count - (my_Silk._count * 2);
            Silk_bar.Value = 0;
           
            Silk_bar.LargeChange = 1;
            Silk_bar.Scroll += bar_Scroll;
            //Water
            water.Text = my_Water._count.ToString();
            Water_value.Text = "0";
            Water_count.Text = enemy_Water._count.ToString();
            Water_bar.Maximum = enemy_Water._count;
            Water_bar.Minimum = my_Water._count - (my_Water._count * 2);
            Water_bar.Value = 0;
           
            Water_bar.LargeChange = 1;
            Water_bar.Scroll += bar_Scroll;
            //Food
            food.Text = my_Food._count.ToString();
            Food_value.Text = "0";
            Food_count.Text = enemy_Food._count.ToString();
            Food_bar.Maximum = enemy_Food._count;
            Food_bar.Minimum = my_Food._count - (my_Food._count * 2);
            Food_bar.Value = 0;
          
            Food_bar.LargeChange = 1;
            Food_bar.Scroll += bar_Scroll;
            //Leather
            leather.Text = my_Leather._count.ToString();
            Leather_value.Text = "0";
            Leather_count.Text = enemy_Leather._count.ToString();
            Leather_bar.Maximum = enemy_Leather._count;
            Leather_bar.Minimum = my_Leather._count - (my_Leather._count * 2);
            Leather_bar.Value = 0;
            
            Leather_bar.LargeChange = 1;
            Leather_bar.Scroll += bar_Scroll;
            //Wood
            wood.Text = my_Wood._count.ToString();
            Wood_value.Text = "0";
            Wood_count.Text = enemy_Wood._count.ToString();
            Wood_bar.Maximum = enemy_Wood._count;
            Wood_bar.Minimum = my_Wood._count - (my_Wood._count * 2);
            Wood_bar.Value = 0;
           
            Wood_bar.LargeChange = 1;
            Wood_bar.Scroll += bar_Scroll;
            //Tobacco
            tobacco.Text = my_Tobacco._count.ToString();
            Tobacco_value.Text = "0";
            Tobacco_count.Text = enemy_Tobacco._count.ToString();
            Tobacco_bar.Maximum = enemy_Tobacco._count;
            Tobacco_bar.Minimum = my_Tobacco._count - (my_Tobacco._count * 2);
            Tobacco_bar.Value = 0;
           
            Tobacco_bar.LargeChange = 1;
            Tobacco_bar.Scroll += bar_Scroll;
            //Coffee
            coffee.Text = my_Coffee._count.ToString();
            Coffee_value.Text = "0";
            Coffee_count.Text = enemy_Coffee._count.ToString();
            Coffee_bar.Maximum = enemy_Coffee._count;
            Coffee_bar.Minimum = my_Coffee._count - (my_Coffee._count * 2);
            Coffee_bar.Value = 0;
          
            Coffee_bar.LargeChange = 1;
            Coffee_bar.Scroll += bar_Scroll;

        }

        private void Select_all_btn_Click(object sender, EventArgs e)
        {
            Money_bar.Value = _enemy_ship._captain._money;
            Rum_bar.Value = enemy_rum._count;
            Silk_bar.Value = enemy_Silk._count;
            Water_bar.Value = enemy_Water._count;
            Food_bar.Value = enemy_Food._count;
            Leather_bar.Value = enemy_Leather._count;
            Wood_bar.Value = enemy_Wood._count;
            Tobacco_bar.Value = enemy_Tobacco._count;
            Coffee_bar.Value = enemy_Coffee._count;
            Calculation_Capacity();
        }

        // расчет загрузки
        private void Calculation_Capacity()
        {
            Rum_value.Text = Rum_bar.Value.ToString();
            Silk_value.Text = Silk_bar.Value.ToString();
            Water_value.Text = Water_bar.Value.ToString();
            Food_value.Text = Food_bar.Value.ToString();
            Leather_value.Text = Leather_bar.Value.ToString();
            Wood_value.Text = Wood_bar.Value.ToString();
            Tobacco_value.Text = Tobacco_bar.Value.ToString();
            Coffee_value.Text = Coffee_bar.Value.ToString();

            _loot_capacity = 0;
            
            // расчет веса
            _loot_capacity += Rum_bar.Value * my_rum._weight;
            _loot_capacity += Silk_bar.Value * my_Silk._weight;
            _loot_capacity += Water_bar.Value * my_Water._weight;
            _loot_capacity += Food_bar.Value * my_Food._weight;
            _loot_capacity += Leather_bar.Value * my_Leather._weight;
            _loot_capacity += Wood_bar.Value * my_Wood._weight;
            _loot_capacity += Tobacco_bar.Value * my_Tobacco._weight;
            _loot_capacity += Coffee_bar.Value * my_Coffee._weight;
            _capacity_txt.Text = $"{_new_current_capacity + _loot_capacity}/{_my_ship._max_capacity}";
        }
        private void Btn_get_loot_Click(object sender, EventArgs e)
        {
            if (_new_current_capacity + _loot_capacity > _my_ship._max_capacity)
            {
                MessageBox.Show("Вам столько не влезет", "ПЕРЕГРУЗ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _my_ship._current_capacity = _new_current_capacity + _loot_capacity;
            _my_ship._captain._money += Money_bar.Value;
            _my_ship.AddProduct(Product_type.Rum, Rum_bar.Value);
            _my_ship.AddProduct(Product_type.Silk, Silk_bar.Value);
            _my_ship.AddProduct(Product_type.Water, Water_bar.Value);
            _my_ship.AddProduct(Product_type.Food, Food_bar.Value);
            _my_ship.AddProduct(Product_type.Leather, Leather_bar.Value);
            _my_ship.AddProduct(Product_type.Wood, Wood_bar.Value);
            _my_ship.AddProduct(Product_type.Tobacco, Tobacco_bar.Value);
            _my_ship.AddProduct(Product_type.Coffee, Coffee_bar.Value);
            Close();
        }     
        private void Money_bar_Scroll(object sender, ScrollEventArgs e) => Money_value.Text = Money_bar.Value.ToString();
        private void bar_Scroll(object sender, ScrollEventArgs e) => Calculation_Capacity();
       
    }
}
