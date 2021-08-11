using System;
using System.Collections.Generic;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model.Ship
{
    public enum Cunnon_type { small, medium, big }
    public class Сannon
    {
       public Cunnon_type _cunnon_type; // тип пушки 
        public int _damage; // урон пушки
        public int _speed; // скорость снаряда
        public int _range ;// дальность полёта
        public Сannon(Ship_type ship_Type, Cunnon_type cunnon_Type) => Set_Cunnon_Type(ship_Type, cunnon_Type);
        public void Set_Cunnon_Type(Ship_type ship_Type, Cunnon_type cunnon_Type)
        {
            _cunnon_type = cunnon_Type;
            switch (ship_Type)
            {
                case Ship_type.Boat:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 10;
                            _speed = 4;
                            _range = 20;

                            break;
                        case Cunnon_type.medium:
                            _damage = 20;
                            _speed = 4;
                            _range = 25;

                            break;
                        case Cunnon_type.big:
                            _damage = 30;
                            _speed = 4;
                            _range = 25;

                            break;
                    }
                    break;
                case Ship_type.Schooner:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 40;
                            _speed = 4;
                            _range = 25;

                            break;
                        case Cunnon_type.medium:
                            _damage = 50;
                            _speed = 5;
                            _range = 25;

                            break;
                        case Cunnon_type.big:
                            _damage = 60;
                            _speed = 5;
                            _range = 25;

                            break;
                    }
                    break;
                case Ship_type.Caravel:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 70;
                            _speed = 5;
                            _range = 27;

                            break;
                        case Cunnon_type.medium:
                            _damage = 80;
                            _speed = 5;
                            _range = 27;

                            break;
                        case Cunnon_type.big:
                            _damage = 90;
                            _speed = 5;
                            _range = 27;

                            break;
                    }
                    break;
                case Ship_type.Brig:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 100;
                            _speed = 6;
                            _range = 30;

                            break;
                        case Cunnon_type.medium:
                            _damage = 110;
                            _speed = 6;
                            _range = 30;

                            break;
                        case Cunnon_type.big:
                            _damage = 120;
                            _speed = 6;
                            _range = 30;

                            break;
                    }
                    break;
                case Ship_type.Frigate:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 130;
                            _speed = 6;
                            _range = 30;

                            break;
                        case Cunnon_type.medium:
                            _damage = 140;
                            _speed = 6;
                            _range = 30;

                            break;
                        case Cunnon_type.big:
                            _damage = 150;
                            _speed = 6;
                            _range = 30;

                            break;
                    }
                    break;
                case Ship_type.Galleon:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 160;
                            _speed = 7;
                            _range = 35;

                            break;
                        case Cunnon_type.medium:
                            _damage = 170;
                            _speed = 7;
                            _range = 35;

                            break;
                        case Cunnon_type.big:
                            _damage = 180;
                            _speed = 7;
                            _range = 35;

                            break;
                    }
                    break;
                case Ship_type.Corvette:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 190;
                            _speed = 7;
                            _range = 35;

                            break;
                        case Cunnon_type.medium:
                            _damage = 200;
                            _speed = 7;
                            _range = 35;

                            break;
                        case Cunnon_type.big:
                            _damage = 210;
                            _speed = 7;
                            _range = 35;

                            break;
                    }
                    break;
                case Ship_type.Battleship:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 220;
                            _speed = 8;
                            _range = 40;

                            break;
                        case Cunnon_type.medium:
                            _damage = 230;
                            _speed = 8;
                            _range = 40;

                            break;
                        case Cunnon_type.big:
                            _damage = 240;
                            _speed = 8;
                            _range = 40;

                            break;
                    }
                    break;
            }
        }
       
    }
}
   