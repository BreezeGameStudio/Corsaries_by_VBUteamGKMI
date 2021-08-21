using Corsaries_by_VBUteamGKMI.Model;
using Corsaries_by_VBUteamGKMI.Model.People_on_ship;
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
    public partial class SeaportView : Form
    {
        public Ship _ship;
        public Ship _temp_ship;
        public Cannon _temp_cannon;
        public Seaport _seaport;      
        public int _loot_capacity = 0; // текущее вес награбленого
        public int _count_new_sailors = 0; //количество новых матросов
        public int _price_1hp_cap; // цена за единицу выличеного хп
        public int _loot_price; // текущая цена награбленого
        public int _sailor_price; // текущая цена всех матросов
        public int _price_1_unga; // цена 1го юнгу
        public int _price_1_Experienced; // цена 1го бывалого
        public int _price_1_Sea_wolf; // цена 1го морского волка
        Product my_rum, port_rum, my_Silk, port_Silk, my_Water,
            port_Water, my_Food, port_Food, my_Leather, port_Leather,
            my_Wood, port_Wood, my_Tobacco, port_Tobacco, my_Coffee, port_Coffee;
        Sailor my_Jung, port_Jung, my_Experienced, port_Experienced, my_Sea_wolf, port_Sea_wolf;



        public SeaportView(Ship ship, Seaport seaport)
        {
           
            _ship = ship;
            _seaport = seaport;
            _price_1hp_cap = seaport._price_1hp_cap;// цена за единицу выличеного хп
            _price_1_unga = seaport._price_1_unga; // цена 1го юнгу
            _price_1_Experienced = seaport._price_1_Experienced; // цена 1го бывалого
            _price_1_Sea_wolf = seaport._price_1_Sea_wolf; // цена 1го морского волка

            _temp_ship = new MyShip(_ship._ship_type, _ship._cannon._cunnon_type);
            _temp_cannon = new Cannon(_temp_ship._ship_type,_temp_ship._cannon._cunnon_type);

            InitializeComponent();
            // кнопки
            btn_buy.Click += Btn_buy_Click;
            tabControl1.Selected += TabControl1_Selected;
            update_team_btn.Click += Update_team_btn_Click;
            buy_cannon_btn.Click += Buy_cannon_btn_Click;
            buy_ship_btn.Click += Buy_ship_btn_Click;

            shipCB.SelectedIndex = shipCB.Items.IndexOf(_ship._name);
            shipCB.SelectedIndexChanged += ShipCB_SelectedIndexChanged;


            cannonCB.SelectedIndex = cannonCB.Items.IndexOf(_ship._cannon._name);
            cannonCB.SelectedIndexChanged += CannonCB_SelectedIndexChanged;


            SetSettingShipyard();
            SetSettingMarket();
            SetSettingHospital();
            SetSettingTaverna();
        }

       

        // обновление данных при переключение вкладок
        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            SetSettingShipyard();
            SetSettingMarket();
            SetSettingHospital();
            SetSettingTaverna();
        }

        #region верфь
        private void SetSettingShipyard()
        {                       
            SetSettingShipyard_ship();
            SetSettingShipyard_cannon();
           
            if (_temp_ship._ship_type == _ship._ship_type)
                buy_ship_btn.Enabled = false;
            else
                buy_ship_btn.Enabled = true;


            if (_temp_ship._ship_type == _ship._ship_type &&
                _temp_cannon._cunnon_type == _ship._cannon._cunnon_type)
                buy_cannon_btn.Enabled = false;
            else
                buy_cannon_btn.Enabled = true;

        }
        private void Buy_ship_btn_Click(object sender, EventArgs e)
        {
            if (_temp_ship._price > _ship._captain._money)
            {
                MessageBox.Show("Маловато золотишка =(", "ПРОСТИ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_temp_ship._ship_type == _ship._ship_type)
            {
                MessageBox.Show("У тебя такой же корабыль!", "ЕЙ АЛЛО!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ship._captain.SpendMoney(_temp_ship._price);
            _ship.Set_Ship_Type(_temp_ship._ship_type);

            SetSettingShipyard();
        }

        private void Buy_cannon_btn_Click(object sender, EventArgs e)
        {
            if (_temp_cannon._price > _ship._captain._money)
            {
                MessageBox.Show("Маловато золотишка =(", "ПРОСТИ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_temp_ship._ship_type == _ship._ship_type&& 
                _temp_cannon._cunnon_type == _ship._cannon._cunnon_type)
            {
                MessageBox.Show("У тебя такие же пушки !", "ЕЙ АЛЛО!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ship._captain.SpendMoney(_temp_cannon._price);
            _ship._cannon.Set_Cunnon_Type(_ship._ship_type, _temp_cannon._cunnon_type);

            SetSettingShipyard();
        }
        private void CannonCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _temp_cannon.Set_Cunnon_Type((Ship_type)shipCB.SelectedIndex,(Cannon_type)cannonCB.SelectedIndex);           
            SetSettingShipyard();
        }

        private void ShipCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _temp_ship.Set_Ship_Type((Ship_type)shipCB.SelectedIndex);
            _temp_cannon.Set_Cunnon_Type((Ship_type)shipCB.SelectedIndex, (Cannon_type)cannonCB.SelectedIndex);
            SetSettingShipyard();
        }

        private void SetSettingShipyard_ship()
        {
            _ship_capacity.Text = _temp_ship._max_capacity.ToString();
            _ship_caunt_sailors.Text = _temp_ship._max_count_sailors.ToString();
            _ship_price.Text = _temp_ship._price.ToString();
            _ship_name.Text = _temp_ship._name;           
            _ship_cannon_count.Text = _temp_ship._count_cannon.ToString();
            _ship_DODGE.Text = _temp_ship._dodge_chance.ToString();
            _ship_DEF.Text = _temp_ship._protection.ToString();
            _ship_SPEED.Text = _temp_ship._speed.ToString();
            _ship_HP.Text = $"{_temp_ship._current_hp}/{_temp_ship._max_hp}";

        }
        private void SetSettingShipyard_cannon()
        {
            _ship_cannon_price.Text = _temp_cannon._price.ToString();
            _ship_CANNON_RANGE.Text = _temp_cannon._range.ToString();
            _ship_CANNON_SPEED.Text = _temp_cannon._speed.ToString();
            _ship_CANNON_TYPE.Text = _temp_cannon._name;
            _ship_DMG.Text = _temp_cannon._damage.ToString();
        }


        #endregion

        #region таверна
        private void Update_team_btn_Click(object sender, EventArgs e)
        {
            if (_sailor_price > _ship._captain._money)
            {
                MessageBox.Show("Маловато золотишка =(", "ПРОСТИ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_ship._current_count_sailors + _count_new_sailors > _ship._max_count_sailors)
            {
                MessageBox.Show("Это слишком большая команда", "ЕЙ АЛЛО!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ship._captain.SpendMoney(JungSB.Value * port_Jung._price);
            _ship._captain.SpendMoney(ExperiencedSB.Value * port_Experienced._price);
            _ship._captain.SpendMoney(Sea_wolfSB.Value * port_Sea_wolf._price);

            my_Jung._count += JungSB.Value;
            my_Experienced._count += ExperiencedSB.Value;
            my_Sea_wolf._count += Sea_wolfSB.Value;

            port_Jung._count -= JungSB.Value;
            port_Experienced._count -= ExperiencedSB.Value;
            port_Sea_wolf._count -= Sea_wolfSB.Value;

            _ship._current_count_sailors += (JungSB.Value + ExperiencedSB.Value + Sea_wolfSB.Value);
           
            _ship._captain.Set_Cap_Prop(_ship._sailors);
            SetSettingTaverna();
            Calculation_Sailors();
        }

        private void SetSettingTaverna()
        {
            gold_sailor.Text = $"{_ship._captain._money}/ +{Math.Abs(_sailor_price)}";
            new_count_sailor.Text = $"{_count_new_sailors}";
            sailors_count.Text = $"{_ship._current_count_sailors}/{_ship._max_count_sailors}";
            sailors_bar.Maximum = _ship._max_count_sailors;
            sailors_bar.Minimum = 0;
            sailors_bar.Value = _ship._current_count_sailors;
            my_Jung = _ship._sailors[0];
            port_Jung = _seaport._sailors[0];
            my_Experienced = _ship._sailors[1]; 
            port_Experienced=_seaport._sailors[1];
            my_Sea_wolf = _ship._sailors[2];
            port_Sea_wolf= _seaport._sailors[2]; 

            sailors_bar.Maximum = _ship._max_count_sailors;
            sailors_bar.Minimum = 0;
            sailors_bar.Value = _ship._current_count_sailors;

            //Jung
            Jung_my.Text = $"{my_Jung._count}";
            Jung_price.Text = $"{port_Jung._price}";
            Jung_port.Text = $"{port_Jung._count}";
            JungSB.Maximum = port_Jung._count;
            JungSB.Minimum = my_Jung._count -(my_Jung._count*2);
            JungSB.Value = 0;
            JungSB.Scroll += Taverna_bar_Scroll;
            Jung_value.Text = JungSB.Value.ToString();

            //Experienced
            Experienced_price.Text = $"{port_Experienced._price}";
            Experienced_my.Text = $"{my_Experienced._count}";
            Experienced_port.Text = $"{port_Experienced._count}";
            ExperiencedSB.Maximum = port_Experienced._count;
            ExperiencedSB.Minimum = my_Experienced._count - (my_Experienced._count * 2);
            ExperiencedSB.Value = 0;
            ExperiencedSB.Scroll += Taverna_bar_Scroll;
            Experienced_value.Text = ExperiencedSB.Value.ToString();

            //Sea_wolf
            Sea_wolf_price.Text = $"{port_Sea_wolf._price}";
            Sea_wolf_my.Text = $"{my_Sea_wolf._count}";
            Sea_wolf_port.Text = $"{port_Sea_wolf._count}";
            Sea_wolfSB.Maximum = port_Sea_wolf._count;
            Sea_wolfSB.Minimum = my_Sea_wolf._count - (my_Sea_wolf._count * 2);
            Sea_wolfSB.Value = 0;
            Sea_wolfSB.Scroll += Taverna_bar_Scroll;
            Sea_wolf_value.Text = Sea_wolfSB.Value.ToString();
        }
     
        private void Taverna_bar_Scroll(object sender, ScrollEventArgs e) => Calculation_Sailors();
        private void Calculation_Sailors()
        {

            Jung_value.Text = JungSB.Value.ToString();
            Experienced_value.Text = ExperiencedSB.Value.ToString();
            Sea_wolf_value.Text = Sea_wolfSB.Value.ToString();


            _count_new_sailors = 0;
            _count_new_sailors += JungSB.Value ;
            _count_new_sailors += ExperiencedSB.Value;
            _count_new_sailors += Sea_wolfSB.Value ;
            _sailor_price = 0;
            _sailor_price += JungSB.Value * port_Jung._price;
            _sailor_price += ExperiencedSB.Value * port_Experienced._price;
            _sailor_price += Sea_wolfSB.Value * port_Sea_wolf._price;

            new_count_sailor.Text = $"{_count_new_sailors}";

            if (_sailor_price >= 0)
                gold_sailor.Text = $"{_ship._captain._money}/ -{_sailor_price}";
            else
                gold_sailor.Text = $"{_ship._captain._money}/ +{Math.Abs(_sailor_price)}";
        }
        #endregion
        
        #region госпиталь
        private void SetSettingHospital()
        {
            helth_btn.Click += Helth_btn_Click;
            cap_hp.Text = $"{_ship._captain._current_hp}/{_ship._captain._max_hp}";
            cap_hp_bar.Minimum = 0;
            cap_hp_bar.Maximum = _ship._captain._max_hp;
            cap_hp_bar.Value = _ship._captain._current_hp;
            price_1hp.Text = $"{_price_1hp_cap} зотолых";
            scrol_hp.LargeChange = 1;
            scrol_hp.Minimum = 0;
            scrol_hp.Maximum = _ship._captain._max_hp- _ship._captain._current_hp;
            scrol_hp.Value = 0;
            scrol_hp.Scroll += Scrol_hp_Scroll;
            price_helth.Text = $"{scrol_hp.Value * _price_1hp_cap}/{_ship._captain._money}";
            helth_hp.Text = $"{scrol_hp.Value} из {scrol_hp.Maximum}";
        }

        private void Helth_btn_Click(object sender, EventArgs e)
        {
            if (scrol_hp.Value * _price_1hp_cap > _ship._captain._money)
            {
                MessageBox.Show("Маловато золотишка =(", "ПРОСТИ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ship._captain.SpendMoney(scrol_hp.Value * _price_1hp_cap);
            _ship._captain._current_hp += scrol_hp.Value;
            SetSettingHospital();
        }

        private void Scrol_hp_Scroll(object sender, ScrollEventArgs e)
        {
            price_helth.Text = $"{scrol_hp.Value * _price_1hp_cap}/{_ship._captain._money}";
            helth_hp.Text = $"{scrol_hp.Value} из {scrol_hp.Maximum}";
        }
        #endregion

        #region рынок
        public void SetSettingMarket()
        {
            _loot_capacity = 0;
            _capacity_txt.Text = $"{_ship._current_capacity + _loot_capacity}/{_ship._max_capacity}";
            money_txt.Text = $"{_ship._captain._money}/{_loot_capacity}";
            #region для вкладки рынок
            my_rum = _ship._products.Find(i => i._product_Type == Product_type.Rum);
            port_rum = _seaport._products.Find(i => i._product_Type == Product_type.Rum);
            my_Silk = _ship._products.Find(i => i._product_Type == Product_type.Silk);
            port_Silk = _seaport._products.Find(i => i._product_Type == Product_type.Silk);
            my_Water = _ship._products.Find(i => i._product_Type == Product_type.Water);
            port_Water = _seaport._products.Find(i => i._product_Type == Product_type.Water);
            my_Food = _ship._products.Find(i => i._product_Type == Product_type.Food);
            port_Food = _seaport._products.Find(i => i._product_Type == Product_type.Food);
            my_Leather = _ship._products.Find(i => i._product_Type == Product_type.Leather);
            port_Leather = _seaport._products.Find(i => i._product_Type == Product_type.Leather);
            my_Wood = _ship._products.Find(i => i._product_Type == Product_type.Wood);
            port_Wood = _seaport._products.Find(i => i._product_Type == Product_type.Wood);
            my_Tobacco = _ship._products.Find(i => i._product_Type == Product_type.Tobacco);
            port_Tobacco = _seaport._products.Find(i => i._product_Type == Product_type.Tobacco);
            my_Coffee = _ship._products.Find(i => i._product_Type == Product_type.Coffee);
            port_Coffee = _seaport._products.Find(i => i._product_Type == Product_type.Coffee);

            //Rum
            rum.Text = my_rum._count.ToString();
            Rum_value.Text = "0";
            Rum_count.Text = port_rum._count.ToString();
            Rum_bar.Maximum = port_rum._count;
            Rum_bar.Minimum = my_rum._count - (my_rum._count * 2);
            Rum_bar.Value = 0;
            rum_price.Text = $"{port_rum._price}/{port_rum._weight}";
            Rum_bar.LargeChange = 1;
            Rum_bar.Scroll += Market_bar_Scroll;
            //Silk
            shilk.Text = my_Silk._count.ToString();
            Silk_value.Text = "0";
            Silk_count.Text = port_Silk._count.ToString();
            Silk_bar.Maximum = port_Silk._count;
            Silk_bar.Minimum = my_Silk._count - (my_Silk._count * 2);
            Silk_bar.Value = 0;
            silk_price.Text = $"{port_Silk._price}/{port_Silk._weight}";
            Silk_bar.LargeChange = 1;
            Silk_bar.Scroll += Market_bar_Scroll;
            //Water
            water.Text = my_Water._count.ToString();
            Water_value.Text = "0";
            Water_count.Text = port_Water._count.ToString();
            Water_bar.Maximum = port_Water._count;
            Water_bar.Minimum = my_Water._count - (my_Water._count * 2);
            Water_bar.Value = 0;
            water_price.Text = $"{port_Water._price}/{port_Water._weight}";
            Water_bar.LargeChange = 1;
            Water_bar.Scroll += Market_bar_Scroll;
            //Food
            food.Text = my_Food._count.ToString();
            Food_value.Text = "0";
            Food_count.Text = port_Food._count.ToString();
            Food_bar.Maximum = port_Food._count;
            Food_bar.Minimum = my_Food._count - (my_Food._count * 2);
            Food_bar.Value = 0;
            food_price.Text = $"{port_Food._price}/{port_Food._weight}";
            Food_bar.LargeChange = 1;
            Food_bar.Scroll += Market_bar_Scroll;
            //Leather
            leather.Text = my_Leather._count.ToString();
            Leather_value.Text = "0";
            Leather_count.Text = port_Leather._count.ToString();
            Leather_bar.Maximum = port_Leather._count;
            Leather_bar.Minimum = my_Leather._count - (my_Leather._count * 2);
            Leather_bar.Value = 0;
            leather_price.Text = $"{port_Leather._price}/{port_Leather._weight}";
            Leather_bar.LargeChange = 1;
            Leather_bar.Scroll += Market_bar_Scroll;
            //Wood
            wood.Text = my_Wood._count.ToString();
            Wood_value.Text = "0";
            Wood_count.Text = port_Wood._count.ToString();
            Wood_bar.Maximum = port_Wood._count;
            Wood_bar.Minimum = my_Wood._count - (my_Wood._count * 2);
            Wood_bar.Value = 0;
            wood_price.Text = $"{port_Wood._price}/{port_Wood._weight}";
            Wood_bar.LargeChange = 1;
            Wood_bar.Scroll += Market_bar_Scroll;
            //Tobacco
            tobacco.Text = my_Tobacco._count.ToString();
            Tobacco_value.Text = "0";
            Tobacco_count.Text = port_Tobacco._count.ToString();
            Tobacco_bar.Maximum = port_Tobacco._count;
            Tobacco_bar.Minimum = my_Tobacco._count - (my_Tobacco._count * 2);
            Tobacco_bar.Value = 0;
            tobacco_price.Text = $"{port_Tobacco._price}/{port_Tobacco._weight}";
            Tobacco_bar.LargeChange = 1;
            Tobacco_bar.Scroll += Market_bar_Scroll;
            //Coffee
            coffee.Text = my_Coffee._count.ToString();
            Coffee_value.Text = "0";
            Coffee_count.Text = port_Coffee._count.ToString();
            Coffee_bar.Maximum = port_Coffee._count;
            Coffee_bar.Minimum = my_Coffee._count - (my_Coffee._count * 2);
            Coffee_bar.Value = 0;
            coffee_price.Text = $"{port_Coffee._price}/{port_Coffee._weight}";
            Coffee_bar.LargeChange = 1;
            Coffee_bar.Scroll += Market_bar_Scroll;
            #endregion


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
            _loot_price = 0;
            // расчет веса
            _loot_capacity += Rum_bar.Value  * my_rum._weight;
            _loot_capacity +=  Silk_bar.Value   * my_Silk._weight;
            _loot_capacity +=  Water_bar.Value  * my_Water._weight;
            _loot_capacity +=  Food_bar.Value   * my_Food._weight;
            _loot_capacity +=  Leather_bar.Value   * my_Leather._weight;
            _loot_capacity +=  Wood_bar.Value   * my_Wood._weight;
            _loot_capacity +=  Tobacco_bar.Value   * my_Tobacco._weight;
            _loot_capacity +=  Coffee_bar.Value   * my_Coffee._weight;
            // расчет стоимости
            _loot_price +=  Rum_bar.Value   * my_rum._price;
            _loot_price +=  Silk_bar.Value   * my_Silk._price;
            _loot_price +=  Water_bar.Value   * my_Water._price;
            _loot_price +=  Food_bar.Value   * my_Food._price;
            _loot_price +=  Leather_bar.Value   * my_Leather._price;
            _loot_price +=  Wood_bar.Value   * my_Wood._price;
            _loot_price +=  Tobacco_bar.Value   * my_Tobacco._price;
            _loot_price +=  Coffee_bar.Value   * my_Coffee._price;

            _capacity_txt.Text = $"{_ship._current_capacity + _loot_capacity}/{_ship._max_capacity}";
            if (_loot_price >= 0)
                money_txt.Text = $"{_ship._captain._money}/ -{_loot_price}";
            else
                money_txt.Text = $"{_ship._captain._money}/ +{Math.Abs(_loot_price)}";

        }
        private void Btn_buy_Click(object sender, EventArgs e)
        {
            if (_loot_price > _ship._captain._money)
            {
                MessageBox.Show("Маловато золотишка =(", "ПРОСТИ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_ship._current_capacity + _loot_capacity > _ship._max_capacity)
            {
                MessageBox.Show("Вам столько не влезет", "ПЕРЕГРУЗ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            _ship._current_capacity  += _loot_capacity;
            _ship._captain.SpendMoney(_loot_price);
            _ship.AddProduct(Product_type.Rum, Rum_bar.Value);
            _ship.AddProduct(Product_type.Silk, Silk_bar.Value);
            _ship.AddProduct(Product_type.Water, Water_bar.Value);
            _ship.AddProduct(Product_type.Food, Food_bar.Value);
            _ship.AddProduct(Product_type.Leather, Leather_bar.Value);
            _ship.AddProduct(Product_type.Wood, Wood_bar.Value);
            _ship.AddProduct(Product_type.Tobacco, Tobacco_bar.Value);
            _ship.AddProduct(Product_type.Coffee, Coffee_bar.Value);


            port_Coffee._count -= Coffee_bar.Value;
            port_rum._count -= Rum_bar.Value;
            port_Silk._count -= Silk_bar.Value;
            port_Water._count -= Water_bar.Value;
            port_Food._count -= Food_bar.Value;
            port_Leather._count -= Leather_bar.Value;
            port_Wood._count -= Wood_bar.Value;
            port_Tobacco._count -= Tobacco_bar.Value;
            SetSettingMarket();
        }
        private void Market_bar_Scroll(object sender, ScrollEventArgs e) => Calculation_Capacity();
        
        #endregion


    }
}
    

