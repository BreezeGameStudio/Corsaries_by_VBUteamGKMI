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

        Button play_btn;
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

            this.play_btn = new Button();
            this.play_btn.Font = new Font(font.Families[0],40, FontStyle.Regular);
            this.play_btn.BackColor = Color.Empty;
            this.play_btn.Text = "play";
            this.play_btn.Size = new Size(170, 50);
            this.play_btn.Location = new Point(menu_list_bg.Location.X + menu_list_bg.Width / 2 - play_btn.Width / 2, menu_list_bg.Location.Y + menu_list_bg.Height / 2 - play_btn.Height / 2);
            this.play_btn.FlatStyle = FlatStyle.Flat;
            this.play_btn.FlatAppearance.BorderSize = 0;
            this.play_btn.FlatAppearance.BorderColor = Color.Empty;
            this.play_btn.Click += Play_btn_Click;

            this.Controls.Add(this.play_btn);
            this.Controls.Add(this.menu_list_bg);
        }

        private void Play_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();     
        }
    }
}
