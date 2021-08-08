﻿using Corsaries_by_VBUteamGKMI.Model;
using Corsaries_by_VBUteamGKMI.Model.Ship;
using Corsaries_by_VBUteamGKMI.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corsaries_by_VBUteamGKMI
{
    
    public class Game1 : Game
    {
      
        public static  int _game_ground_X_Y = 10000;
       
        public static List<NPS_Ship> _nps = new List<NPS_Ship>(); // коллекция нпс

        //наймер смены направления движения нпс
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();



        // размеры игровой карты
       public static Game_ground _game_ground = new Game_ground(_game_ground_X_Y, _game_ground_X_Y);
        SpriteFont _text;
        Vector2 _text_pos ; // позиция

        private List<Archipelag> _islands = new List<Archipelag>(); // коллекция островов

        // камера
        Camera2d _camera = new Camera2d();
        //текущий монитор   
        public static System.Drawing.Size _size_screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;   
        private GraphicsDeviceManager _graphics; // графика
        private SpriteBatch _spriteBatch; // отрисовщик спрайтов
        private MyShip _myShip; // мой кораблик

        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; // директория закачки контента
            IsMouseVisible = true; // видимость мышки
                                   // таймер 
            _timer.Interval = 3000;// раз в 10 сек
            _timer.Tick += _timer_Tick; // событие тика
            _timer.Start();

            // инициализируем камеру
            _camera.Pos = new Vector2(500.0f, 200.0f);

            // задаём размер игрового окна с отступами        
            //_graphics.IsFullScreen = true;
            //
            _graphics.PreferredBackBufferHeight = _size_screen.Height;
            _graphics.PreferredBackBufferWidth = _size_screen.Width;
        }

       

        private void _timer_Tick(object sender, System.EventArgs e) => _nps.ForEach(i => i.Next_Move());


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            // добавляем нпс
            _nps.Clear(); //очищаем коллекцию чтобы при перезапуске не становилось больше NPS
            for (int i = 0; i < 5; i++)
            {
                _nps.Add(new NPS_Ship((Ship_type)new Random().Next(0,7),Content));
            }

            for (int i = 0; i < 1; i++)
            {
                Archipelag island = new Archipelag(Content, GraphicsDevice, new Vector2(i*1000,0));
                _islands.Add(island);
                island.Draw_Island(_spriteBatch);
            }

            _myShip = new MyShip(Ship_type.Corvette,Content);
           

            // тестовый текст
            _text = Content.Load<SpriteFont>("testtext");
           
        }

        protected override void LoadContent() => _spriteBatch = new SpriteBatch(GraphicsDevice);


        protected override void Update(GameTime gameTime)
        {
            if (this.IsActive)
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

                // проверка столкнованеий моего корабля
                if (Collision_island(_myShip))
                    _myShip.Step_Back_Position(); // возвращение к старой позиции при столкновениее


                // проверка столкноваений НПС с островами
                //foreach (var item in _nps)
                //{
                //    if (Collision_island(item))
                //        item.Step_Back_Position();// возвращение к старой позиции при столкновениее
                //}

                Collision_NPS(_myShip); // столкновение меня и нпс


                // даём камере позицию корабля
                _camera.Pos = _myShip._position;
                _text_pos.Y = _myShip._position.Y - (_size_screen.Height / 2);
                _text_pos.X = _myShip._position.X - (_size_screen.Width / 2);


                // НПС ДВИЖЕНИЕ
                _nps.ForEach(i => i.Move());


                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // обязательный метод начала отрисовки в который передают камеру
            _spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend, null, null, null, null,
                        _camera.get_transformation(GraphicsDevice));

            foreach (var item in _islands)
            {
                _spriteBatch.Draw(item._render, item._position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            _spriteBatch.Draw(_myShip._current_sprite, _myShip._position, Color.White); // отрисовка корабля        
            // отрисовка островов                                                                             
            // отрисовка nps  
            _nps.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White));
            /// тест текста

        Color color = new Color(0, 0, 0); // цвет желтый
            if (_myShip != null)
            {
                _spriteBatch.DrawString(_text, $"X {_myShip._position.X} Y {_myShip._position.Y}",
                     _text_pos, color);
            } // рисуем текст





            _spriteBatch.End();// обязательный метод 
            base.Draw(gameTime);
        }
        // метод столкновения с островами
        protected bool Collision_island(Ship ship)
        {
            bool collide = false;
                //создаём прямоугольник корабля 
              Rectangle  R_ship = new Rectangle((int)ship._position.X, (int)ship._position.Y,
                     ship._current_sprite.Width, ship._current_sprite.Height);
            //бежим по колекции островов и проверяем на столкновение
            Parallel.For(0,_islands.Count, i => {
                List<Tile> sands = _islands[i].Exclude_Terrain(Content).Value;
                Parallel.For(0,sands.Count, j =>
                {
                    if (sands[j].Texture.Bounds.Intersects(R_ship))
                    {
                        collide = true;
                    }
                });
            });
            return collide;
        }

        protected void Collision_NPS(Ship ship)
        {
            //создаём прямоугольник корабля 
            Rectangle R_ship = new Rectangle((int)ship._position.X, (int)ship._position.Y,
                   ship._current_sprite.Width, ship._current_sprite.Height);
            //бежим по колекции NPS и проверяем на столкновение
            try
            {
                foreach (var item in _nps)
                {
                    // создаём прямоугольник NPS
                    Rectangle nps = new Rectangle((int)item._position.X-100, (int)item._position.Y-100,
                        item._current_sprite.Width+100, item._current_sprite.Height+100);
                    if (R_ship.Intersects(nps))
                    {

                        System.Windows.Forms.DialogResult rez = System.Windows.Forms.MessageBox.Show($"Вы хотите вступить в бой с {item._name}", "Обнаружен корабыль",
                            System.Windows.Forms.MessageBoxButtons.YesNo);

                        if (rez == System.Windows.Forms.DialogResult.Yes) //если предложение о бое было принято
                        {
                            System.Windows.Forms.DialogResult result = new Battle_Form(_myShip, item).ShowDialog(); //запускаем форму и присваем результат переменной

                            if(result == System.Windows.Forms.DialogResult.No) //если форма возвращает ответ "нет" тоесть мы не победили а проиграли то тогда перезапустить игру
                            {
                                this.Initialize(); //перезапуск игры
                            }
                            
                        }

                        else //если форма была закрыта или предложение о бое было отклонено
                        {
                            //проверка направления NPS и перемещение нашего корабля в противоположном направлении
                            if(item._direction == NPS_Ship.Direction.up)
                            {
                                ship._position = new Vector2(nps.X + nps.Width/2,nps.Y+nps.Height + ship._rectangle.Height);
                            }

                            else if (item._direction == NPS_Ship.Direction.up_right)
                            {
                                ship._position = new Vector2(nps.X - ship._rectangle.Width, nps.Y + nps.Height);
                            }

                            else if (item._direction == NPS_Ship.Direction.right)
                            {
                                ship._position = new Vector2(nps.X - ship._rectangle.Width, nps.Y + nps.Height/2);
                            }

                            else if (item._direction == NPS_Ship.Direction.right_down)
                            {
                                ship._position = new Vector2(nps.X - ship._rectangle.Width, nps.Y - ship._rectangle.Height);
                            }

                            else if (item._direction == NPS_Ship.Direction.down)
                            {
                                ship._position = new Vector2(nps.X + nps.Width/2, nps.Y - ship._rectangle.Height);
                            }

                            else if (item._direction == NPS_Ship.Direction.down_left)
                            {
                                ship._position = new Vector2(nps.X + nps.Width, nps.Y - ship._rectangle.Height);
                            }

                            else if (item._direction == NPS_Ship.Direction.left)
                            {
                                ship._position = new Vector2(nps.X + nps.Width, nps.Y + nps.Height/2);
                            }

                            else if (item._direction == NPS_Ship.Direction.left_up)
                            {
                                ship._position = new Vector2(nps.X + nps.Width, nps.Y + nps.Height);
                            }

                        }

                    }
                }

            }
            catch (InvalidOperationException ) { return; }
                     
        }
    }

    public struct Game_ground // структура для храения размеров игрового поля
    {
        public Game_ground (int x_e, int y_e)
        {
            _x_b = 0; _x_e = x_e;
            _y_b = 0; _y_e = y_e;
        }
        public int _x_b { get; set; } // ось Х начало
        public int _x_e { get; set; } // ось Х конец
        public int _y_b { get; set; } // ось У начало
        public int _y_e { get; set; } // ось У конец
    }
}
