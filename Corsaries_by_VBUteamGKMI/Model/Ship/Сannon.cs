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
                            break;
                        case Cunnon_type.medium:
                            _damage = 20;
                            break;
                        case Cunnon_type.big:
                            _damage = 30;
                            break;
                    }
                    break;
                case Ship_type.Schooner:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 40;
                            break;
                        case Cunnon_type.medium:
                            _damage = 50;
                            break;
                        case Cunnon_type.big:
                            _damage = 60;
                            break;
                    }
                    break;
                case Ship_type.Caravel:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 70;
                            break;
                        case Cunnon_type.medium:
                            _damage = 80;
                            break;
                        case Cunnon_type.big:
                            _damage = 90;
                            break;
                    }
                    break;
                case Ship_type.Brig:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 100;
                            break;
                        case Cunnon_type.medium:
                            _damage = 110;
                            break;
                        case Cunnon_type.big:
                            _damage = 120;
                            break;
                    }
                    break;
                case Ship_type.Frigate:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 130;
                            break;
                        case Cunnon_type.medium:
                            _damage = 140;
                            break;
                        case Cunnon_type.big:
                            _damage = 150;
                            break;
                    }
                    break;
                case Ship_type.Galleon:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 160;
                            break;
                        case Cunnon_type.medium:
                            _damage = 170;
                            break;
                        case Cunnon_type.big:
                            _damage = 180;
                            break;
                    }
                    break;
                case Ship_type.Corvette:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 190;
                            break;
                        case Cunnon_type.medium:
                            _damage = 200;
                            break;
                        case Cunnon_type.big:
                            _damage = 210;
                            break;
                    }
                    break;
                case Ship_type.Battleship:
                    switch (cunnon_Type)
                    {
                        case Cunnon_type.small:
                            _damage = 220;
                            break;
                        case Cunnon_type.medium:
                            _damage = 230;
                            break;
                        case Cunnon_type.big:
                            _damage = 240;
                            break;
                    }
                    break;
            }
        }
       
    }
}
   