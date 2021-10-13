using Corsaries_by_VBUteamGKMI.Model;
using Corsaries_by_VBUteamGKMI.Model.People_on_ship;
using Corsaries_by_VBUteamGKMI.Model.Products;
using Corsaries_by_VBUteamGKMI.Model.Save;
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
        // переменная константа количества нпс на карте
        public const int _nps_count = 50;
        // переменная для хранения игровой даты
        Save _current_save = null;
        DateTime _gameTime = new DateTime(1500, 1, 1);
        SpriteFont _sprite_gameTime; // игровая дата спрайт
        Vector2 _sprite_gameTime_pos; //игровая дата  позиция
        // таймер для игрового времени
        public System.Windows.Forms.Timer _gameTime_timer = new System.Windows.Forms.Timer();
        // мои снаряды
        public static List<Cannonball> _my_cannonballs = new List<Cannonball>();
        //снаряды врага
        public static List<Cannonball> _enemy_cannonballs = new List<Cannonball>();
        public Game_Sate _game_state; //состояние игры
        public static int _game_ground_X_Y = 5000; // размер карты     
        // размеры игровой карты
        public static Game_ground _game_ground = new Game_ground(_game_ground_X_Y, _game_ground_X_Y);
        public static List<NPS_Ship> _nps = new List<NPS_Ship>(); // коллекция нпс
        //таймер смены направления движения нпс0
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        Vector2 _pos_in_world;
        SpriteFont _coordinates; // координаты спрайт
        Vector2 _coordinates_pos; //координаты  позиция
        private List<Island> _islands = new List<Island>(); // коллекция островов
        private List<Seaport> _seaports = new List<Seaport>(); // коллекция портов
        private List<Vector2> _island_positions = new List<Vector2>() { new Vector2(1000, 2500), new Vector2(2000, 1500), new Vector2(3000, 1500), new Vector2(4500, 2500), new Vector2(3500, 4000), new Vector2(2500, 4000) };
        private List<Vector2> _edge_island_positions = new List<Vector2>() { new Vector2(2000, 0), new Vector2(2500, 4500) };
        private List<Vector2> _continent_positions = new List<Vector2>() { new Vector2(_game_ground_X_Y - 500, 0), new Vector2(0,0), new Vector2(0, _game_ground_X_Y-500), new Vector2(_game_ground_X_Y - 500, _game_ground_X_Y - 500) };
        private List<Vector2> _seaport_positions = new List<Vector2>() { new Vector2(380, 30), new Vector2(210, 330), new Vector2(330,4670), new Vector2(90,4540), new Vector2(2380,-20), new Vector2(4580,40), new Vector2(4880,370), new Vector2(2830,4150), new Vector2(3240,1640), new Vector2(1280, 2750), new Vector2(2300, 1660), new Vector2(2500, 2250), new Vector2(2590, 4890), new Vector2(3800,4300), new Vector2(4550,4870), new Vector2(4790,2630), new Vector2(4700,4580)};
        // камера
        public  Camera2d _camera = new Camera2d();
        //текущий монитор   
        public static System.Drawing.Size _size_screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
        public static GraphicsDeviceManager _graphics; // графика
        private SpriteBatch _spriteBatch; // отрисовщик спрайтов
        private MyShip _myShip; // мой кораблик
        private NPS_Ship _enemyShip;

        private System.Media.SoundPlayer player;
        private System.Windows.Forms.Timer auto_saver = new System.Windows.Forms.Timer();

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

            auto_saver.Interval = 10000;
            auto_saver.Tick += Auto_saver_Tick;
            auto_saver.Start();

            // инициализируем камеру
            _camera.Pos = new Vector2(500.0f, 200.0f);

            // задаём размер игрового окна с отступами        
            _graphics.IsFullScreen = false;
            //
            _graphics.PreferredBackBufferHeight = _size_screen.Height;
            _graphics.PreferredBackBufferWidth = _size_screen.Width;
        }

        public Game1(Save current_save)
        {
            _current_save = current_save;
            _game_state = Game_Sate.In_World;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; // директория закачки контента
            IsMouseVisible = true; // видимость мышки
                                   // таймер 
            _timer.Interval = 3000;// раз в 10 сек
            _timer.Tick += _timer_Tick; // событие тика
            _timer.Start();

            auto_saver.Interval = 4000;
            auto_saver.Tick += Auto_saver_Tick;
            auto_saver.Start();

            // инициализируем камеру
            _camera.Pos = new Vector2(500.0f, 200.0f);

            // задаём размер игрового окна с отступами        
            _graphics.IsFullScreen = false;
            //
            _graphics.PreferredBackBufferHeight = _size_screen.Height;
            _graphics.PreferredBackBufferWidth = _size_screen.Width;
        }

        private void Auto_saver_Tick(object sender, EventArgs e)
        {
            SaveRepository.Save_Progress(new Save(_myShip, _gameTime));
        }

        // тик таймера изменение движения нпс
        private void _timer_Tick(object sender, System.EventArgs e) => _nps.ForEach(i => i.Next_Move());

        protected override void Initialize()
        {
            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Corsairs")){
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Corsairs");
            }

            base.Initialize();

            // игровой таймер
            _gameTime_timer.Interval = 60000;
            _gameTime_timer.Tick += _gameTime_timer_Tick;
            _gameTime_timer.Start();

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
            Random random = new Random();
            foreach (var item in _seaport_positions)
            {
                _seaports.Add(new Seaport(Content, $"port_{random.Next(1, 4)}", item));
            }
            if(_current_save == null)
            {
                _myShip = new MyShip(Ship_type.Corvette, Content, 500, 500);
            }
            else
            {
                _myShip = new MyShip(Ship_type.Boat, Content, 500, 500);
                _myShip._captain.FromString(_current_save.captain);
                _myShip._ship_type = (Ship_type)Enum.Parse(typeof(Ship_type),_current_save.ship_type);
                _myShip._products.Clear();
                foreach (var item in _current_save.products)
                {
                    _myShip._products.Add(Product.FromString(item));
                }
                _myShip._sailors.Clear();
                foreach (var item in _current_save.sailors)
                {
                    _myShip._sailors.Add(Sailor.FromString(item));
                }
                _myShip._name = _current_save.name;
                _myShip._price = _current_save.price;
                _myShip._current_count_sailors = _current_save.current_count_sailors;
                _myShip._max_count_sailors = _current_save.max_count_sailors;
                _myShip._max_capacity = _current_save.max_capacity;
                _myShip._max_hp = _current_save.max_hp;
                _myShip._current_hp = _current_save.current_hp;
                _myShip._speed = _current_save.speed;
                _myShip._cannon.FromString(_current_save.cannon);
                _myShip._count_cannon = _current_save.count_cannon;
                _myShip._protection = _current_save.protection;
                _myShip._dodge_chance = _current_save.dodge_chance;
                _myShip._position = new Vector2(_current_save.position_x, _current_save.position_y);
                _gameTime = DateTime.Parse(_current_save.gameTime);
            }

            // добавляем нпс
            _nps.Clear(); //очищаем коллекцию чтобы при перезапуске не становилось больше NPS
            for (int i = 0; i < _nps_count; i++)            
                _nps.Add(new NPS_Ship((Ship_type)new Random().Next(0, 7), Content));
            
    
            //  текст координат
            _coordinates = Content.Load<SpriteFont>("testtext");
            _sprite_gameTime = Content.Load<SpriteFont>("testtext");



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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // обязательный метод начала отрисовки в который передают камеру
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                        null, SamplerState.LinearWrap,null,null,null, _camera.get_transformation(GraphicsDevice));
            _spriteBatch.Draw(Content.Load<Texture2D>("water"), new Vector2(0,0), new Rectangle(0, 0, _game_ground_X_Y,_game_ground_X_Y),Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            _spriteBatch.End();
            _spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null,
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
            player.Stop();
            _myShip._position = _pos_in_world;
            // задаём размеры игрового поля 
            _game_ground = new Game_ground(_game_ground_X_Y, _game_ground_X_Y);
            // задаём состояние игры
            _game_state = Game_Sate.In_World;
            //запускаем таймер игрового времени
            _gameTime_timer.Start();
        }
        // установка стостояния игры в битве
        public void Set_In_Battle_GS()
        {
            player = new System.Media.SoundPlayer(Environment.CurrentDirectory + "\\Content\\snd\\battle.wav");
            player.PlayLooping();
            // задаём размеры боевого поля 
            _game_ground = new Game_ground(Convert.ToInt32(_camera._pos.X - (_size_screen.Width / (2*_camera.Zoom))),
                Convert.ToInt32(_camera._pos.X + (_size_screen.Width / (2 * _camera.Zoom))),
                Convert.ToInt32(_camera._pos.Y - (_size_screen.Height / (2 * _camera.Zoom))),
                Convert.ToInt32(_camera._pos.Y + (_size_screen.Height / (2 * _camera.Zoom))));
            // задаём состояние игры
            _game_state = Game_Sate.In_Battle;
            _my_hp_bar = new HP_Bar(GraphicsDevice, _myShip,Color.GreenYellow);
            _enemy_hp_bar = new HP_Bar(GraphicsDevice, _enemyShip,Color.Red);
            _my_sailor_bar = new Sailor_Bar(GraphicsDevice, _myShip,Color.Aqua);
            _enemy_sailor_bar = new Sailor_Bar(GraphicsDevice, _enemyShip,Color.Aqua);
            // чистим снаряды
            _enemy_cannonballs.Clear();
            _my_cannonballs.Clear();
            //останавливаем таймер игрового времени
            _gameTime_timer.Stop();

        }

        #region In_World            
        // метод изменения дня
        private void _gameTime_timer_Tick(object sender, EventArgs e)
        {
            _gameTime = _gameTime.AddDays(1);
            // кушаем еду
            try { _myShip.Food_consumption(); }

            catch (Exception ex)
            {
                if (_myShip._current_count_warning < MyShip._max_count_warning)
                    MessageBox.Show("У нас проблемы!", ex.Message, new List<string>() { "Ок" });
                else
                {
                    MessageBox.Show("У нас проблемы!", ex.Message, new List<string>() { "Ок" });
                    var answer =  MessageBox.Show("Игра окончена",
                        "Команда взбунтовалась против нас\r\n и выбросила нас за борт =(",
                        new List<string>() { "Перезапустить?","Выйти?" }).Result.Value;
                    if (answer == 0)
                        Initialize();
                    else
                        Exit();
                }
                _seaports.ForEach(i => i.SetPortState());
            }
        }
        // обновленние данных при состояние игры игровой мир
        private void In_World_Update(GameTime gameTime)
        {           
            // кнопка выхода
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // кнопка проверки своего судна
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Show_Info_Ship(_myShip);
            // задаём НПС позицию
            _nps.FindAll(i=> i._position.X==0&&i._position.Y==0)
                .ForEach(i => i.Set_Spawn_Position(_islands));

            // проверка столкнованеий моего корабля с островом
            if (Collision_island(_myShip))
                _myShip.Step_Back_Position(); // возвращение к старой позиции при столкновениее
            // проверка столкнованеий моего корабля с Морскими портами
            Collision_seaport(_myShip);
           
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
            // даём положение координатам на экране
            if(_size_screen.Width == 1366 && _size_screen.Height == 768)
            {
                _coordinates_pos.Y = _camera.Pos.Y - (190 * (2 / _camera.Zoom)) + 5;
                _coordinates_pos.X = _camera.Pos.X - (340 * (2 / _camera.Zoom)) + 50;
            }
            else if(_size_screen.Width == 1920 && _size_screen.Height == 1080)
            {
                _coordinates_pos.Y = _camera.Pos.Y - (270 * (2 / _camera.Zoom));
                _coordinates_pos.X = _camera.Pos.X - (480 * (2 / _camera.Zoom));
            }
            // даём положение игровому времени на экране
            if (_size_screen.Width == 1366 && _size_screen.Height == 768)
            {
                _sprite_gameTime_pos.Y = _camera.Pos.Y - (190 * (2 / _camera.Zoom));
                _sprite_gameTime_pos.X = _camera.Pos.X + (270 * (2 / _camera.Zoom));
            }
            else if (_size_screen.Width == 1920 && _size_screen.Height == 1080)
            {
                _sprite_gameTime_pos.Y = _camera.Pos.Y - (270 * (2 / _camera.Zoom));
                _sprite_gameTime_pos.X = _camera.Pos.X + (410 * (2 / _camera.Zoom));
            }
            // НПС ДВИЖЕНИЕ
            _nps.ForEach(i => i.Move_Random());
            //проверка количества нпс на карте 
            if (_nps.Count < _nps_count)
                AddNps();
            
        }
        // метод добавляющий недостающих нпс на карту
        private void AddNps()
        {
            for (int i = 0; i < _nps_count - _nps.Count; i++)
                _nps.Add(new NPS_Ship((Ship_type)new Random().Next(0, 7), Content));

        }

        // метод столкновения с морскими портами
        private void Collision_seaport(Ship ship)
        {
            if (ship._activity)
            {
                //создаём прямоугольник корабля 
                Rectangle R_ship = new Rectangle((int)ship._position.X, (int)ship._position.Y,
                   ship._current_sprite.Width, ship._current_sprite.Height);
                foreach (var item in _seaports)
                {
                    //создаём прямоугольник порта 
                    Rectangle R_seaport = new Rectangle((int)item._position.X, (int)item._position.Y,
                           item._current_sprite.Width, item._current_sprite.Height);
                    //бежим по колекции островов и проверяем на столкновение
                    if (R_ship.Intersects(R_seaport))
                    {
                        // коллекция вопросов при столкновении
                        List<string> questions = new List<string> { "Причалить", "Уплыть" };
                        int answer;
                        answer = MessageBox.Show("Земля!!!", "Выбирете действие", questions).Result.Value;
                        if (answer == 1)//если мы не хотим причаливать
                        {
                            ship._activity = false;
                            ship._timer_activity.Start();
                           
                        }
                        else 
                        {
                            _gameTime_timer.Stop(); // остановим время
                            Enter_Seaport(ship, item); // зайдем в порт
                            ship._activity = false; // выключим активность
                            ship._timer_activity.Start(); // запустим перезарядку активности
                            _gameTime_timer.Start(); // запустим игровое время
                        }
                    }
                }
            }
        }

        // отрисовка данных при состояние игры игровой мир
        private void In_World_Draw(GameTime gameTime)
        {
            // отрисовка островов   
            foreach (var item in _islands)
            {
                _spriteBatch.Draw(item._current_sprite, item._position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
                _seaports.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White));
                // отрисовка кораблей в цикле отрисовки островов что бы корабли рисовались поверх островов
                _spriteBatch.Draw(_myShip._current_sprite, _myShip._position, Color.White); // отрисовка корабля
                // отрисовка nps  
                _nps.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White));
                // рисуем координаты
                if (_myShip != null)
                {
                    _spriteBatch.DrawString(_coordinates, $" X:{(int)_myShip._position.X} Y:{ (int)_myShip._position.Y}",
                         _coordinates_pos, new Color(0, 0, 0));
                    _spriteBatch.Draw(Content.Load<Texture2D>("bar"), new Vector2(_coordinates_pos.X-50,_coordinates_pos.Y-5), Color.White);
                    string data = $"{_gameTime.Day}:{_gameTime.Month}:{_gameTime.Year}";
                    if (_gameTime.Day < 10)
                        data = $"0{_gameTime.Day}:{_gameTime.Month}:{_gameTime.Year}";
                    if (_gameTime.Month < 10)
                        data = $"{_gameTime.Day}:0{_gameTime.Month}:{_gameTime.Year}";
                    if (_gameTime.Day < 10 && _gameTime.Day < 10)
                        data = $"0{_gameTime.Day}:0{_gameTime.Month}:{_gameTime.Year}";
                    _spriteBatch.DrawString(_sprite_gameTime, data, _sprite_gameTime_pos, new Color(0, 0, 0));
                    _spriteBatch.Draw(Content.Load<Texture2D>("bar"), new Vector2(_sprite_gameTime_pos.X-100,_sprite_gameTime_pos.Y), Color.White);
                }
            }
            
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
                            if (color != System.Drawing.Color.FromArgb(0,0,0))
                            {
                                
                                collide = true;
                            }
                        }
                        catch (Exception) { return collide; }
                    }                 
                }
            }

            return collide;
        }
        // получение цвета пикселей острова для колизии с островами
        private System.Drawing.Color GetColorWaterIsland(Island island, int x, int y) => island._bitmap.GetPixel(x, y);
        // проверка столкновений с НПС
        protected void Collision_NPS(Ship ship)
        {
            if (ship._activity)
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
                        Rectangle nps = new Rectangle(Convert.ToInt32(item._position.X - (100 / _camera.Zoom)), Convert.ToInt32(item._position.Y - ((100 / _camera.Zoom))),
                            item._current_sprite.Width + Convert.ToInt32(100 / _camera.Zoom), item._current_sprite.Height + Convert.ToInt32(100 / _camera.Zoom));
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
                                _pos_in_world = _myShip._position;
                                _enemyShip = item;
                                // даём игре состояние битвы
                                Set_In_Battle_GS();

                            }
                            else //если форма была закрыта или предложение о бое было отклонено
                            {
                                ship._activity = false;
                                ship._timer_activity.Start();
                            }

                        }
                    }

                }
                catch (InvalidOperationException) { return; }

            }
        }
        // показать инфо о корадбях запуск формы
        private void Show_Info_Ship(Ship ship)=> new Info_Form(ship).ShowDialog();
        // метод входа в порт
        private void Enter_Seaport(Ship ship, Seaport seaport) => new SeaportView(ship, seaport).ShowDialog();

        #endregion

        #region In Battle

        public HP_Bar _my_hp_bar;
        public HP_Bar _enemy_hp_bar;
        public Sailor_Bar _my_sailor_bar;
        public Sailor_Bar _enemy_sailor_bar;


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
            _my_sailor_bar.Update();
            _enemy_sailor_bar.Update();


        }
        // отрисовка данных при состояние игры бой
        private void In_Battle_Draw(GameTime gameTime)
        {           

            _spriteBatch.Draw(_myShip._current_sprite, _myShip._position, Color.White); // отрисовка корабля
            _spriteBatch.Draw(_enemyShip._current_sprite, _enemyShip._position, Color.White); // отрисовка врага
            _my_cannonballs.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White)); // отрисовка снаряда 
            _enemy_cannonballs.ForEach(i => _spriteBatch.Draw(i._current_sprite, i._position, Color.White)); //  отрисовка снаряда врага                                                                              // отрисовка хп бара

            _my_hp_bar.Draw(_spriteBatch );
            _enemy_hp_bar.Draw(_spriteBatch );
            _my_sailor_bar.Draw(_spriteBatch );
            _enemy_sailor_bar.Draw(_spriteBatch );


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
                    _enemyShip.Move_Random();
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
                new Get_Loot_View(_myShip, _enemyShip).ShowDialog();
                rez = true;
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
                try { 
                    new Abordage_Form(My_ship, Enemy_ship).ShowDialog();                  
                }
                catch (Exception ex) { throw ex; }
                
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

            _enemyShip.Move_Random();
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
