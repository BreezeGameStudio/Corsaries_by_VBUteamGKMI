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
        public MyShip(Ship_type ship_Type,
            Microsoft.Xna.Framework.Content.ContentManager content,
            float x_pos,float y_pos) : base(ship_Type)
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


      
    }
}

