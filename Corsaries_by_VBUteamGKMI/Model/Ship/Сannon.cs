using System;
using System.Collections.Generic;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model.Ship
{
    public enum Cannon_type { small, medium, big }
    public class Cannon
    {
        public string _name;// имя пушки
       public Cannon_type _cunnon_type; // тип пушки 
        public int _damage; // урон пушки
        public int _price; // цена пушки
        public int _speed; // скорость снаряда
        public int _range ;// дальность полёта
        public Cannon(Ship_type ship_Type, Cannon_type cunnon_Type) => Set_Cunnon_Type(ship_Type, cunnon_Type);
        public void Set_Cunnon_Type(Ship_type ship_Type, Cannon_type cunnon_Type)
        {
            _cunnon_type = cunnon_Type;
            switch (_cunnon_type)
            {
                case Cannon_type.small: _name = "Маленькие"; break;
                case Cannon_type.medium: _name = "Средние"; break;
                case Cannon_type.big:  _name = "Большие"; break;
                default: break;
            }         
            switch (ship_Type)
            {
                case Ship_type.Boat:
                    switch (_cunnon_type)
                    {
                        case Cannon_type.small:
                            _price = 1000;
                            _damage = 10;
                            _speed = 4;
                            _range = 20;

                            break;
                        case Cannon_type.medium:                          
                            _damage = 20;
                            _price = 3000;
                            _speed = 4;
                            _range = 25;

                            break;
                        case Cannon_type.big:
                            _price = 5000;
                            _damage = 30;
                            _speed = 4;
                            _range = 25;

                            break;
                    }
                    break;
                case Ship_type.Schooner:
                    switch (cunnon_Type)
                    {
                        case Cannon_type.small:
                            _price = 5000;
                            _damage = 40;
                            _speed = 4;
                            _range = 25;

                            break;
                        case Cannon_type.medium:
                            _price = 7000;
                            _damage = 50;
                            _speed = 5;
                            _range = 25;

                            break;
                        case Cannon_type.big:
                            _price = 10000;
                            _damage = 60;
                            _speed = 5;
                            _range = 25;

                            break;
                    }
                    break;
                case Ship_type.Caravel:
                    switch (cunnon_Type)
                    {
                        case Cannon_type.small:
                            _price = 10000;
                            _damage = 70;
                            _speed = 5;
                            _range = 27;

                            break;
                        case Cannon_type.medium:
                            _price = 13000;
                            _damage = 80;
                            _speed = 5;
                            _range = 27;

                            break;
                        case Cannon_type.big:
                            _price = 15000;
                            _damage = 90;
                            _speed = 5;
                            _range = 27;

                            break;
                    }
                    break;
                case Ship_type.Brig:
                    switch (cunnon_Type)
                    {
                        case Cannon_type.small:
                            _price = 15000;
                            _damage = 100;
                            _speed = 6;
                            _range = 30;

                            break;
                        case Cannon_type.medium:
                            _price = 20000;
                            _damage = 110;
                            _speed = 6;
                            _range = 30;

                            break;
                        case Cannon_type.big:
                            _price = 25000;
                            _damage = 120;
                            _speed = 6;
                            _range = 30;

                            break;
                    }
                    break;
                case Ship_type.Frigate:
                    switch (cunnon_Type)
                    {
                        case Cannon_type.small:
                            _price = 25000;
                            _damage = 130;
                            _speed = 6;
                            _range = 30;

                            break;
                        case Cannon_type.medium:
                            _price = 35000;
                            _damage = 140;
                            _speed = 6;
                            _range = 30;

                            break;
                        case Cannon_type.big:
                            _price = 40000;
                            _damage = 150;
                            _speed = 6;
                            _range = 30;

                            break;
                    }
                    break;
                case Ship_type.Galleon:
                    switch (cunnon_Type)
                    {
                        case Cannon_type.small:
                            _price = 40000;
                            _damage = 160;
                            _speed = 7;
                            _range = 35;

                            break;
                        case Cannon_type.medium:
                            _price = 55000;
                            _damage = 170;
                            _speed = 7;
                            _range = 35;

                            break;
                        case Cannon_type.big:
                            _price = 65000;
                            _damage = 180;
                            _speed = 7;
                            _range = 35;

                            break;
                    }
                    break;
                case Ship_type.Corvette:
                    switch (cunnon_Type)
                    {
                        case Cannon_type.small:
                            _price = 65000;
                            _damage = 190;
                            _speed = 7;
                            _range = 35;

                            break;
                        case Cannon_type.medium:
                            _price = 85000;
                            _damage = 200;
                            _speed = 7;
                            _range = 35;

                            break;
                        case Cannon_type.big:
                            _price = 100000;
                            _damage = 210;
                            _speed = 7;
                            _range = 35;

                            break;
                    }
                    break;
                case Ship_type.Battleship:
                    switch (cunnon_Type)
                    {
                        case Cannon_type.small:
                            _price = 100000;
                            _damage = 220;
                            _speed = 8;
                            _range = 40;

                            break;
                        case Cannon_type.medium:
                            _price = 150000;
                            _damage = 230;
                            _speed = 8;
                            _range = 40;

                            break;
                        case Cannon_type.big:
                            _price = 250000;
                            _damage = 240;
                            _speed = 8;
                            _range = 40;

                            break;
                    }
                    break;
            }
        }

        public override string ToString()
        {
            return $"{_name},{_cunnon_type},{_damage},{_price},{_speed},{_range}";
        }

        public void FromString(string data)
        {
            _name = data.Split(',')[0];
            _cunnon_type = (Cannon_type)Enum.Parse(typeof(Cannon_type),data.Split(',')[1]);
            _damage = int.Parse(data.Split(',')[2]);
            _price = int.Parse(data.Split(',')[3]);
            _speed = int.Parse(data.Split(',')[4]);
            _range = int.Parse(data.Split(',')[5]);
        }
    }
}
   