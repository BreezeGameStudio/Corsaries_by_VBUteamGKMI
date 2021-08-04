using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model
{
    public partial class Island
    {        
        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public Vector2 _position;
        public Island(Microsoft.Xna.Framework.Content.ContentManager content, string name_sprit,Vector2 pos)
        {
            // выгружаем срайты острова
            _current_sprite=content.Load<Texture2D>(name_sprit);
            _position = pos;
        }
    }
}
