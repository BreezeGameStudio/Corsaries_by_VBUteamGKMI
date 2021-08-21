using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using Corsaries_by_VBUteamGKMI.Model.Products;
using Corsaries_by_VBUteamGKMI.Model.People_on_ship;

namespace Corsaries_by_VBUteamGKMI.Model
{

    public class Seaport
    {
        #region параметры острова
        public Random _random = new Random(); // рандом 
        public Rectangle _rectangle; // прямоугольник для корабля
        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public Vector2 _position;
        public List<Product> _products = new List<Product>(); // колекция товаров
        public List<Sailor> _sailors = new List<Sailor>(); // колекция моряков
        public int _money;// денег в порту
        public int _price_1hp_cap; // цена за единицу выличеного хп
        public int _price_1_unga; // цена 1го юнгу
        public int _price_1_Experienced; // цена 1го бывалого
        public int _price_1_Sea_wolf; // цена 1го морского волка

        #endregion
        public Seaport(Microsoft.Xna.Framework.Content.ContentManager content, string name_sprit, Vector2 pos)
        {
            _current_sprite=content.Load<Texture2D>(name_sprit);
            _position = pos;
            //создаём прямоугольник порта 
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                 _current_sprite.Width, _current_sprite.Height);
            // инициализируем в нашей колекции места пот продукты
            for (int i = 0; i < 8; i++)
                _products.Add(new Product((Product_type)i));
            // инициализируем в нашей колекции матросов
            for (int i = 0; i < 3; i++)
                _sailors.Add(new Sailor((Sailor_type)i));

            _products.ForEach(i => i._count = 500);
            _sailors.ForEach(i => i._count = 500);

            _price_1hp_cap = 1;
            _price_1_unga = _sailors[0]._price; // цена 1го юнгу
            _price_1_Experienced = _sailors[1]._price; // цена 1го бывалого
            _price_1_Sea_wolf = _sailors[2]._price;  // цена 1го морского волка
        }
       
    }
}
