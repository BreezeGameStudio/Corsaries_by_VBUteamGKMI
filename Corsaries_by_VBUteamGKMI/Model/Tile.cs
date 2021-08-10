using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Corsaries_by_VBUteamGKMI.Model
{
    public class Tile
    {

        private int id = new Random().Next(0, 3);

        public int Id { get { return id; } set { id = value; } }

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public Tile(ContentManager content, Vector2 position)
        {
            this.Position = position;
            if (this.Id == 0)
            {
                this.Texture = content.Load<Texture2D>("water");
            }
            else if (this.Id == 1)
            {
                this.Texture = content.Load<Texture2D>("sand");
            }
            else if (this.Id == 2)
            {
                this.Texture = content.Load<Texture2D>("grass");
            }
        }
        public Tile(ContentManager content, Vector2 position, int id)
        {
            this.Id = id;
            this.Position = position;
            if (this.Id == 0)
            {
                this.Texture = content.Load<Texture2D>("water");
            }
            else if (this.Id == 1)
            {
                this.Texture = content.Load<Texture2D>("sand");
            }
            else if (this.Id == 2)
            {
                this.Texture = content.Load<Texture2D>("grass");
            }
        }
    }
}
