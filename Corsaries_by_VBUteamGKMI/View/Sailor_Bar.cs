using Corsaries_by_VBUteamGKMI.Model.Ship;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Corsaries_by_VBUteamGKMI
{
    public partial class Sailor_Bar
    {
        Ship _ship;
        public Vector2 _position;
        GraphicsDevice _graphicsDevice;
        public Ship _current_ship;
        public int _max_sailor;
        public int _current_sailor;
        // рамка
        public Vector2 _border_position;
        public Texture2D _border_texture;
        public int _border_width;
        public int _border_heght;
        public Color _border_color = Color.Black;
        // HP     
        public Vector2 _hp_position;
        public Texture2D _hp_texture;
        public int _sailor_width;
        public int _sailor_heght;
        public Color _sailor_color;
        public Sailor_Bar(GraphicsDevice graphicsDevice, Ship ship, Color hp_color)
        {
            _ship = ship;
            _position = new Vector2(_ship._position.X - 30, _ship._position.Y - 20);
            _sailor_color = hp_color;
            _border_width = ship._current_sprite.Width * 2 + 4;
            _border_heght = ship._current_sprite.Height / 5 + 4;
            _sailor_width = (ship._current_sprite.Width * 2);
            _sailor_heght = ship._current_sprite.Height / 5;

            _graphicsDevice = graphicsDevice;
            _current_ship = ship;
            //ХП
            _hp_texture = new Texture2D(graphicsDevice, _sailor_width, _sailor_heght, true, SurfaceFormat.Color);
            Color[] _hp_colors = new Color[_sailor_width * _sailor_heght];//set the color to the amount of pixels in the textures
            for (int i = 0; i < _hp_colors.Length; i++)//loop through all the colors setting them to whatever values we want
            {
                _hp_colors[i] = _sailor_color;
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


            _max_sailor = _current_ship._max_hp;
            _current_sailor = _current_ship._current_hp;

        }
        public void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(_border_texture, _position, _border_color);
            _spriteBatch.Draw(_hp_texture, new Vector2(_position.X + 2, _position.Y + 2), _sailor_color);
        }
        public void Update()
        {
            _position = new Vector2(_ship._position.X - 30, _ship._position.Y - 20);
            _max_sailor = _current_ship._max_count_sailors;
            _current_sailor = _current_ship._current_count_sailors;
            //ХП
            try
            {
                double width = _sailor_width;
                double width_rezult = ((width / _max_sailor) * _current_sailor);
                _hp_texture = new Texture2D(_graphicsDevice, (int)width_rezult
                    , _sailor_heght, true, SurfaceFormat.Color);
                Color[] _hp_colors = new Color[(int)width_rezult * _sailor_heght];//set the color to the amount of pixels in the textures
                for (int i = 0; i < _hp_colors.Length; i++)//loop through all the colors setting them to whatever values we want
                {
                    _hp_colors[i] = _sailor_color;
                }
                _hp_texture.SetData(_hp_colors);
            }
            catch (Exception) { return; }


        }
    }
}
