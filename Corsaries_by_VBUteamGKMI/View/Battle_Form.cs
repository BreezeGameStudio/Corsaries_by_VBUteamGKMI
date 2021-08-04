﻿using Corsaries_by_VBUteamGKMI.Model.Ship;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Corsaries_by_VBUteamGKMI.View
{
    public partial class Battle_Form : Form
    {
        private int count_attack = 1;
        private Random rdn = new Random();
        public MyShip _MyShip { get; }
        public NPS_Ship _Enemy_Ship { get; }
        public Battle_Form(MyShip myShip, NPS_Ship Enemy_Ship)
        {
            InitializeComponent();
            my_Ship_hp_bar.Maximum = myShip._max_hp;
            my_Ship_hp_bar.Minimum = 0;
            _MyShip = myShip;
           
            _Enemy_Ship = Enemy_Ship;
            Enemy_HP_bar.Maximum = Enemy_Ship._max_hp;
            Enemy_HP_bar.Minimum = 0;
            Initialize_TXT();
            cb_attack.SelectedIndex = 0;
            cb_deff.SelectedIndex = 0;

            btn_Attack.Click += Btn_Attack_Click;
            btn_abbord.Click += Btn_abbord_Click;
        }

        private void Btn_abbord_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                 new Abordage_Form(_MyShip._captain, _Enemy_Ship._captain).ShowDialog(); 
               
                if (_MyShip._captain._current_hp > 0)
                {
                    MessageBox.Show($"Это ПОБЕДА!", "Открывай ром!!!", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.Yes;
                    //тут будет вопрос грабешки
                    Game1._nps.Remove(_Enemy_Ship);
                }


                if (_MyShip._captain._current_hp <= 0)
                {
                    MessageBox.Show($"Нас РАЗМОТАЛИ =(", "Спасайся!!!", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.No;

                }
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private void Btn_Attack_Click(object sender, EventArgs e)
        {
            if (Attack())
                this.Close();
        }
       
        // метод аттаки возвращает true если бой закончен false если бой не закончен
        public bool Attack()
        {
            log.Text += $"Аттака № {count_attack}\r\n";
            // атака на врага
            // проверка на уклонение
            if (rdn.Next(100) > _Enemy_Ship._dodge_chance)
            {
                int current_damag = _MyShip._count_cannon * _MyShip._cannon._damage;
                int protected_damag = 0;
                // проверка на блокировку
                if (cb_attack.SelectedIndex == rdn.Next(4))
                {
                    protected_damag = (_MyShip._count_cannon * _MyShip._cannon._damage) / 100 * _Enemy_Ship._protection;
                    _Enemy_Ship._current_hp -= (current_damag - protected_damag);
                    log.Text += $"Наш корабль {_MyShip._name} нанес : {current_damag} урона\r\n";
                    log.Text += $"Корабль врвгв {_Enemy_Ship._name} заблокировал : {protected_damag} урона\r\n";
                }
                else
                {
                    _Enemy_Ship._current_hp -= current_damag;
                    log.Text += $"Наш корабль {_MyShip._name} нанес : {current_damag} урона\r\n";
                    log.Text += $"Корабль врвгв {_Enemy_Ship._name} заблокировал : {protected_damag} урона\r\n";
                }
            }
            else
            { log.Text += $"Корабль врaгв {_Enemy_Ship._name} увернулся от  аттаки\r\n"; }

            if (IsEndBattle())
                return true;


            //атака на нас
            // проверка на уклонение
            if (rdn.Next(100) > _MyShip._dodge_chance)
            {
                int current_damag = _Enemy_Ship._count_cannon * _Enemy_Ship._cannon._damage;
                int protected_damag = 0;
                // проверка на блокировку
                if (cb_deff.SelectedIndex == rdn.Next(4))
                {
                    protected_damag = (_Enemy_Ship._count_cannon * _Enemy_Ship._cannon._damage) / 100 * _MyShip._protection;
                    _MyShip._current_hp -= (current_damag - protected_damag);
                    log.Text += $"Корабль врага {_Enemy_Ship._name} нанес : {current_damag} урона\r\n";
                    log.Text += $"Наш корабь {_MyShip._name} заблокировал : {protected_damag} урона\r\n";
                }
                else
                {
                    _MyShip._current_hp -= current_damag;
                    log.Text += $"Корабль врага {_Enemy_Ship._name} нанес : {current_damag} урона\r\n";
                    log.Text += $"Наш корабль {_MyShip._name} заблокировал : {protected_damag} урона\r\n";
                }
            }
            else
            { log.Text += $"Наш корабль {_MyShip._name} увернулся от  аттаки\r\n"; }
            log.Text += "\r\n\r\n";
            count_attack++;
            if (IsEndBattle())
                return true;
            else
                return false;


        }
        // проверка на конец боя
        public bool IsEndBattle()
        {
            
            if (_Enemy_Ship._current_hp <= 0)
            {
                Game1._nps.Remove(_Enemy_Ship);
                MessageBox.Show($"Это ПОБЕДА над {_Enemy_Ship._name}", "Открывай ром!!!", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.Yes;
                return true;
            }
            else
                Initialize_TXT();

            if (_MyShip._current_hp <= 0)
            {
                MessageBox.Show($"Нас РАЗГРОМИЛ {_Enemy_Ship._name}", "Спасайся!!!", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.No;
                return true;
            }
            else
                Initialize_TXT();
            return false;
        }
        private void Initialize_TXT()
        {
            try
            {
                // my ship
                my_Ship_name.Text = _MyShip._name;
                my_Ship_cannon_name.Text = _MyShip._cannon._cunnon_type.ToString();
                my_Ship_count_cannon.Text = _MyShip._count_cannon.ToString();
                my_Ship_damag.Text =  _MyShip._cannon._damage.ToString();
                my_Ship_dodge.Text = _MyShip._dodge_chance.ToString();
                my_Ship_HP.Text = _MyShip._current_hp.ToString();
                my_Ship_protection.Text = _MyShip._protection.ToString() ;
                my_Ship_hp_bar.Value = _MyShip._current_hp;

                // Enemy
                Enemy_Name.Text = _Enemy_Ship._name;
                Enemy_count_cannon.Text = _Enemy_Ship._count_cannon.ToString();
                Enemy_damag.Text = _Enemy_Ship._cannon._damage.ToString();
                Enemy_Dodge.Text = _Enemy_Ship._dodge_chance.ToString();
                Enemy_HP.Text = _Enemy_Ship._current_hp.ToString();
                Enemy_proyected.Text = _Enemy_Ship._protection.ToString() ;
                Enemy_HP_bar.Value = _Enemy_Ship._current_hp;
                Enemy_cannon_type.Text = _Enemy_Ship._cannon._cunnon_type.ToString();
            }
            catch (Exception) { this.Close(); }
           
           

        }
      

      
    }
}
