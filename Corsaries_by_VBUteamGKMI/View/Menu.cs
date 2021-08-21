using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Media;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Corsaries_by_VBUteamGKMI.View
{
    public partial class Menu : Form
    {
        Button title;
        Button play_btn;
        Button continue_btn;
        Button exit_btn;
        PictureBox menu_list_bg;
        Image scroll_paper = Bitmap.FromFile(Environment.CurrentDirectory + "\\Content\\menu_list.png");
        SoundPlayer player;
        PrivateFontCollection font = new PrivateFontCollection();

        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, System.EventArgs e)
        {
            font.AddFontFile(Environment.CurrentDirectory + "\\Content\\dpcomic.ttf");
            player = new SoundPlayer(Environment.CurrentDirectory + "\\Content\\snd\\menu.wav");
            player.PlayLooping();

            this.BackgroundImage = Bitmap.FromFile(Environment.CurrentDirectory + "\\Content\\menu_bg.jpg");

            this.menu_list_bg = new PictureBox();
            this.menu_list_bg.Image = scroll_paper;
            this.menu_list_bg.Size = scroll_paper.Size;
            this.menu_list_bg.Location = new Point(this.Width / 2 - scroll_paper.Width / 2, this.Height / 2);
            this.menu_list_bg.BackColor = Color.Transparent;


            this.title = new Button();
            this.title.Font = new Font(font.Families[0], 60, FontStyle.Regular);
            this.title.ForeColor = Color.White;
            this.title.Text = "Corsairs";
            this.title.Size = new Size(310, 200);
            this.title.Location = new Point(menu_list_bg.Location.X + menu_list_bg.Width / 2 - title.Width / 2, menu_list_bg.Location.Y - title.Height);
            this.title.FlatStyle = FlatStyle.Flat;
            this.title.FlatAppearance.BorderSize = 0;
            this.title.FlatAppearance.BorderColor = Color.Empty;
            this.title.BackColor = Color.Transparent;
            this.title.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.title.FlatAppearance.MouseOverBackColor = Color.Transparent;

            this.play_btn = new Button();
            this.play_btn.Font = new Font(font.Families[0],20, FontStyle.Bold);
            this.play_btn.BackgroundImage = Bitmap.FromFile(Environment.CurrentDirectory + "\\Content\\btn_bg.png");
            this.play_btn.Text = "Новая игра";
            this.play_btn.Size = new Size(170, 60);
            this.play_btn.Location = new Point(menu_list_bg.Location.X + menu_list_bg.Width / 2 - play_btn.Width / 2, menu_list_bg.Location.Y + play_btn.Height + 20);
            this.play_btn.FlatStyle = FlatStyle.Flat;
            this.play_btn.FlatAppearance.BorderSize = 0;
            this.play_btn.FlatAppearance.BorderColor = Color.Empty;
            this.play_btn.Click += Play_btn_Click;

            this.continue_btn = new Button();
            this.continue_btn.Font = new Font(font.Families[0], 20, FontStyle.Bold);
            this.continue_btn.BackgroundImage = Bitmap.FromFile(Environment.CurrentDirectory + "\\Content\\btn_bg.png");
            this.continue_btn.Text = "Загрузить игру";
            this.continue_btn.Size = new Size(170, 60);
            this.continue_btn.Location = new Point(play_btn.Location.X, play_btn.Location.Y + play_btn.Height + 20);
            this.continue_btn.FlatStyle = FlatStyle.Flat;
            this.continue_btn.FlatAppearance.BorderSize = 0;
            this.continue_btn.FlatAppearance.BorderColor = Color.Empty;
            this.continue_btn.Click += Continue_btn_Click;

            this.exit_btn = new Button();
            this.exit_btn.Font = new Font(font.Families[0], 20, FontStyle.Bold);
            this.exit_btn.BackgroundImage = Bitmap.FromFile(Environment.CurrentDirectory + "\\Content\\btn_bg.png");
            this.exit_btn.Text = "Выйти из игры";
            this.exit_btn.Size = new Size(170, 60);
            this.exit_btn.Location = new Point(continue_btn.Location.X, continue_btn.Location.Y + continue_btn.Height + 20);
            this.exit_btn.FlatStyle = FlatStyle.Flat;
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.BorderColor = Color.Empty;
            this.exit_btn.Click += Exit_btn_Click; 

            this.Controls.AddRange( new[] { this.title, this.play_btn, this.continue_btn, this.exit_btn });
            this.Controls.Add(this.menu_list_bg);
        }

        private void Play_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            player.Stop();
            this.Close();     
        }

        private void Continue_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
