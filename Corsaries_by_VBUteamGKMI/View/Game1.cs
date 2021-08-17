using Corsaries_by_VBUteamGKMI.Model;
using Corsaries_by_VBUteamGKMI.Model.Ship;
using Corsaries_by_VBUteamGKMI.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Corsaries_by_VBUteamGKMI
{
    public enum Game_Sate { In_Menu,In_World,In_Battle,In_Port}
    public class Game1 : Game
    {
        // мои снаряды
        public static List<Cannonball> _my_cannonballs = new List<Cannonball>();
        //снаряды врага
        public static List<Cannonball> _enemy_cannonballs = new List<Cannonball>();
        System.Drawing.Color _water_colorl;
        System.Drawing.Color _water_colorl_2;
        public Game_Sate _game_state; //состояние игры
        public static int _game_ground_X_Y = 5000; // размер карты     
        // размеры игровой карты
        public static Game_ground _game_ground = new Game_ground(_game_ground_X_Y, _game_ground_X_Y);
        public static List<NPS_Ship> _nps = new List<NPS_Ship>(); // коллекция нпс
        //таймер смены направления движения нпс
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        SpriteFont _text;
        Vector2 _text_pos; // позиция
        private List<Island> _islands = new List<Island>(); // коллекция островов
        private List<Vector2> _island_positions = new List<Vector2>() { new Vector2(1000, 2500), new Vector2(2000, 1500), new Vector2(3000, 1500), new Vector2(4500, 2500), new Vector2(3500, 4000), new Vector2(2500, 4000) };
        private List<Vector2> _edge_island_positions = new List<Vector2>() { new Vector2(2000, 0), new Vector2(2500, 4500) };
        private List<Vector2> _continent_positions = new List<Vector2>() { new Vector2(_game_ground_X_Y - 500, 0), new Vector2(0,0), new Vector2(0, _game_ground_X_Y-500), new Vector2(_game_ground_X_Y - 500, _game_ground_X_Y - 500) };
        // камера
        public  Camera2d _camera = new Camera2d();
        //текущий монитор   
        public static System.Drawing.Size _size_screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
        public static GraphicsDeviceManager _graphics; // графика
        private SpriteBatch _spriteBatch; // отрисовщик спрайтов
        private MyShip _myShip; // мой кораблик
        private NPS_Ship _enemyShip;



        public Game1()
        {
            _game_state = Game_Sate.In_World;
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

        // тик таймера изменение движения нпс
        private void _timer_Tick(object sender, System.EventArgs e) => _nps.ForEach(i => i.Next_Move());

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            // добавляем нпс
            _nps.Clear(); //очищаем коллекцию чтобы при перезапуске не становилось больше NPS
            for (int i = 0; i < 5; i++)
            {
                _nps.Add(new NPS_Ship((Ship_type)new Random().Next(0, 7), Content));
            }
            // добавляем острова
            _islands.Clear(); //очищаем коллекцию чтобы при перезапуске не становилось больше островов
            foreach (var item in _island_positions)
            {
                _islands.Add(new Island(Content, item, Content.Load<Texture2D>($"island_{_island_positions.IndexOf(item) + 1}")));
            }
            foreach (var item in _edge_island_positions)
            {
                _islands.Add(new Island(Content, item, Content.Load<Texture2D>($"edge_island_{_edge_island_positions.IndexOf(item) + 1}")));
            }
            foreach (var item in _continent_positions)
            {
                _islands.Add(new Island(Content, item, Content.Load<Texture2D>($"continent_{_continent_positions.IndexOf(item) + 1}")));
            }
            _islands.Add(new Island(Content, new Vector2(2250, 2250), Content.Load<Texture2D>("center_island")));
            _myShip = new MyShip(Ship_type.Corvette, Content, 2000, 2000);

            // получаем цвет воды
             _water_colorl = GetColorWaterIsland(_islands[0], 1, 1);
            _water_colorl_2 =  System.Drawing.Color.FromArgb(255,101,148,236);
          

            // тестовый текст
            _text = Content.Load<SpriteFont>("testtext");

        }
        protected override void LoadContent() => _spriteBatch = new SpriteBatch(GraphicsDevice);
        protected override void Update(GameTime gameTime)
        {

            if (this.IsActive)
            {

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
                //обновление в зависимости от состоянис игры
                switch (_game_state)
                {
                    case Game_Sate.In_World:
                        In_World_Update(gameTime);
                        break;
                    case Game_Sate.In_Battle:
                        In_Battle_Update(gameTime);
                        break;
                    case Game_Sate.In_Port:
                        break;
                    default:
                        break;
                }
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

            // отрисовка в зависимости от состояния игры
            switch (_game_state)
            {
                case Game_Sate.In_Menu:
                    break;
                case Game_Sate.In_World:
                    In_World_Draw(gameTime);
                    break;
                case Game_Sate.In_Battle:
                    In_Battle_Draw(gameTime);
                    break;
                case Game_Sate.In_Port:
                    break;
                default:
                    break;
            }
            _spriteBatch.End();// обязательный метод 
            base.Draw(gameTime);
        }




        // установка стостояния игры в открытом мире
        public void Set_In_World_GS()
        {
            // задаём размеры игрового поля 
            _game_ground = new Game_ground(_game_ground_X_Y, _game_ground_X_Y);
            // задаём состояние игры
            _game_state = Game_Sate.In_World;
        }
        // установка стостояния игры в битве
        public void Set_In_Battle_GS()
        {
            // задаём размеры боевого поля 
            _game_ground = new Game_ground((int)_camera._pos.X - (_size_screen.Width / 2),
                (int)_camera._pos.X + (_size_screen.Width / 2),
                (int)_camera._pos.Y - (_size_screen.Height / 2),
                (int)_camera._pos.Y + (_size_screen.Height / 2));
            // задаём состояние игры
            _game_state = Game_Sate.In_Battle;
            _my_hp_bar = new HP_Bar(GraphicsDevice, _myShip);
            _enemy_hp_bar = new HP_Bar(GraphicsDevice, _enemyShip);
            // чистим снаряды
            _enemy_cannonballs.Clear();
            _my_cannonballs.Clear();

        }


        #region In_World
        // обновленние данных при состояние игры игровой мир
        private void In_World_Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // кнопка проверки своего судна
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Show_Info_Ship(_myShip);
            // проверка столкнованеий моего корабля с островом
            if (Collision_island(_myShip))
                _myShip.Step_Back_Position(); // возвращение к старой позиции при столкновениее
            // проверка столкноваений НПС с островами
            foreach (var item in _nps)
            {
                if (Collision_island(item))
                {
                    item.Step_Back_Position();// возвращение к старой позиции при столкновениее
                    item.Next_Move();
                }
            }
            Collision_NPS(_myShip); // столкновение меня и нпс
            // даём камере позицию корабля
            _camera.SetPosition(_myShip);
            _text_pos.Y = _camera.Pos.Y - (_size_screen.Height / 2);
            _text_pos.X = _camera.Pos.X - (_size_screen.Width / 2);
            // НПС ДВИЖЕНИЕ
            _nps.ForEach(i => i.Move());

        }
        // отрисовка данных при состояние игры игровой мир
        private void In_World_Draw(GameTime gameTime)
        {
            if (_myShip != null)
            {
                _spriteBatch.DrawString(_text, $"X {_myShip._position.X} Y {_myShip._position.Y}",
                     _text_pos, new Color(0, 0, 0));
            } // рисуем текст
            // отрисовка островов   
            foreach (var item in _islands)
            {
                _spriteBatch.Draw(item._current_sprite, item._position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
            }
            // отрисовка nps 
            _nps.ForEach(i => i._position = new Vector2(2000, 2000));
            _nps.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White));
            _spriteBatch.Draw(_myShip._current_sprite, _myShip._position, Color.White); // отрисовка корабля

        }
        // метод столкновения с островами
        protected bool Collision_island(Ship ship)
        {
            bool collide = false;

            //создаём прямоугольник корабля 
            Rectangle R_ship = new Rectangle((int)ship._position.X, (int)ship._position.Y,
                   ship._current_sprite.Width, ship._current_sprite.Height);
            foreach (var item in _islands)
            {
                //создаём прямоугольник острова 
                Rectangle R_island = new Rectangle((int)item._position.X, (int)item._position.Y,
                       item._current_sprite.Width, item._current_sprite.Height);
                //бежим по колекции островов и проверяем на столкновение
                if (R_ship.Intersects(R_island))
                {



                    if (ship._position.Y + (ship._current_sprite.Height / 2) > item._position.Y
                        && ship._position.Y + (ship._current_sprite.Height / 2) < (item._position.Y + item._current_sprite.Height)
                       && ship._position.X + (ship._current_sprite.Width / 2) > item._position.X
                       && ship._position.X + (ship._current_sprite.Width / 2) < (item._position.X + item._current_sprite.Width))
                    {


                        int x = (int)(ship._position.X + (ship._current_sprite.Width / 2) - item._position.X);
                        int y =item._current_sprite.Height- (int)(ship._position.Y + (ship._current_sprite.Height / 2) - item._position.Y);
                        try
                        {
                            System.Drawing.Color color = GetColorWaterIsland(item, x, y);
                            if (_water_colorl.ToArgb() != color.ToArgb()
                           && _water_colorl_2.ToArgb() != color.ToArgb())
                             
                            {
                                
                                collide = true;
                            }
                        }
                        catch (Exception) { return collide; }
                    }

                        
                    
                    
                    //if (ship._position.X > item._position.X
                    //&& ship._position.X < (item._position.X + item._current_sprite.Width))
                    //{

                    //    if (ship._position.Y > item._position.Y
                    //        && ship._position.Y < (item._position.Y + item._current_sprite.Height))
                    //    {
                    //        int x = (int)((ship._position.X + ship._current_sprite.Width - item._position.X));
                    //        int y = (int)((ship._position.Y + ship._current_sprite.Height - item._position.Y));
                    //        try
                    //        {
                    //            System.Drawing.Color color = GetColorWaterIsland(item, x, y);
                    //            if (_water_colorl.ToArgb() != color.ToArgb())
                    //            {
                    //                collide = true;
                    //            }
                    //        }
                    //        catch (Exception) { return collide; }


                    //    }
                    //}
                }
            }

            return collide;
        }
        private System.Drawing.Color GetColorWaterIsland(Island island, int x, int y) => island._bitmap.GetPixel(x, y);
      // проверка столкновений с НПС
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
                    Rectangle nps = new Rectangle((int)item._position.X - 100, (int)item._position.Y - 100,
                        item._current_sprite.Width + 100, item._current_sprite.Height + 100);
                    if (R_ship.Intersects(nps))
                    {
                        // коллекция вопросов при столкновении
                        List<string> questions = new List<string> { "Напасть", "Разведка", "Уплыть" };
                        int answer;
                        do
                        {
                            answer = MessageBox.Show("Обнаружен корабыль", "Выбирете действие", questions).Result.Value;
                            if (answer == 1)
                                Show_Info_Ship(item);
                        } while (answer == 1);                     

                        if (answer == 0) //если предложение о бое было принято
                        {
                            // делаем врагом выбраного нпс
                            _enemyShip = item;
                            // даём игре состояние битвы
                            Set_In_Battle_GS();

                        }
                        else //если форма была закрыта или предложение о бое было отклонено
                        {
                            //проверка направления NPS и перемещение нашего корабля в противоположном направлении
                            switch (item._direction)
                            {
                                case Direction.up:
                                    ship._position = new Vector2(nps.X + nps.Width / 2, nps.Y + nps.Height + ship._rectangle.Height);
                                    break;
                                case Direction.up_right:
                                    ship._position = new Vector2(nps.X - ship._rectangle.Width, nps.Y + nps.Height);
                                    break;
                                case Direction.right:
                                    ship._position = new Vector2(nps.X - ship._rectangle.Width, nps.Y + nps.Height / 2);
                                    break;
                                case Direction.right_down:
                                    ship._position = new Vector2(nps.X - ship._rectangle.Width, nps.Y - ship._rectangle.Height);
                                    break;
                                case Direction.down:
                                    ship._position = new Vector2(nps.X + nps.Width / 2, nps.Y - ship._rectangle.Height);
                                    break;
                                case Direction.down_left:
                                    ship._position = new Vector2(nps.X + nps.Width, nps.Y - ship._rectangle.Height);
                                    break;
                                case Direction.left:
                                    ship._position = new Vector2(nps.X + nps.Width, nps.Y + nps.Height / 2);
                                    break;
                                case Direction.left_up:
                                    ship._position = new Vector2(nps.X + nps.Width, nps.Y + nps.Height);
                                    break;
                                default:
                                    break;
                            }

                        }

                    }
                }

            }
            catch (InvalidOperationException) { return; }

        }
        // показать инфо о корадбях запуск формы
        private void Show_Info_Ship(Ship ship)=> new Info_Form(ship).ShowDialog();

        #endregion

        #region In Battle

        public HP_Bar _my_hp_bar;
        public HP_Bar _enemy_hp_bar;


        // обновленние данных при состояние игры бой
        private void In_Battle_Update(GameTime gameTime)
        {
            // кнопка збежать с боя
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Escape_Battle();

            // кнопка абордажа
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Boarding(_myShip,_enemyShip);


            _enemyShip.Move_in_Battle(_myShip);
            // стрелять лево
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                _myShip.Shoot_Left();
            // стрелять лево
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                _myShip.Shoot_Right();

            try { _my_cannonballs.ForEach(i => i.Move()); }// движение снарядов
            catch (Exception) { }
            try { _enemy_cannonballs.ForEach(i => i.Move()); }// движение врага снарядов
            catch (Exception) { }

            // проверка на урон 
            Hit_Enemy(_myShip,_enemyShip);
            Hit_My(_myShip, _enemyShip);

            // очистка поля от снарядов
            _my_cannonballs.FindAll(i => i._range <= 0).ForEach(q => _my_cannonballs.Remove(q));
            _enemy_cannonballs.FindAll(i => i._range <= 0).ForEach(q => _enemy_cannonballs.Remove(q));

            // проверка на конец боя
            if (EndBattle())
                Set_In_World_GS();
            // обновление хп бара
            _my_hp_bar.Update();
            _enemy_hp_bar.Update();


        }
        // отрисовка данных при состояние игры бой
        private void In_Battle_Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_myShip._current_sprite, _myShip._position, Color.White); // отрисовка корабля
            _spriteBatch.Draw(_enemyShip._current_sprite, _enemyShip._position, Color.White); // отрисовка врага
            _my_cannonballs.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White)); // отрисовка снаряда 
            _enemy_cannonballs.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White)); //  отрисовка снаряда врага                                                                              // отрисовка хп бара

            _my_hp_bar.Draw(_spriteBatch, new Vector2(_myShip._position.X - 30, _myShip._position.Y - 30));
            _enemy_hp_bar.Draw(_spriteBatch, new Vector2(_enemyShip._position.X - 30, _enemyShip._position.Y - 30));


        }
        // сбежать с боя
        private void Escape_Battle()
        {
            System.Windows.Forms.DialogResult rez = System.Windows.Forms.MessageBox.Show($"Вы хотите сбежать с боя с {_enemyShip._name}", "Струсил?",
                            System.Windows.Forms.MessageBoxButtons.YesNo);

            if (rez == System.Windows.Forms.DialogResult.Yes) //если предложение о бое было принято
            {
                for (int i = 0; i < 3; i++)
                {
                    _enemyShip.Move();
                }
                Set_In_World_GS();
            }
        }
        // проверка  на конец боя
        public bool EndBattle()
        {
            bool rez = false;
            if ( IsEndBattle(_enemyShip._current_hp)|| IsEndBattle(_enemyShip._captain._current_hp))
            {
                _nps.Remove(_enemyShip);
                MessageBox.Show($"Это ПОБЕДА над {_enemyShip._name}", "Открывай ром!!!",new List<string>() { "ОК"});              
                rez= true;
            }
         
            if (IsEndBattle(_myShip._current_hp)|| IsEndBattle(_myShip._captain._current_hp))
            {
                MessageBox.Show($"Нас РАЗГРОМИЛ {_enemyShip._name}", "Спасайся!!!", new List<string>() { "ОК" });
                Initialize();
                rez = true;
            }
           
              
            return rez;
        }
        //абордаж
       public void Boarding(Ship My_ship, Ship Enemy_ship)
        {
            //создаём прямоугольник корабля 
            Rectangle R_ship = new Rectangle((int)Enemy_ship._position.X, (int)Enemy_ship._position.Y,
                   Enemy_ship._current_sprite.Width, Enemy_ship._current_sprite.Height);
            // создаём прямоугольник врага
                    Rectangle nps = new Rectangle((int)Enemy_ship._position.X - 100, (int)Enemy_ship._position.Y - 100,
                        Enemy_ship._current_sprite.Width + 100, Enemy_ship._current_sprite.Height + 100);
            if (R_ship.Intersects(nps))
            {
                try { new Abordage_Form(My_ship._captain, Enemy_ship._captain).ShowDialog(); }
                catch (Exception) { }
            }
           
            
        }
        // проверка жизненых показателей
        private bool IsEndBattle(int mark) => mark <= 0;
        // урон по врагу
        protected void Hit_Enemy(Ship My_ship, Ship Enemy_ship)
        {
            //создаём прямоугольник корабля 
            Rectangle R_ship = new Rectangle((int)Enemy_ship._position.X, (int)Enemy_ship._position.Y,
                   Enemy_ship._current_sprite.Width, Enemy_ship._current_sprite.Height);
            //бежим по колекции ядер и проверяем на столкновение
            try
            {
                foreach (var item in _my_cannonballs)
                {
                    // создаём прямоугольник ядер
                    Rectangle cannonball = new Rectangle((int)item._position.X , (int)item._position.Y ,
                        item._current_sprite.Width , item._current_sprite.Height );
                    if (R_ship.Intersects(cannonball))
                    {
                        Enemy_ship.GetDamaged(My_ship._cannon);
                        item._range = 0; // для того что бы не продолжало дальше наносить урон
                    }
                }
            }
            catch (Exception) { }
        }
       // урон по мне
        protected void Hit_My(Ship My_ship, Ship Enemy_ship)
        {
            //создаём прямоугольник корабля 
            Rectangle R_ship = new Rectangle((int)My_ship._position.X, (int)My_ship._position.Y,
                   My_ship._current_sprite.Width, My_ship._current_sprite.Height);
            //бежим по колекции ядер и проверяем на столкновение
            try
            {
                foreach (var item in _enemy_cannonballs)
                {
                    // создаём прямоугольник ядер
                    Rectangle cannonball = new Rectangle((int)item._position.X , (int)item._position.Y ,
                        item._current_sprite.Width , item._current_sprite.Height );
                    if (R_ship.Intersects(cannonball))
                    {
                        My_ship.GetDamaged(Enemy_ship._cannon);
                        item._range = 0; // для того что бы не продолжало дальше наносить урон
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion

        private void In_Menu_Update(GameTime gameTime)
        {

            // кнопка абордажа
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Boarding(_myShip, _enemyShip);

            _enemyShip.Move();
            // стрелять лево
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                _myShip.Shoot_Left();
            // стрелять лево
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                _myShip.Shoot_Right();

            try { _my_cannonballs.ForEach(i => i.Move()); }// движение снарядов
            catch (Exception) { }
            try { _enemy_cannonballs.ForEach(i => i.Move()); }// движение врага снарядов
            catch (Exception) { }

            // проверка на урон 
            Hit_Enemy(_myShip, _enemyShip);
            Hit_My(_myShip, _enemyShip);

            // проверка на конец боя
            if (EndBattle())
                Set_In_World_GS();



        }
         private void In_Menu_Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_myShip._current_sprite, _myShip._position, Color.White); // отрисовка корабля
            _spriteBatch.Draw(_enemyShip._current_sprite, _enemyShip._position, Color.White); // отрисовка врага
            _my_cannonballs.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White)); // отрисовка снаряда 

        }
    }

    public struct Game_ground // структура для храения размеров игрового поля
    {
        public Game_ground (int x_e, int y_e)
        {
            _x_b = 0; _x_e = x_e;
            _y_b = 0; _y_e = y_e;
        }

        public Game_ground(int x_b, int x_e, int y_b, int y_e) 
        {
            _x_b = x_b;
            _x_e = x_e;
            _y_b = y_b;
            _y_e = y_e;
        }

        public int _x_b { get; set; } // ось Х начало
        public int _x_e { get; set; } // ось Х конец
        public int _y_b { get; set; } // ось У начало
        public int _y_e { get; set; } // ось У конец
    }


}
