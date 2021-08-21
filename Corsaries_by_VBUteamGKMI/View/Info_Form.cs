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
    public partial class Info_Form : Form
    {
        public Info_Form(Ship ship)
        {
            InitializeComponent();
            Initalize_Ship(ship);
            Initalize_Capitan(ship);
            Initalize_Hold(ship);
        }
        private void Initalize_Ship(Ship ship)
        {
            _ship_name.Text = ship._name;
            _ship_CANNON_RANGE.Text = ship._cannon._range.ToString();
            _ship_CANNON_SPEED.Text = ship._cannon._speed.ToString();
            _ship_DMG.Text = ship._cannon._damage.ToString();
            _ship_cannon_count.Text = ship._count_cannon.ToString();
            _ship_CANNON_TYPE.Text = ship._cannon._name;
            _ship_DODGE.Text = ship._dodge_chance.ToString();
            _ship_DEF.Text = ship._protection.ToString();
            _ship_SPEED.Text = ship._speed.ToString();
            _ship_MATROS_BAR.Maximum = ship._max_count_sailors;
            _ship_MATROS_BAR.Value = ship._current_count_sailors;            
            _ship_MATROS_name1.Text = ship._sailors[0]._name;
            _ship_MATROS_name2.Text = ship._sailors[1]._name;
            _ship_MATROS_name3.Text = ship._sailors[2]._name;
            _ship_MATROS_name1_count.Text = ship._sailors[0]._count.ToString();
            _ship_MATROS_name2_count.Text = ship._sailors[1]._count.ToString(); 
            _ship_MATROS_name3_count.Text = ship._sailors[2]._count.ToString();
            _ship_MATROS_count.Text = ship._current_count_sailors.ToString();
            _ship_HP_BAR.Maximum = ship._max_hp;
            _ship_HP_BAR.Value = ship._current_hp;
            _ship_HP.Text = $"{ship._current_hp}/{ship._max_hp}";
        }
        private void Initalize_Capitan(Ship ship)
        {
            _capitan_HP_BAR.Maximum = ship._captain._max_hp;
            _capitan_HP_BAR.Value = ship._captain._current_hp;
            _capitan_CRIT.Text = ship._captain._critical.ToString();
            _capitan_DODGE.Text = ship._captain._dodge.ToString();
            _capitan_DEF.Text = ship._captain._deff.ToString();
            _capitan_DMG.Text = ship._captain._damag.ToString();
            _capitan_HP.Text = ship._captain._current_hp.ToString();
            _capitan_money.Text = ship._captain._money.ToString();

        }
       
        private void Initalize_Hold(Ship ship)
        {
            _hold_rum.Text = ship._products[0]._count.ToString();
            _hold_silk.Text = ship._products[1]._count.ToString();
            _hold_woter.Text = ship._products[2]._count.ToString();
            _hold_food.Text = ship._products[3]._count.ToString();
            _hold_wood.Text = ship._products[4]._count.ToString();
            _hold_leazer.Text = ship._products[5]._count.ToString();
            _hold_tobaco.Text = ship._products[6]._count.ToString();
            _hold_koffee.Text = ship._products[7]._count.ToString();
            _hold_capasity.Text =$"{ship._current_capacity}/{ship._max_capacity}" ;
            _hold_BAR.Maximum = ship._max_capacity;
            _hold_BAR.Value = ship._current_capacity;

        }
    }
}
