using Corsaries_by_VBUteamGKMI.Model;
using Corsaries_by_VBUteamGKMI.Model.Ship;
using Corsaries_by_VBUteamGKMI.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Corsaries_by_VBUteamGKMI
{
    public partial class HP_Bar
    {
        Ship _ship;
        public Vector2 _position;
        GraphicsDevice _graphicsDevice;
        public Ship _current_ship;
        public int _maxHP;
        public int _currentHP ;
        // рамка
        public Vector2 _border_position;  
        public Texture2D _border_texture;
        public int _border_width;
        public int _border_heght;
        public Color _border_color = Color.Black;
        // HP     
        public Vector2 _hp_position;  
        public Texture2D _hp_texture;
        public int _hp_width;
        public int _hp_heght;
        public Color _hp_color;
        public HP_Bar(GraphicsDevice graphicsDevice,Ship ship,Color hp_color)
        {
            _ship = ship;
            _position = new Vector2(_ship._position.X - 30, _ship._position.Y - 30);
            _hp_color = hp_color;
            _border_width = ship._current_sprite.Width*2+10;
            _border_heght = ship._current_sprite.Height/5+6;
            _hp_width = (ship._current_sprite.Width*2);
            _hp_heght = ship._current_sprite.Height/5;

            _graphicsDevice = graphicsDevice;
            _current_ship = ship;
            //ХП
            _hp_texture = new Texture2D(graphicsDevice, _hp_width, _hp_heght, true, SurfaceFormat.Color);
            Color[] _hp_colors = new Color[_hp_width * _hp_heght];//set the color to the amount of pixels in the textures
            for (int i = 0; i < _hp_colors.Length; i++)//loop through all the colors setting them to whatever values we want
            {
                _hp_colors[i] = _hp_color;
            }            
            _hp_texture.SetData(_hp_colors);
            // рамка
            _border_texture = new Texture2D(graphicsDevice, _border_width, _border_heght, true, SurfaceFormat.Color);
    
            Color[] _border_colors = new Color[_border_width * _border_heght];//set the color to the amount of pixels in the textures
            for (int i = 0; i < _border_colors.Length; i++)//loop through all the colors setting them to whatever values we want
            {
                _border_colors[i] = _border_color;
            }
            _border_texture.SetData(_border_colors);


            _maxHP = _current_ship._max_hp;
            _currentHP = _current_ship._current_hp;

        }
        public  void Draw(SpriteBatch _spriteBatch, Vector2 position)
        {
           
            _spriteBatch.Draw(_border_texture, position, _border_color);
            _spriteBatch.Draw(_hp_texture, new Vector2(position.X+3,position.Y+3), _hp_color);
        }
        public void Update()
        {
            _position = new Vector2(_ship._position.X - 30, _ship._position.Y - 30);
            _maxHP = _current_ship._max_hp;
            _currentHP = _current_ship._current_hp;
            //ХП
            try
            {
                double width = _hp_width;
                double width_rezult = ((width / _maxHP) * _currentHP);
                _hp_texture = new Texture2D(_graphicsDevice, (int)width_rezult
                    , _hp_heght, true, SurfaceFormat.Color);
                Color[] _hp_colors = new Color[(int)width_rezult * _hp_heght];//set the color to the amount of pixels in the textures
                for (int i = 0; i < _hp_colors.Length; i++)//loop through all the colors setting them to whatever values we want
                {
                    _hp_colors[i] = _hp_color;
                }
                _hp_texture.SetData(_hp_colors);
            }
            catch (Exception) { return; }
           
            
        }
    }
}
