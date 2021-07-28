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
        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public int _speed = 5; // скорость корабля
        public List<Texture2D> _ship_sprites = new List<Texture2D>(); // коллекция спрайтов в разные направления
        public Vector2 _position = new Vector2(0, 0);
        public MyShip(Microsoft.Xna.Framework.Content.ContentManager content)
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
        }
        #region методы перемещения
        public void Go_U() // вверх
        {
            _position.Y -= _speed;
            _current_sprite = _ship_sprites[2];
        }
        public void Go_UL() // вверх лево
        {
            _position.Y -= _speed;
            _position.X -= _speed;
            _current_sprite = _ship_sprites[4];
        }
        public void Go_UR() // вверх право
        {
            _position.Y -= _speed;
            _position.X += _speed;
            _current_sprite = _ship_sprites[5];
        }
        public void Go_D()  // вниз
        {
            _position.Y += _speed;
            _current_sprite = _ship_sprites[3];
        }
        public void Go_DL()  // вниз лево
        {
            _position.Y += _speed;
            _position.X -= _speed;
            _current_sprite = _ship_sprites[6];
        }
        public void Go_DR()  // вниз право
        {
            _position.Y += _speed;
            _position.X += _speed;
            _current_sprite = _ship_sprites[7];
        }

        public void Go_L() // в лево
        {
            _position.X -= _speed;
            _current_sprite = _ship_sprites[1];
        }
        public void Go_R() // в право
        {
            _position.X += _speed;
            _current_sprite = _ship_sprites[0];
        }
        #endregion
    }
}

