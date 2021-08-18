using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model
{

    public class Seaport
    {
        #region параметры острова
        public Rectangle _rectangle; // прямоугольник для корабля
        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public Vector2 _position;
        #endregion
        public int _money;// денег в порту
        public Seaport(Microsoft.Xna.Framework.Content.ContentManager content, string name_sprit, Vector2 pos)
        {
            _current_sprite=content.Load<Texture2D>(name_sprit);
            _position = pos;
            //создаём прямоугольник порта 
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                 _current_sprite.Width, _current_sprite.Height);
        }
       
    }
}
