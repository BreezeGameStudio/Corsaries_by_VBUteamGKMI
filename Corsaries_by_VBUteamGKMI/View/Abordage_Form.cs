using Corsaries_by_VBUteamGKMI.Model.Ship;
using System;
using System.Windows.Forms;
using Corsaries_by_VBUteamGKMI.Model.People_on_ship;

namespace Corsaries_by_VBUteamGKMI.View
{
    public partial class Abordage_Form : Form
    {
        private int count_attack = 1;
        private Random rdn = new Random();
        public Captain _My_Capitan { get; }
        public Captain _Enemy_Capitan { get; }
        Ship _My_Ship;
        public Abordage_Form(Ship My_Ship, Ship Enemy_Ship)
        {
            InitializeComponent();

            _My_Ship = My_Ship;


            _My_Capitan = My_Ship._captain;
            _My_Capitan.Set_Cap_Prop(My_Ship._sailors);
            My_Hp_bar.Maximum = _My_Capitan._max_hp;
            My_Hp_bar.Minimum = 0;
            My_Hp_bar.Value = _My_Capitan._current_hp;


            _Enemy_Capitan = Enemy_Ship._captain;
            _Enemy_Capitan.Set_Cap_Prop(Enemy_Ship._sailors);
            Enemy_HP_bar.Maximum = _Enemy_Capitan._max_hp;
            Enemy_HP_bar.Minimum = 0;
            Enemy_HP_bar.Value = _Enemy_Capitan._current_hp;

            cb_attack.SelectedIndex = 0;
            cb_deff.SelectedIndex = 0;
            cb_deff2.SelectedIndex = 0;
            btn_Attack.Click += Btn_Attack_Click;
            Initialize_TXT();
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
            if (rdn.Next(100) > _Enemy_Capitan._dodge)
            {
                int final_damag;
                int protected_damag = 0;
                int crit_damag = 0;

                //проверка на критический удар
                if (rdn.Next(100) > _My_Capitan._critical)
                    crit_damag = _My_Capitan._damag + (_My_Capitan._damag / 100 * 30);

                // проверка на блокировку
                if (cb_attack.SelectedIndex == rdn.Next(4) ||
                    cb_attack.SelectedIndex == rdn.Next(4))
                    protected_damag = _My_Capitan._damag / 100 * _Enemy_Capitan._deff;

                final_damag = _My_Capitan._damag + crit_damag - protected_damag;
                log.Text += $"Наш Капитан нанес {final_damag} урона\r\n";
                log.Text += $"Базовый урон: {_My_Capitan._damag}\r\n";
                log.Text += $" + Критический урон: {crit_damag}\r\n";
                log.Text += $" - заблокированый урон урон: {protected_damag}\r\n";
                _Enemy_Capitan._current_hp -= final_damag;
            }
            else
                log.Text += $"Наш Враг УВЕРНУЛСЯ!\r\n";

            if (IsEndBattle())
                    return true;

                log.Text += "\r\n\r\n";

            // атака на нас
            // проверка на уклонение
            if (rdn.Next(100) > _My_Capitan._dodge)
            {
                int final_damag;
                int protected_damag = 0;
                int crit_damag = 0;

                //проверка на критический удар
                if (rdn.Next(100) > _Enemy_Capitan._critical)
                    crit_damag = _Enemy_Capitan._damag + (_Enemy_Capitan._damag / 100 * 30);

                // проверка на блокировку
                int attac_rand = rdn.Next(4);
                if (cb_deff.SelectedIndex == attac_rand ||
                    cb_deff2.SelectedIndex == attac_rand)
                    protected_damag = _Enemy_Capitan._damag / 100 * _My_Capitan._deff;

                final_damag = _Enemy_Capitan._damag + crit_damag - protected_damag;
                log.Text += $"Вражеский Капитан нанес {final_damag} урона\r\n";
                log.Text += $"Базовый урон: {_Enemy_Capitan._damag}\r\n";
                log.Text += $" + Критический урон: {crit_damag}\r\n";
                log.Text += $" - заблокированый урон урон: {protected_damag}\r\n";
                _My_Capitan._current_hp -= final_damag;
            }
            else
                log.Text += $"Наш Капитан УВЕРНУЛСЯ!\r\n";
            count_attack++;
            if (IsEndBattle())
                return true;
            else
                return false;


        }
        // проверка на конец боя
        public bool IsEndBattle()
        {

            if (_Enemy_Capitan._current_hp <= 0)
                return true;

            else
                Initialize_TXT();

            if (_My_Capitan._current_hp <= 0)
                return true;

            else
                Initialize_TXT();
            return false;
        }
        private void Initialize_TXT()
        {
            try
            {
                // my 
                My_HP.Text = _My_Capitan._current_hp.ToString();
                this.My_Hp_bar.Value = _My_Capitan._current_hp;
                My_damag.Text = _My_Capitan._damag.ToString();
                My_deff.Text = _My_Capitan._deff.ToString();
                My_dodge.Text = _My_Capitan._dodge.ToString();
                My_creed.Text = _My_Capitan._critical.ToString();

                // Enemy
                Enemy_HP.Text = _Enemy_Capitan._current_hp.ToString();
                this.Enemy_HP_bar.Value = _Enemy_Capitan._current_hp;
                Enemy_dmg.Text = _Enemy_Capitan._damag.ToString();
                Enemy_deff.Text = _Enemy_Capitan._deff.ToString();
                Enemy_dodge.Text = _Enemy_Capitan._dodge.ToString();
                Enemy_creed.Text = _Enemy_Capitan._critical.ToString();
            }
            catch (Exception ) 
            { this.Close(); }

        }

       
    }
}
