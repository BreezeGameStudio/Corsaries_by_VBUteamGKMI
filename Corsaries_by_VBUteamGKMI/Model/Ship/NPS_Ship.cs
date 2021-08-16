using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using Corsaries_by_VBUteamGKMI.Model.Products;
using Corsaries_by_VBUteamGKMI.Model.People_on_ship;

namespace Corsaries_by_VBUteamGKMI.Model.Ship
{
    public class NPS_Ship : Ship
    {
        private Random _random = new Random(); // рандом для смены направления движения
                                               // список направлений движений

        Rectangle _left_attack_zone;
        Rectangle _right_attack_zone;
        public NPS_Ship(Ship_type ship_Type, Microsoft.Xna.Framework.Content.ContentManager content) : base(ship_Type, content)
        {
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
            //создаём прямоугольник корабля 
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                 _current_sprite.Width, _current_sprite.Height);
            // // заполняем коллекцию матросов матросов =)
            SetSailorsList();
            // оживляем капитана
            _captain = new Captain(_sailors);
            // заполняем коллекцию продуктов продуктами =)
            SetProductList();
        }
        // заполняем коллекцию матросов матромаит =)
        private void SetSailorsList()
        {

            while (_current_count_sailors < _max_count_sailors)
            {
                foreach (var item in _sailors)
                {
                    item._count++;
                    _current_count_sailors++;
                    if (_current_count_sailors == _max_count_sailors)
                        break;
                }

            }
        }

        // заполняем коллекцию продуктов продуктами =)
        private void SetProductList()
        {
            // даём нпс по 30 штук каждого продукта
            _products.ForEach(i => i._count = _random.Next(30));
            // подсчитуем на сколько мы перегрузили корабыль
            _products.ForEach(i => _current_capacity += (i._count * i._weight));

            //крутим безконечный цикл пока текущая загруженость не будет меньше равно максимальной загружености
            while (_current_capacity >= _max_capacity)
            {
                //бежим по продуктам
                foreach (var item in _products)
                {
                    // проверяем достаточни ли у нас продукта
                    if (item._count > 0)
                    {
                        item._count--; // убераем одну еденицу продукта
                        _current_capacity -= item._weight; // отнимаем убраный вес
                        if (_current_capacity <= _max_capacity) // проверяем перегружены мы еще или нет
                            break; // если да конец цикла
                    }
                    else
                        continue;
                }
            }
        }

        public void Step_Back_Position() => _position = _old_position;
        public void Next_Move() => _direction = (Direction)_random.Next(7);
        public void Move()
        {                 
            switch (_direction)
            {
                case Direction.up:
                    Go_U();
                    break;
                case Direction.up_right:
                    Go_UR();
                    break;
                case Direction.right:
                    Go_R();
                    break;
                case Direction.right_down:
                    Go_DR();
                    break;
                case Direction.down:
                    Go_D();
                    break;
                case Direction.down_left:
                    Go_DL();
                    break;
                case Direction.left:
                    Go_L();
                    break;
                case Direction.left_up:
                    Go_UL();
                    break;
            }
        }
        public void Move_in_Battle(Ship enemy)
        {
            if (_ready_shoot_right)
            { 

            }
            if (_ready_shoot_left)
            {

            }
            else
                Move();

        }
        public override void Shoot_Left()
        {
            if (_ready_shoot_left)
            {
                base.Shoot_Left();
                // если корабыль смотрит в верх или в низ
                if (_direction == Direction.down || _direction == Direction.up)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._enemy_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Left,
                            new Vector2(
                                _position.X + (_current_sprite.Width / 2),// позиция по Х
                         _position.Y + ((_current_sprite.Height / (_count_cannon / 2)) * i)// позиция по У
                         )
                         , _direction)); // направление

                    }
                }
                if (_direction == Direction.left || _direction == Direction.right)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._enemy_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Left,
                            new Vector2(
                                _position.X + ((_current_sprite.Width / (_count_cannon / 2)) * i), // позиция по Х
                       _position.Y + (_current_sprite.Height / 2) // позиция по У
                       )
                         , _direction));// направление

                    }
                }
                if (_direction == Direction.left_up || _direction == Direction.right_down)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._enemy_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Left,
                            new Vector2(
                                _position.X + ((_current_sprite.Width / (_count_cannon / 2)) * i), // позиция по Х
                                  _position.Y + ((_current_sprite.Height / (_count_cannon / 2)) * i)    // позиция по У
                                )
                         , _direction));// направление

                    }
                }
                if (_direction == Direction.up_right || _direction == Direction.down_left)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._my_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Left,
                           new Vector2(
                              (_position.X + _current_sprite.Width) - ((_current_sprite.Width / (_count_cannon / 2)) * i), // позиция по Х
                                 _position.Y + ((_current_sprite.Height / (_count_cannon / 2)) * i)    // позиция по У
                               )
                        , _direction));// направление

                    }
                }



                _cooldown_timer_left.Start();
                _ready_shoot_left = false;
            }
        }
        public override void Shoot_Right()
        {
            if (_ready_shoot_right)
            {
                base.Shoot_Right();
                if (_direction == Direction.down || _direction == Direction.up)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._enemy_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Right,
                            new Vector2(
                                _position.X + (_current_sprite.Width / 2),// позиция по Х
                         _position.Y + ((_current_sprite.Height / (_count_cannon / 2)) * i)// позиция по У
                         )
                         , _direction)); // направление

                    }
                }
                if (_direction == Direction.left || _direction == Direction.right)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._enemy_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Right,
                            new Vector2(
                                _position.X + ((_current_sprite.Width / (_count_cannon / 2)) * i), // позиция по Х
                       _position.Y + (_current_sprite.Height / 2) // позиция по У
                       )
                         , _direction));// направление

                    }
                }
                if (_direction == Direction.left_up || _direction == Direction.right_down)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._enemy_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Right,
                            new Vector2(
                                _position.X + ((_current_sprite.Width / (_count_cannon / 2)) * i), // позиция по Х
                                  _position.Y + ((_current_sprite.Height / (_count_cannon / 2)) * i)    // позиция по У
                                )
                         , _direction));// направление

                    }
                }
                if (_direction == Direction.up_right || _direction == Direction.down_left)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._my_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Right,
                            new Vector2(
                               (_position.X + _current_sprite.Width) - ((_current_sprite.Width / (_count_cannon / 2)) * i), // позиция по Х
                                  _position.Y + ((_current_sprite.Height / (_count_cannon / 2)) * i)    // позиция по У
                                )
                         , _direction));// направление

                    }
                }
                _cooldown_timer_right.Start();
                _ready_shoot_right = false;
            }
        }
        #region методы перемещения
        public override void Go_U() // вверх
        {
            if (_position.Y > Game1._game_ground._y_b)
            {
                _old_position = _position; // перезапись памяти позиции
                _position.Y -= _speed;
                _current_sprite = _ship_sprites[2];
            }
            else
                Next_Move();
        }

        public override void Go_UL() // вверх лево
        {
            if (_position.Y > Game1._game_ground._y_b
                && _position.X > Game1._game_ground._x_b)
            {
                _old_position = _position;
                _position.Y -= _speed;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[4];
            }
            else
                Next_Move();
        }
        public override void Go_UR() // вверх право
        {
            if (_position.Y > Game1._game_ground._y_b
                && _position.X < Game1._game_ground._x_e - _current_sprite.Width)
            {
                _old_position = _position;
                _position.Y -= _speed;
                _position.X += _speed;
                _current_sprite = _ship_sprites[5];

            }
            else
                Next_Move();
        }
        public override void Go_D()  // вниз
        {
            if (_position.Y < Game1._game_ground._y_e - _current_sprite.Height)
            {
                _old_position = _position;
                _position.Y += _speed;
                _current_sprite = _ship_sprites[3];
            }
            else
                Next_Move();
        }
        public override void Go_DL()  // вниз лево
        {
            if (_position.Y < Game1._game_ground._y_e - _current_sprite.Height
                && _position.X > Game1._game_ground._x_b)
            {
                _old_position = _position;
                _position.Y += _speed;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[6];
            }
            else
                Next_Move();
        }
        public override void Go_DR()  // вниз право
        {
            if (_position.Y < Game1._game_ground._y_e - _current_sprite.Height
                  && _position.X < Game1._game_ground._x_e - _current_sprite.Width)
            {
                _old_position = _position;
                _position.Y += _speed;
                _position.X += _speed;
                _current_sprite = _ship_sprites[7];
            }
            else
                Next_Move();
        }
        public override void Go_L() // в лево
        {
            if (_position.X > Game1._game_ground._x_b)
            {
                _old_position = _position;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[1];
            }
            else
                Next_Move();
        }
        public override void Go_R() // в право
        {
            if (_position.X < Game1._game_ground._x_e - _current_sprite.Width)
            {
                _old_position = _position;
                _position.X += _speed;
                _current_sprite = _ship_sprites[0];
            }
            else
                Next_Move();
        }
        #endregion
    }
}
