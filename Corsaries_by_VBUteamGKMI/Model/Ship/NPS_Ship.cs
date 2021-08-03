﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model.Ship
{
   public class NPS_Ship : Ship
    {
        private Random _random = new Random(); // рандом для смены направления движения
        // список направлений движений
        private enum Eirection { up, up_right, right, right_down, down, down_left, left, left_up }
        private Eirection _eirection { get; set; }

        public NPS_Ship(Ship_type ship_Type, Microsoft.Xna.Framework.Content.ContentManager content) : base(ship_Type)
        {
            _position = new Vector2(Game1._game_ground._x_e / 2 - 100, Game1._game_ground._y_e / 2);
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
        }
        public void Step_Back_Position() => _position = _old_position;
        public void Next_Move() => _eirection = (Eirection)_random.Next(7);
        public void Move()
        {
            switch (_eirection)
            {
                case Eirection.up:
                    Go_U();
                    break;
                case Eirection.up_right:
                    Go_UR();
                    break;
                case Eirection.right:
                    Go_R();
                    break;
                case Eirection.right_down:
                    Go_DR();
                    break;
                case Eirection.down:
                    Go_D();
                    break;
                case Eirection.down_left:
                    Go_DL();
                    break;
                case Eirection.left:
                    Go_L();
                    break;
                case Eirection.left_up:
                    Go_UL();
                    break;
            }
        }
    

    #region методы перемещения
    public void Go_U() // вверх
        {
            if (_position.Y > Game1._game_ground._y_b)
            {
                _old_position = _position; // перезапись памяти позиции
                _position.Y -= _speed;
                _current_sprite = _ship_sprites[2];
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

            }
        }
        public void Go_D()  // вниз
        {
            if (_position.Y < Game1._game_ground._y_e - _current_sprite.Height)
            {
                _old_position = _position;
                _position.Y += _speed;
                _current_sprite = _ship_sprites[3];
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
            }
        }

        public void Go_L() // в лево
        {
            if (_position.X > Game1._game_ground._x_b)
            {
                _old_position = _position;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[1];
            }
        }
        public void Go_R() // в право
        {
            if (_position.X < Game1._game_ground._x_e - _current_sprite.Width)
            {
                _old_position = _position;
                _position.X += _speed;
                _current_sprite = _ship_sprites[0];
            }
        }
    #endregion
}
}