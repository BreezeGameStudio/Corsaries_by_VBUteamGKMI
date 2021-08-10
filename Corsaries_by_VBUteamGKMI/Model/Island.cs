using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.IO;

namespace Corsaries_by_VBUteamGKMI.Model
{
    public partial class Island
    {
        public Rectangle _rectangle; // прямоугольник для корабля
        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public Vector2 _position; // позицыя
        public System.Drawing.Bitmap _bitmap;// битмап для пикселя = new System.Drawing.Bitmap(memoryStream);
        public Island(Microsoft.Xna.Framework.Content.ContentManager content,
           float x_pos, float y_pos)
        {
            _position = new Vector2(x_pos, y_pos);
            // выгружаем срайты 

            _current_sprite = content.Load<Texture2D>("1");
            //создаём прямоугольник корабля 
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                 _current_sprite.Width, _current_sprite.Height);
            // инициализация битмапа
            SetBitMap();


        }
        private void SetBitMap()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                _current_sprite.SaveAsJpeg(memoryStream,
                    _current_sprite.Width, _current_sprite.Height);
                _bitmap = new System.Drawing.Bitmap(memoryStream);
            }
        }
    }

//=======================================================================
    public partial class Archipelag
    {
        //Необходимые для отрисовки обьекты Monogame

        public GraphicsDevice GraphicsDevice { get; set; }
        public RenderTarget2D _render { get; set; } 


        //Параметры архипелага. Ширина,высота и позиция
        public int _width { get; set; } = 100;
        public int _height { get; set; } = 100;
        public Vector2 _position { get; set; }

        //Колекция всех тайлов
        public Lazy<List<List<Tile>>> tiles = new Lazy<List<List<Tile>>>();

        //Заранее проиницилизированные тайлы. Вода и песок
        public static Tile water { get; set; }
        public static Tile sand { get; set; }


        //Конструкторы для инициализации обьекта архипелага и присвоения значений.
        //Присутствует два конструктора 1 - без ширины и высоты, 2 - с ними.
        //Размерность при исползовании 1-ого конструктора по умолчанию 1000 на 1000 пикселей

        public Archipelag(Microsoft.Xna.Framework.Content.ContentManager content,Microsoft.Xna.Framework.Graphics.GraphicsDevice device, Vector2 pos)
        {
            this.GraphicsDevice = device;
            this._render = new RenderTarget2D(device,this._width,this._height);
            this._position = pos;
            water = new Tile(content, Vector2.Zero,0);
            sand = new Tile(content, Vector2.Zero, 1);
            Generate_Island();
        }

        public Archipelag(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.Graphics.GraphicsDevice device, Vector2 pos, int width, int height)
        {
            this.GraphicsDevice = device;
            this._render = new RenderTarget2D(device, this._width, this._height);
            this._position = pos;
            water = new Tile(content, Vector2.Zero, 0);
            sand = new Tile(content, Vector2.Zero, 1);
            this._width = width;
            this._height = height;
            Generate_Island();
        }


        //-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_

        //Методы для генерации и отрисовки архипелага
        private void Generate_Island()
        {
            float scale = 0.01f;
            int z_index = new Random().Next(0, 100);
            for (int i = 0; i < _width; i++)
            {
                List<Tile> list = new List<Tile>();
                for (int j = 0; j < _height; j++)
                {
                    int calc = (int)(((SimplexNoise.Noise.Generate(i * scale, j * scale, z_index) + 1) / 2) * 255);
                    Tile tile = null;
                    for (int c = 200; c < 256; c++)
                    {
                        if (System.Drawing.Color.FromArgb(255, calc, calc, calc) == System.Drawing.Color.FromArgb(255, c, c, c))
                        {
                            tile = sand;
                            break;
                        }
                        else
                        {
                            tile = water;
                        }
                    }
                    tile.Position = new Vector2(j+_position.X, i+_position.Y);
                    list.Add(tile);
                }
                this.tiles.Value.Add(list);
            }
        }
        public Lazy<List<Tile>> Exclude_Terrain(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            Lazy<List<Tile>> sand_tiles = new Lazy<List<Tile>>();
            foreach (var item in tiles.Value)
            {
                foreach (var tile in item)
                {
                    if(tile.Texture == content.Load<Texture2D>("sand"))
                    {
                        sand_tiles.Value.Add(tile);
                    }
                }
            }
            return sand_tiles;
        }

        public void Draw_Island(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            GraphicsDevice.SetRenderTarget(_render);
            _spriteBatch.Begin();
            for (int i = 0; i < tiles.Value.Count; i++)
            {
                for (int j = 0; j < tiles.Value[i].Count; j++)
                {
                    _spriteBatch.Draw(tiles.Value[i][j].Texture, new Vector2(j, i), Color.White);
                }
            }
            _spriteBatch.End();
        }
    }
}
