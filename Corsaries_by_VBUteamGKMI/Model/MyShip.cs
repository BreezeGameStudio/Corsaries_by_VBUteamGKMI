using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model
{
    public class MyShip
    {
       
        private System.Drawing.Size _size_screen; // размер монитора что бы не выходил за его приделы
        private List<Texture2D> _ship_sprites = new List<Texture2D>(); // коллекция спрайтов в разные направления
        private int _speed = 5; // скорость корабля

        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public Vector2 _position = new Vector2(0, 0);
        public Vector2 _old_position; // память старой позиции на случай столкновения
        public MyShip(Microsoft.Xna.Framework.Content.ContentManager content, System.Drawing.Size size_screen)
        {
            _size_screen = size_screen;
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
           
        }
        public void Step_Back_Position() => _position = _old_position;


        #region методы перемещения
        public void Go_U() // вверх
        {
            if (_position.Y > -10 )
            {
                _old_position = _position; // перезапись памяти позиции
                _position.Y -= _speed;
                _current_sprite = _ship_sprites[2];
            }
        }
        public void Go_UL() // вверх лево
        {
            if (_position.Y > -10 && _position.X > -10)
            {
                _old_position = _position;
                _position.Y -= _speed;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[4];
            }
        }
        public void Go_UR() // вверх право
        {
            if (_position.Y > -10 && _position.X < _size_screen.Width - _current_sprite.Width)
            {
                _old_position = _position;
                _position.Y -= _speed;
                _position.X += _speed;
                _current_sprite = _ship_sprites[5];
               
            }
        }
        public void Go_D()  // вниз
        {
            if ( _position.Y < _size_screen.Height - _current_sprite.Height)
            {
                _old_position = _position;
                _position.Y += _speed;
                _current_sprite = _ship_sprites[3];
            }
        }
        public void Go_DL()  // вниз лево
        {
            if ( _position.Y < _size_screen.Height - _current_sprite.Height && _position.X > -10)
            {
                _old_position = _position;
                _position.Y += _speed;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[6];
            }
        }
        public void Go_DR()  // вниз право
        {
            if ( _position.Y < _size_screen.Height - _current_sprite.Height
                && _position.X < _size_screen.Width - _current_sprite.Width)
            {
                _old_position = _position;
                _position.Y += _speed;
                _position.X += _speed;
                _current_sprite = _ship_sprites[7];
            }
        }

        public void Go_L() // в лево
        {
            if (_position.X > -10 )
            {
                _old_position = _position;
                _position.X -= _speed;
                _current_sprite = _ship_sprites[1];
            }
        }
        public void Go_R() // в право
        {
            if ( _position.X < _size_screen.Width - _current_sprite.Width)
            {
                _old_position = _position;
                _position.X += _speed;
                _current_sprite = _ship_sprites[0];
            }
        }
        #endregion
    }
}

