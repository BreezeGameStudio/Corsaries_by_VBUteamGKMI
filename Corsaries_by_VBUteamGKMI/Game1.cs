using Corsaries_by_VBUteamGKMI.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Corsaries_by_VBUteamGKMI
{
    public class Game1 : Game
    {
        private List<Island> _islands = new List<Island>();




        private System.Drawing.Size _size_screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;//текущий монитор      
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MyShip _myShip;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // задаём размер игрового окна с отступами        
            _graphics.IsFullScreen = true;
            //
            _graphics.PreferredBackBufferHeight = _size_screen.Height;
            _graphics.PreferredBackBufferWidth = _size_screen.Width;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            _myShip = new MyShip(Content, _size_screen);
            _islands.Add(new Island(Content, "1", new Vector2(0, 500)));
            _islands.Add(new Island(Content, "1", new Vector2(600, 100)));
            _islands.Add(new Island(Content, "1", new Vector2(100, 200)));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            #region кнопки перемещения
            // перемещение  по карте

            // верх лево
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A))
                _myShip.Go_UL();
            // верх право
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D))
                _myShip.Go_UR();
            // низ лево
            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A))
                _myShip.Go_DL();
            // низ право
            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D))
                _myShip.Go_DR();



            //лево
            if (Keyboard.GetState().IsKeyDown(Keys.A) &&
                Keyboard.GetState().IsKeyUp(Keys.W) &&
                Keyboard.GetState().IsKeyUp(Keys.S))
                _myShip.Go_L();
            //право
            if (Keyboard.GetState().IsKeyDown(Keys.D) &&
                Keyboard.GetState().IsKeyUp(Keys.W) &&
                Keyboard.GetState().IsKeyUp(Keys.S))
                _myShip.Go_R();
            //верх
            if (Keyboard.GetState().IsKeyDown(Keys.W) &&
                Keyboard.GetState().IsKeyUp(Keys.A) &&
                Keyboard.GetState().IsKeyUp(Keys.D))
                _myShip.Go_U();
            // низ
            if (Keyboard.GetState().IsKeyDown(Keys.S) &&
                Keyboard.GetState().IsKeyUp(Keys.A) &&
                Keyboard.GetState().IsKeyUp(Keys.D))
                _myShip.Go_D();
            #endregion
            if (Collide())
                _myShip.Step_Back_Position(); // возвращение к старой позиции при столкновениее
          


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(); // обязательный метод начала
            _spriteBatch.Draw(_myShip._current_sprite, _myShip._position, Color.White); // отрисовка корабля        
           // отрисовка островов
            _islands.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White));
            _spriteBatch.End();// обязательный метод 
            base.Draw(gameTime);
        }
        // метод столкновения
        protected bool Collide()
        {
            //создаём прямоугольник корабля 
            Rectangle ship = new Rectangle((int)_myShip._position.X, (int)_myShip._position.Y,
                _myShip._current_sprite.Width, _myShip._current_sprite.Height);
            //бежим по колекции островов и проверяем на столкновение
            foreach (var item in _islands)
            {
                // создаём прямоугольник острова
                Rectangle island = new Rectangle((int)item._position.X, (int)item._position.Y, 
                    item._current_sprite.Width, item._current_sprite.Height);
                if (ship.Intersects(island))
                    return true;
            }

            return false;
        }
    }
}
