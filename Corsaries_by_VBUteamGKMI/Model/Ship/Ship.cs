using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model.Ship
{
    public enum Ship_type { Boat, Schooner, Caravel, Brig, Frigate, Galleon, Corvette, Battleship }

    public abstract class Ship
    {
        protected Ship_type _ship_type;

        #region параметры именяемые от типа корабля
        public string _name; // имя корабля
        public int  _count_sailors; // максимальное количество матросов
        public int _max_count_sailors; // максимальное количество матросов
        public int _capacity; //  вместимость
        public int _hp; // здоровье
        public int _speed; // скорость корабля
        public Сannon _cannon; // пушки корабля
        public int _count_cannon; // количество пушек
        public int _protection; // защита корабля от выстрела в процентах
        public int _dodge_chance; // шанс уворота в процентах
        #endregion
        #region параметры не именяемые от типа корабля
        protected List<Texture2D> _ship_sprites = new List<Texture2D>(); // коллекция спрайтов в разные направления
        public Rectangle _rectangle; // прямоугольник для корабля
        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public Vector2 _position = new Vector2(Game1._game_ground._x_e / 2, Game1._game_ground._y_e / 2);
        public Vector2 _old_position; // память старой позиции на случай столкновения
        #endregion
        protected Ship(Ship_type ship_Type) => Set_Ship_Type(ship_Type);

        // метода задавания типа корабля паблик так ак нужен будет в классах наследниках 
        #region 
        public void Set_Ship_Type(Ship_type ship_Type)
        {
            _ship_type = ship_Type; // задаём тип корабля
            _cannon = new Сannon(_ship_type, Cunnon_type.small); // даём ему пушки
            switch (_ship_type)
            {
                case Ship_type.Boat: // шлюшка
                    _name = "Шлюпка";
                    _capacity = 100; // всестимость
                    _hp = 500; // здоровье 
                    _speed = 1; // скорость               
                     _count_cannon = 4; // количество пушке
                    _max_count_sailors = 10; // максимальное количество матросов
                    _protection = 10;// защит от выстрела в процентах
                    _dodge_chance = 30; // шанс уворота в процентах
                    break;
                case Ship_type.Schooner:
                    _name ="Шхуна";
                    _capacity = 150; // всестимость
                    _hp = 1000; // здоровье 
                    _speed = 2; // скорость               
                    _count_cannon = 6; // количество пушке
                    _max_count_sailors = 20; // максимальное количество матросов
                     _protection = 15 ;// защит от выстрела в процентах
                    _dodge_chance = 30; // шанс уворота в процентах
                    break;
                case Ship_type.Caravel:
                    _name ="Каравелла";
                     _capacity = 200; // всестимость
                    _hp = 2000; // здоровье 
                    _speed = 3; // скорость               
                     _count_cannon = 8; // количество пушке
                    _max_count_sailors = 25; // максимальное количество матросов
                     _protection = 25 ;// защит от выстрела в процентах
                    _dodge_chance = 35; // шанс уворота в процентах
                    break;
                case Ship_type.Brig:
                    _name ="Бриг";
                     _capacity = 250; // всестимость
                    _hp = 2300; // здоровье 
                    _speed = 3; // скорость               
                     _count_cannon = 8; // количество пушке
                    _max_count_sailors = 30; // максимальное количество матросов
                     _protection =30 ;// защит от выстрела в процентах
                    _dodge_chance  =20; // шанс уворота в процентах
                    break;
                case Ship_type.Frigate:
                    _name ="Фрегат";
                     _capacity = 300; // всестимость
                    _hp = 4000; // здоровье 
                    _speed = 4; // скорость               
                     _count_cannon = 10; // количество пушке
                    _max_count_sailors = 40; // максимальное количество матросов
                     _protection = 30;// защит от выстрела в процентах
                    _dodge_chance = 20; // шанс уворота в процентах
                    break;
                case Ship_type.Galleon:
                    _name ="Галеон";
                     _capacity = 400; // всестимость
                    _hp = 6500; // здоровье 
                    _speed = 5; // скорость               
                     _count_cannon = 12; // количество пушке
                    _max_count_sailors = 50; // максимальное количество матросов
                     _protection = 35;// защит от выстрела в процентах
                    _dodge_chance = 15; // шанс уворота в процентах
                    break;
                case Ship_type.Corvette:
                    _name ="Корвет";
                     _capacity = 350; // всестимость
                    _hp = 6700; // здоровье 
                    _speed = 8; // скорость               
                     _count_cannon = 10; // количество пушке
                    _max_count_sailors = 40; // максимальное количество матросов
                     _protection = 20;// защит от выстрела в процентах
                    _dodge_chance = 40; // шанс уворота в процентах
                    break;
                case Ship_type.Battleship:
                    _name ="Линкор";
                     _capacity = 500; // всестимость
                    _hp = 10000; // здоровье 
                    _speed = 6; // скорость               
                     _count_cannon = 12; // количество пушке
                    _max_count_sailors = 70; // максимальное количество матросов
                     _protection = 60;// защит от выстрела в процентах
                    _dodge_chance= 15; // шанс уворота в процентах
                    break;
            }
        }
        #endregion
    }
}


 
          