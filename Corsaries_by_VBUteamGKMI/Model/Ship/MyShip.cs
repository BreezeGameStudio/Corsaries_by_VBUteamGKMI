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
    
    public partial class MyShip : Ship
    {
        public const int _max_count_warning = 3;
        public int _current_count_warning = 0;
        public MyShip(Ship_type ship_Type,
            Microsoft.Xna.Framework.Content.ContentManager content,
            float x_pos,float y_pos) : base(ship_Type, content)
        {
          _position = new Vector2(x_pos , y_pos);          
            //тест капитана 
            AddSailors(Sailor_type.Sea_wolf, 40);
            AddProducts(Product_type.Food, 120);
            AddProducts(Product_type.Water, 120);
            _captain = new Captain(_sailors,300);

        }      

        // метод назначения моряков
        public void AddSailors(Sailor_type sailor_Type, int count)
        { _sailors.Find(i => i._sailor_Type == sailor_Type)._count += count; _current_count_sailors += count; }
        public void AddProducts(Product_type product_Type, int count)
        {
            var product = _products.Find(i => i._product_Type == product_Type);
            product._count += count;
            _current_capacity += product._weight * count;
        }
      

        // метод потребления еды командой
        public void Food_consumption()
        {
            var food = _products.Find(i => i._product_Type == Product_type.Food);
            var water = _products.Find(i => i._product_Type == Product_type.Water);
            var rum = _products.Find(i => i._product_Type == Product_type.Rum);
            //бежим по типам  матросов
            foreach (var sailor in _sailors)
            {
                // бежим по количеству матросов
                for (int i = 0; i < sailor._count; i++)
                {
                    // если есть вода пьем
                    if (water._count - sailor._food_consumption >= 0)
                    {
                        water._count -= sailor._food_consumption;
                        _current_capacity -= water._weight * sailor._food_consumption;
                    }
                    // если нет воды кричим все комнде что нет воды
                    else
                    {
                        _current_count_warning++;
                        throw new Exception("У нас закончилась Вода!");
                    }
                    // если есть еда кушаем
                    if (food._count - sailor._food_consumption >= 0)
                    {
                        food._count -= sailor._food_consumption;
                        _current_capacity -= food._weight* sailor._food_consumption;
                    }
                    // если нет еды кричим все комнде что нет воды
                    else
                    {
                        _current_count_warning++;
                        throw new Exception("У нас закончилась еда!");
                    }
                    // если есть и вода и еда сбрасываем количество придуприждений капитану
                    _current_count_warning = 0;
                    // 7% шанс того что матрос бухнет ром
                    if (_random.Next(100)>7)
                    {
                        if (rum._count - sailor._food_consumption >= 0)
                        {
                            rum._count -= sailor._food_consumption;
                            _current_capacity -= rum._weight* sailor._food_consumption;
                        }
                    }
                }
            }
        }

        #region методы стрельбы
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
                        Game1._my_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Left, 
                            new Vector2(
                                _position.X + (_current_sprite.Width / 2),// позиция по Х
                         _position.Y + ((_current_sprite.Height /(_count_cannon / 2))*i)// позиция по У
                         ) 
                         , _direction)); // направление

                    }
                }
                if (_direction == Direction.left || _direction == Direction.right)
                {
                    for (int i = 0; i < _count_cannon / 2; i++)
                    {
                        Game1._my_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Left, 
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
                        Game1._my_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Left,
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
                               ( _position.X + _current_sprite.Width)-((_current_sprite.Width / (_count_cannon / 2)) * i), // позиция по Х
                                  _position.Y +((_current_sprite.Height / (_count_cannon / 2)) * i)    // позиция по У
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
                        Game1._my_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Right,
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
                        Game1._my_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Right,
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
                        Game1._my_cannonballs.Add(new Cannonball(_cannon, Cannonball_side.Right,
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
                               ( _position.X + _current_sprite.Width)-((_current_sprite.Width / (_count_cannon / 2)) * i), // позиция по Х
                                  _position.Y +((_current_sprite.Height / (_count_cannon / 2)) * i)    // позиция по У
                                )
                         , _direction));// направление

                    }
                }              
                _cooldown_timer_right.Start();
                _ready_shoot_right = false;
            }
        }
        #endregion
    }
}

