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
        public Get_Loot_View(Ship my_ship, Ship enemy_ship)
        {
            InitializeComponent();
            _my_ship = my_ship;
            _enemy_ship = enemy_ship;
            _new_current_capacity = _my_ship._current_capacity;
            _capacity_txt.Text = $"{_new_current_capacity+ _loot_capacity}/{_my_ship._max_capacity}";

            // кнопака забрать лут
            btn_get_loot.Click += Btn_get_loot_Click;

            // money
            Money_value.Text = "0";
            Money_count.Text = _enemy_ship._captain._money.ToString();
            Money_bar.Maximum = _enemy_ship._captain._money;
            Money_bar.Value = 0;
            Money_bar.LargeChange = 1;
            Money_bar.Scroll += Money_bar_Scroll;
            //Rum
            Rum_value.Text = "0";
            Rum_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._count.ToString();
            Rum_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._count;
            Rum_bar.Value = 0;
            Rum_bar.LargeChange = 1;
            Rum_bar.Scroll += Rum_bar_Scroll;
            //Silk
            Silk_value.Text = "0";
            Silk_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Silk)._count.ToString();
            Silk_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Silk)._count;
            Silk_bar.Value = 0;
            Silk_bar.LargeChange = 1;
            Silk_bar.Scroll += Silk_bar_Scroll;
            //Water
            Water_value.Text = "0";
            Water_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Water)._count.ToString();
            Water_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Water)._count;
            Water_bar.Value = 0;
            Water_bar.LargeChange = 1;
            Water_bar.Scroll += Water_bar_Scroll;
            //Food
            Food_value.Text = "0";
            Food_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Food)._count.ToString();
            Food_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Food)._count;
            Food_bar.Value = 0;
            Food_bar.LargeChange = 1;
            Food_bar.Scroll += Food_bar_Scroll;
            //Leather
            Leather_value.Text = "0";
            Leather_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Leather)._count.ToString();
            Leather_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Leather)._count;
            Leather_bar.Value = 0;
            Leather_bar.LargeChange = 1;
            Leather_bar.Scroll += Leather_bar_Scroll;
            //Wood
            Wood_value.Text = "0";
            Wood_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Wood)._count.ToString();
            Wood_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Wood)._count;
            Wood_bar.Value = 0;
            Wood_bar.LargeChange = 1;
            Wood_bar.Scroll += Wood_bar_Scroll;
            //Tobacco
            Tobacco_value.Text = "0";
            Tobacco_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Tobacco)._count.ToString();
            Tobacco_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Tobacco)._count;
            Tobacco_bar.Value = 0;
            Tobacco_bar.LargeChange = 1;
            Tobacco_bar.Scroll += Tobacco_bar_Scroll;
            //Coffee
            Coffee_value.Text = "0";
            Coffee_count.Text = _enemy_ship._products.Find(i => i._product_Type == Product_type.Coffee)._count.ToString();
            Coffee_bar.Maximum = _enemy_ship._products.Find(i => i._product_Type == Product_type.Coffee)._count;
            Coffee_bar.Value = 0;
            Coffee_bar.LargeChange = 1;
            Coffee_bar.Scroll += Coffee_bar_Scroll;
        }
        // расчет загрузки
        private void Calculation_Capacity()
        {
            _loot_capacity = 0;
            _loot_capacity += Rum_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight;
            _loot_capacity += Silk_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight;
            _loot_capacity += Water_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight;
            _loot_capacity += Food_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight;
            _loot_capacity += Leather_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight;
            _loot_capacity += Wood_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight;
            _loot_capacity += Tobacco_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight;
            _loot_capacity += Coffee_bar.Value * _enemy_ship._products.Find(i => i._product_Type == Product_type.Rum)._weight;
            _capacity_txt.Text = $"{_new_current_capacity + _loot_capacity}/{_my_ship._max_capacity}";
        }
        private void Btn_get_loot_Click(object sender, EventArgs e)
        {
            if(_new_current_capacity+ _loot_capacity > _my_ship._max_capacity)
            {
                MessageBox.Show("Вам столько не влезет", "ПЕРЕГРУЗ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _my_ship._current_capacity = _new_current_capacity+ _loot_capacity;
            _my_ship._captain._money += Money_bar.Value;
            _my_ship._products.Find(i => i._product_Type == Product_type.Rum)._count += Rum_bar.Value;
            _my_ship._products.Find(i => i._product_Type == Product_type.Silk)._count += Silk_bar.Value;
            _my_ship._products.Find(i => i._product_Type == Product_type.Water)._count += Water_bar.Value;
            _my_ship._products.Find(i => i._product_Type == Product_type.Food)._count += Food_bar.Value;
            _my_ship._products.Find(i => i._product_Type == Product_type.Leather)._count += Leather_bar.Value;
            _my_ship._products.Find(i => i._product_Type == Product_type.Wood)._count += Wood_bar.Value;
            _my_ship._products.Find(i => i._product_Type == Product_type.Tobacco)._count += Tobacco_bar.Value;
            _my_ship._products.Find(i => i._product_Type == Product_type.Coffee)._count += Coffee_bar.Value;
           Close();
        }       
        private void Money_bar_Scroll(object sender, ScrollEventArgs e) => Money_value.Text = Money_bar.Value.ToString();
        private void Rum_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Rum_value.Text = Rum_bar.Value.ToString();         
            Calculation_Capacity();
        }
        private void Silk_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Silk_value.Text = Silk_bar.Value.ToString();
            Calculation_Capacity();
        }
        private void Water_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Water_value.Text = Water_bar.Value.ToString();         
            Calculation_Capacity();
        }
        private void Food_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Food_value.Text = Food_bar.Value.ToString();
            Calculation_Capacity();
        }
        private void Leather_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Leather_value.Text = Leather_bar.Value.ToString();
            Calculation_Capacity();
        }
        private void Wood_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Wood_value.Text = Wood_bar.Value.ToString();
            Calculation_Capacity();
        }
        private void Tobacco_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Tobacco_value.Text = Tobacco_bar.Value.ToString();
            Calculation_Capacity();
        }
        private void Coffee_bar_Scroll(object sender, ScrollEventArgs e)
        {
            Coffee_value.Text = Coffee_bar.Value.ToString();
            Calculation_Capacity();
        }
    }
}
