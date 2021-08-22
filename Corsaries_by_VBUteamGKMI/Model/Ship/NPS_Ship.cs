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
      
        public NPS_Ship(Ship_type ship_Type, Microsoft.Xna.Framework.Content.ContentManager content) : base(ship_Type, content)
        {            
             // заполняем коллекцию матросов матросов =)
            SetSailorsList();
            // оживляем капитана
            switch (_ship_type)
            {
                case Ship_type.Boat: _captain = new Captain(_sailors,_random.Next(50,150));
                    break;
                case Ship_type.Schooner: _captain = new Captain(_sailors, _random.Next(100, 200));
                    break;
                case Ship_type.Caravel:_captain = new Captain(_sailors, _random.Next(200, 300));
                    break;
                case Ship_type.Brig: _captain = new Captain(_sailors, _random.Next(300, 400));
                    break;
                case Ship_type.Frigate: _captain = new Captain(_sailors, _random.Next(400, 500));
                    break;
                case Ship_type.Galleon: _captain = new Captain(_sailors, _random.Next(500, 650));
                    break;
                case Ship_type.Corvette:  _captain = new Captain(_sailors, _random.Next(600, 750));
                    break;
                case Ship_type.Battleship: _captain = new Captain(_sailors, _random.Next(700, 850));
                    break;
               
            }
            // заполняем коллекцию продуктов продуктами =)
            SetProductList();
        }       
        // заполняем коллекцию матросов матромаит =)
        public void SetSailorsList()
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
        public void SetProductList()
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
       
        public void Next_Move() => _direction = (Direction)_random.Next(7);
        public void Move_Random()
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
        // движение в сражении
        public void Move_in_Battle(Ship enemy)
        {
            if(_ready_shoot_right||_ready_shoot_left)
            {
                if (_cannon._range*_cannon._speed/2 < Distance(_position, enemy._position))
                    Move_to_Enemy(enemy);
                else
                {
                    if (_ready_shoot_right)
                    {
                        switch (_direction)
                        {
                            case Direction.up:
                                Go_L();
                                break;
                            case Direction.up_right:
                                Go_UL();
                                break;
                            case Direction.right:
                                Go_U();
                                break;
                            case Direction.right_down:
                                Go_UR();
                                break;
                            case Direction.down:
                                Go_L();
                                break;
                            case Direction.down_left:
                                Go_UR();
                                break;
                            case Direction.left:
                                Go_D();
                                break;
                            case Direction.left_up:
                                Go_DL();
                                break;
                            default: break;
                        }
                        Shoot_Right();
                    }
                    if (_ready_shoot_left)
                    {
                        switch (_direction)
                        {
                            case Direction.up:
                                Go_D();
                                break;
                            case Direction.up_right:
                                Go_UL();
                                break;
                            case Direction.right:
                                Go_L();
                                break;
                            case Direction.right_down:
                                Go_UL();
                                break;
                            case Direction.down:
                                Go_U();
                                break;
                            case Direction.down_left:
                                Go_UR();
                                break;
                            case Direction.left:
                                Go_R();
                                break;
                            case Direction.left_up:
                                Go_DR();
                                break;
                            default: break;
                        }
                        Shoot_Left();
                    }
                }

            }
            else { Move_Random(); }


        }
        // установка стартовой позиции
        public void Set_Spawn_Position(List<Island> islands)
        {
            bool collide = false;

            do
            {
                _position = new Vector2(_random.Next(600,Game1._game_ground_X_Y), _random.Next(600,Game1._game_ground_X_Y));
                _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                _current_sprite.Width, _current_sprite.Height);
                foreach (var item in islands)
                {
                    collide = item._rectangle.Intersects(_rectangle);                  
                }
                if (_position.X == 0 && _position.Y == 0)
                    collide = true;
            } while (collide);
        }

        // нахождение растояния к врагу
        private int Distance(Vector2 a,Vector2 b ) => Convert.ToInt32(Math.Sqrt(
               Math.Pow(Convert.ToDouble(a.X - b.X), 2)
             + Math.Pow(Convert.ToDouble(a.Y - b.Y), 2)));
        //движение к врагу
        private void Move_to_Enemy(Ship enemy)
        {
            if (_position.X > enemy._position.X-_speed && _position.X > enemy._position.X + _speed 
                && _position.Y > enemy._position.Y+_speed &&   _position.Y > enemy._position.Y - _speed)
            { Go_UL(); return; }
            if (_position.X < enemy._position.X-_speed && _position.X < enemy._position.X + _speed
                && _position.Y < enemy._position.Y-_speed && _position.Y < enemy._position.Y + _speed)
            { Go_DR(); return; }

            if (_position.X > enemy._position.X -_speed&& _position.X > enemy._position.X  +_speed
                && _position.Y < enemy._position.Y+_speed&& _position.Y < enemy._position.Y - _speed)
            { Go_DL(); return; }
            if (_position.X < enemy._position.X+_speed && _position.X < enemy._position.X - _speed
                && _position.Y > enemy._position.Y+_speed && _position.Y > enemy._position.Y - _speed)
            { Go_UR(); return; }
            else
            {
                if (_position.X < enemy._position.X-_speed&& _position.X < enemy._position.X + _speed)
                { Go_R(); return; }
                if (_position.X > enemy._position.X - _speed && _position.X > enemy._position.X + _speed)
                { Go_L(); return; }
                if (_position.Y < enemy._position.Y-_speed&& _position.Y < enemy._position.Y + _speed )
                { Go_D(); return; }
                if (_position.Y > enemy._position.Y - _speed && _position.Y > enemy._position.Y + _speed)
                { Go_U(); return; }
            }
           
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
                        Game1._enemy_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Left,
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
                        Game1._enemy_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Right,
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
                _direction = Direction.up;
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
                _direction = Direction.left_up;
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
                _direction = Direction.up_right;
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
                _direction = Direction.down;
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
                _direction = Direction.down_left;
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
                _direction = Direction.right_down;
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
                _direction = Direction.left;
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
                _direction = Direction.right;
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
