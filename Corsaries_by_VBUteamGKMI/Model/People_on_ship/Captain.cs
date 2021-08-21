using System;
using System.Collections.Generic;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model.People_on_ship
{
    public class Captain
    {
        public int _money; // деньги у капитана
        public int _current_hp ;   // текущее  здоровья капитана
        public int _max_hp ;   //  здоровья капитана
        public int _damag ;  //  урона капитана
        public int _deff ;  //  защиты капитана
        public int _dodge ;  //   уворота капитана
        public int _critical ; //  шанса крит удара капитана
        public Captain(List<Sailor> sailors, int money)
        { Set_Cap_Prop(sailors);_current_hp = _max_hp; _money = money; }
        // метод для задавания параметров капитана
        public void Set_Cap_Prop(List<Sailor> sailors)
        {
           
            _max_hp = 20;   //  здоровья капитанаaw
            _damag = 5;   //  урона капитана
            _deff = 0;  //  защиты капитана
            _dodge = 0;  //   уворота капитана
            _critical = 0; //  шанса крит удара капитана
            sailors.ForEach(i => _max_hp += (i._hp_boost * i._count));// хп кэпа

            double temp_damag = 0;
            sailors.ForEach(i => temp_damag += (i._damag_boost * i._count));// урон кэпа
            _damag += (int)temp_damag;

            double temp_deff = 0;
            sailors.ForEach(i => temp_deff += (i._deff_boost * i._count));// защита кэпа
            _deff = (int)temp_deff;

            double temp_dodge = 0;
            sailors.ForEach(i => temp_dodge += (i._dodge_boost * i._count));// уворот кэпа
            _dodge = (int)temp_dodge;

            double temp_critical = 0;
            sailors.ForEach(i => temp_critical += (i._critical_boost * i._count));// крит кэпа
            _critical = (int)temp_critical;
            if(_max_hp<_current_hp)
                _current_hp = _max_hp; 

        }
        // методы денег
        public void AddMoney( int count) => _money+= count;
        public void SpendMoney( int count) => _money-= count;

        public override string ToString()
        {
            return $"{_money},{_current_hp},{_max_hp},{_damag},{_deff},{_dodge},{_critical}";
        }

        public void FromString(string data)
        {
            _money = int.Parse(data.Split(',')[0]);
            _current_hp = int.Parse(data.Split(',')[1]);
            _max_hp = int.Parse(data.Split(',')[2]);
            _damag = int.Parse(data.Split(',')[3]);
            _deff = int.Parse(data.Split(',')[4]);
            _dodge = int.Parse(data.Split(',')[5]);
            _critical = int.Parse(data.Split(',')[6]);
        }
    }
}
