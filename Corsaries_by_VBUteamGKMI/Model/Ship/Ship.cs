using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using Corsaries_by_VBUteamGKMI.Model.Products;
using Corsaries_by_VBUteamGKMI.Model.People_on_ship;
using Microsoft.Xna.Framework.Media;

namespace Corsaries_by_VBUteamGKMI.Model.Ship
{
    public enum Ship_type { Boat, Schooner, Caravel, Brig, Frigate, Galleon, Corvette, Battleship }
    public enum Direction { up, up_right, right, right_down, down, down_left, left, left_up }
    public abstract class Ship
    {
        public Random _random = new Random(); // рандом для смены направления движения      
        public bool _activity = true; // переменная которая отвечает за готовность 
                                      // взаимодействовать с нпс
                                      // таймер перезарядки взаимодействия с другими нпс
        public System.Windows.Forms.Timer _timer_activity = new System.Windows.Forms.Timer();
        // перезарядка активности
        private int _cooldown_activity = 500;
        private Song _hit_song;
        private Song _shoot_song;
        public bool _ready_shoot_left = true;
        public bool _ready_shoot_right = true;
        private int _cooldown = 3000;// перезарядка
        public System.Windows.Forms.Timer _cooldown_timer_left = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer _cooldown_timer_right = new System.Windows.Forms.Timer();
        public Rectangle _rectangle;
        public Direction _direction { get; set; } // направление движения
        public Captain _captain; // капитан наш любимый
        protected Ship_type _ship_type; // тип корабля
        public List<Product> _products = new List<Product>(); // колекция товаров
        public List<Sailor> _sailors = new List<Sailor>(); // колекция моряков
        #region параметры именяемые от типа корабля
        public string _name; // имя корабля
        public int _current_count_sailors = 0; // текущее количество матросов
        public int _max_count_sailors; // максимальное количество матросов
        public int _max_capacity; //  вместимость
        public int _current_capacity = 0; //  показывет сколько места на корабле занято
        public int _max_hp; // максимальное количество здоровья
        public int _current_hp;// текущее количество хп
        public float _speed; // скорость корабля
        public Cannon _cannon; // пушки корабля       
        public int _count_cannon; // количество пушек
        public int _protection; // защита корабля от выстрела в процентах
        public int _dodge_chance; // шанс уворота в процентах
        #endregion
        #region параметры не именяемые от типа корабля
        protected List<Texture2D> _ship_sprites = new List<Texture2D>(); // коллекция спрайтов в разные направления      
        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public Vector2 _position; // позицыя
        public Vector2 _old_position; // память старой позиции на случай столкновения     
        #endregion
        protected Ship(Ship_type ship_Type, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            // таймер перезарядки взаимодействия с другими нпс
            _timer_activity.Interval = _cooldown_activity;
            _timer_activity.Tick += _timer_activity_Tick;
            // выгружаем срайты корабля
            _ship_sprites.Add(content.Load<Texture2D>("ship_R"));
            _ship_sprites.Add(content.Load<Texture2D>("ship_L"));
            _ship_sprites.Add(content.Load<Texture2D>("ship_U"));
            _ship_sprites.Add(content.Load<Texture2D>("ship_D"));
            _ship_sprites.Add(content.Load<Texture2D>("ship_UL"));
            _ship_sprites.Add(content.Load<Texture2D>("ship_UR"));
            _ship_sprites.Add(content.Load<Texture2D>("ship_DL"));
            _ship_sprites.Add(content.Load<Texture2D>("ship_DR"));
            _current_sprite = _ship_sprites[0];
            // выгружаем зуки выстрелов
            _hit_song = content.Load<Song>("hit");
            _shoot_song = content.Load<Song>("shoot");
            _cooldown_timer_left.Tick += _cooldown_timer_left_Tick;
            _cooldown_timer_right.Tick += _cooldown_timer_right_Tick;
            _cooldown_timer_left.Interval = _cooldown;
            _cooldown_timer_right.Interval = _cooldown;
            Set_Ship_Type(ship_Type);
            _cannon = new Cannon(_ship_type, Cannon_type.small); // даём ему пушки
        }
       
        #region методы перемещения
        public virtual void Go_U() // вверх
        {
            if (_position.Y > Game1._game_ground._y_b)
            {
                _old_position = _position; // перезапись памяти позиции
                _position.Y -= _speed;
                _current_sprite = _ship_sprites[2];
                _direction = Direction.up;
            }
        }
        public virtual void Go_UL() // вверх лево
        {
            if (_position.Y > Game1._game_ground._y_b
                && _position.X > Game1._game_ground._x_b)
            {
                _old_position = _position;
                _position.Y -= _speed;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[4];
                _direction = Direction.left_up;
            }
        }
        public virtual void Go_UR() // вверх право
        {
            if (_position.Y > Game1._game_ground._y_b
                && _position.X < Game1._game_ground._x_e - _current_sprite.Width)
            {
                _old_position = _position;
                _position.Y -= _speed;
                _position.X += _speed;
                _current_sprite = _ship_sprites[5];
                _direction = Direction.up_right;
            }
        }
        public virtual void Go_D()  // вниз
        {
            if (_position.Y < Game1._game_ground._y_e - _current_sprite.Height)
            {
                _old_position = _position;
                _position.Y += _speed;
                _current_sprite = _ship_sprites[3];
                _direction = Direction.down;
            }
        }
        public virtual void Go_DL()  // вниз лево
        {
            if (_position.Y < Game1._game_ground._y_e - _current_sprite.Height
                && _position.X > Game1._game_ground._x_b)
            {
                _old_position = _position;
                _position.Y += _speed;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[6];
                _direction = Direction.down_left;
            }
        }
        public virtual void Go_DR()  // вниз право
        {
            if (_position.Y < Game1._game_ground._y_e - _current_sprite.Height
                  && _position.X < Game1._game_ground._x_e - _current_sprite.Width)
            {
                _old_position = _position;
                _position.Y += _speed;
                _position.X += _speed;
                _current_sprite = _ship_sprites[7];
                _direction = Direction.right_down;
            }
        }
        public virtual void Go_L() // в лево
        {
            if (_position.X > Game1._game_ground._x_b)
            {
                _old_position = _position;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[1];
                _direction = Direction.left;
            }
        }
        public virtual void Go_R() // в право
        {
            if (_position.X < Game1._game_ground._x_e - _current_sprite.Width)
            {
                _old_position = _position;
                _position.X += _speed;
                _current_sprite = _ship_sprites[0];
                _direction = Direction.right;
            }
        }
        #endregion
        // метода задавания типа корабля паблик так ак нужен будет в классах наследниках 
        public void Set_Ship_Type(Ship_type ship_Type)
        {
            _ship_type = ship_Type; // задаём тип корабля                   
            switch (_ship_type)
            {
                case Ship_type.Boat: // шлюшка
                    _name = "Шлюпка";
                    _max_capacity = 350; // всестимость
                    _max_hp = 500; // максимальное количество здоровья 
                    _speed = 0.25f; // скорость               
                    _count_cannon = 4; // количество пушке
                    _max_count_sailors = 10; // максимальное количество матросов
                    _protection = 10;// защит от выстрела в процентах
                    _dodge_chance = 30; // шанс уворота в процентах
                    break;
                case Ship_type.Schooner:
                    _name = "Шхуна";
                    _max_capacity = 650; // всестимость
                    _max_hp = 1000; // максимальное количество здоровья 
                    _speed = 0.5f; // скорость               
                    _count_cannon = 6; // количество пушке
                    _max_count_sailors = 20; // максимальное количество матросов
                    _protection = 15;// защит от выстрела в процентах
                    _dodge_chance = 30; // шанс уворота в процентах
                    break;
                case Ship_type.Caravel:
                    _name = "Каравелла";
                    _max_capacity = 800; // всестимость
                    _max_hp = 2000; // максимальное количество здоровья 
                    _speed = 0.75f; // скорость               
                    _count_cannon = 8; // количество пушке
                    _max_count_sailors = 25; // максимальное количество матросов
                    _protection = 25;// защит от выстрела в процентах
                    _dodge_chance = 35; // шанс уворота в процентах
                    break;
                case Ship_type.Brig:
                    _name = "Бриг";
                    _max_capacity = 950; // всестимость
                    _max_hp = 2300; // максимальное количество здоровья 
                    _speed = 0.75f; // скорость               
                    _count_cannon = 8; // количество пушке
                    _max_count_sailors = 30; // максимальное количество матросов
                    _protection = 30;// защит от выстрела в процентах
                    _dodge_chance = 20; // шанс уворота в процентах
                    break;
                case Ship_type.Frigate:
                    _name = "Фрегат";
                    _max_capacity = 1250; // всестимость
                    _max_hp = 4000; // максимальное количество здоровья 
                    _speed = 1f; // скорость               
                    _count_cannon = 10; // количество пушке
                    _max_count_sailors = 40; // максимальное количество матросов
                    _protection = 30;// защит от выстрела в процентах
                    _dodge_chance = 20; // шанс уворота в процентах
                    break;
                case Ship_type.Galleon:
                    _name = "Галеон";
                    _max_capacity = 1550; // всестимость
                    _max_hp = 6500; // максимальное количество здоровья 
                    _speed = 1.25f; // скорость               
                    _count_cannon = 12; // количество пушке
                    _max_count_sailors = 50; // максимальное количество матросов
                    _protection = 35;// защит от выстрела в процентах
                    _dodge_chance = 15; // шанс уворота в процентах
                    break;
                case Ship_type.Corvette:
                    _name = "Корвет";
                    _max_capacity = 1250; // всестимость
                    _max_hp = 6700; // максимальное количество здоровья 
                    _speed = 2f; // скорость               
                    _count_cannon = 10; // количество пушке
                    _max_count_sailors = 40; // максимальное количество матросов
                    _protection = 20;// защит от выстрела в процентах
                    _dodge_chance = 40; // шанс уворота в процентах
                    break;
                case Ship_type.Battleship:
                    _name = "Линкор";
                    _max_capacity = 2200; // всестимость
                    _max_hp = 10000; // максимальное количество здоровья 
                    _speed = 1.5f; // скорость               
                    _count_cannon = 12; // количество пушке
                    _max_count_sailors = 70; // максимальное количество матросов
                    _protection = 60;// защит от выстрела в процентах
                    _dodge_chance = 15; // шанс уворота в процентах
                    break;
            }
            _current_hp = _max_hp; // присваиваем макс хп к текущему хп
           
            // инициализируем в нашей колекции места пот продукты
            for (int i = 0; i < 8; i++)
                _products.Add(new Product((Product_type)i));
            // инициализируем в нашей колекции матросов
            for (int i = 0; i < 3; i++)
                _sailors.Add(new Sailor((Sailor_type)i));
          
        }
        // иент перезарядки взаимодействия с другими нпс
        private void _timer_activity_Tick(object sender, EventArgs e)
        {
            _activity = true;
            _timer_activity.Stop();
        }
        // перезарядка с права
        private void _cooldown_timer_right_Tick(object sender, EventArgs e)
        {
            _ready_shoot_right = true;
            _cooldown_timer_right.Stop();
        }
        // перезарядка с лева
        private void _cooldown_timer_left_Tick(object sender, EventArgs e)
        {
            _ready_shoot_left = true;
            _cooldown_timer_left.Stop();
        }
        // выстре с лева
        public virtual void Shoot_Left() 
        {
            for (int i = 0; i < _count_cannon / 2; i++)
            { MediaPlayer.Play(_shoot_song); }
        }
        // выстрел с права
        public virtual void Shoot_Right() 
        {
            for (int i = 0; i < _count_cannon / 2; i++)
            { MediaPlayer.Play(_shoot_song); }
        }
        // получение урона
        public void GetDamaged(Cannon cannon)
        {                    
            // проверка на уворот
            if (_random.Next(100) > _dodge_chance)
            {
                int current_damag = cannon._damage;
                int protected_damag;
                // проверка на блокировку
                if (_random.Next(100) < _protection)
                {
                    MediaPlayer.Play(_hit_song);
                    protected_damag = (cannon._damage / 100) * _protection;
                    _current_hp -= (current_damag - protected_damag);
                }
                else { _current_hp -= current_damag; MediaPlayer.Play(_hit_song); } 
                // шанс попадания по матросам
                if(_random.Next(100)<30)
                {
                    foreach (var sailor in _sailors)
                    {
                        for (int i = 0; i < sailor._count; i++)
                        {
                            if(_random.Next(100)<10)
                            {
                                if (sailor._count > 0 && _current_count_sailors > 0)
                                {
                                    sailor._count--;
                                    _current_count_sailors--;
                                }
                            }
                        }
                    }
                }
            }
            else { }
            
        }
        // метод добавления продуктов
        public void AddProduct(Product_type product_Type, int count) => _products.Find(i => i._product_Type == product_Type)._count += count;
        // шаг назад при столкновении
        public void Step_Back_Position() => _position = _old_position;
    }

}


 
          