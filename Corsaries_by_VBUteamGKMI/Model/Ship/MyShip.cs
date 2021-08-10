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
        public Direction _direction { get; set; }
        public MyShip(Ship_type ship_Type,
            Microsoft.Xna.Framework.Content.ContentManager content,
            float x_pos,float y_pos ) : base(ship_Type)
        {
          _position = new Vector2(x_pos , y_pos);
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


            //тест капитана 
            SetSailorsList(Sailor_type.Sea_wolf, 40);
            _captain = new Captain(_sailors);
        }

        // метод назначения продуктов
        public void SetProductList(Product_type product_Type , int count) => _products.Find(i => i._product_Type == product_Type)._count = count;
        // метод назначения моряков
        public void SetSailorsList(Sailor_type sailor_Type, int count) => _sailors.Find(i => i._sailor_Type == sailor_Type)._count = count;

        public void Step_Back_Position() => _position = _old_position;


        #region методы перемещения
        public void Go_U() // вверх
        {
            if (_position.Y > Game1._game_ground._y_b)
            {
                _old_position = _position; // перезапись памяти позиции
                _position.Y -= _speed;
                _current_sprite = _ship_sprites[2];
                _direction = Direction.up;
            }
        }
        public void Go_UL() // вверх лево
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
        public void Go_UR() // вверх право
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
        public void Go_D()  // вниз
        {
            if (_position.Y < Game1._game_ground._y_e - _current_sprite.Height)
            {
                _old_position = _position;
                _position.Y += _speed;
                _current_sprite = _ship_sprites[3];
                _direction = Direction.down;
            }
        }
        public void Go_DL()  // вниз лево
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
        public void Go_DR()  // вниз право
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

        public void Go_L() // в лево
        {
            if (_position.X > Game1._game_ground._x_b)
            {
                _old_position = _position;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[1];
                _direction = Direction.left;
            }
        }
        public void Go_R() // в право
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
    }
}

