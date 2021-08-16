
namespace Corsaries_by_VBUteamGKMI.View
{
    partial class Abordage_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.log = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cb_deff = new System.Windows.Forms.ComboBox();
            this.cb_attack = new System.Windows.Forms.ComboBox();
            this.btn_Attack = new System.Windows.Forms.Button();
            this.Enemy_HP_bar = new System.Windows.Forms.ProgressBar();
            this.Enemy_HP = new System.Windows.Forms.Label();
            this.Enemy_creed = new System.Windows.Forms.Label();
            this.Enemy_dodge = new System.Windows.Forms.Label();
            this.Enemy_deff = new System.Windows.Forms.Label();
            this.Enemy_dmg = new System.Windows.Forms.Label();
            this.My_HP = new System.Windows.Forms.Label();
            this.My_Hp_bar = new System.Windows.Forms.ProgressBar();
            this.My_creed = new System.Windows.Forms.Label();
            this.My_deff = new System.Windows.Forms.Label();
            this.My_damag = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_deff2 = new System.Windows.Forms.ComboBox();
            this.My_dodge = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(27, 281);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(748, 157);
            this.log.TabIndex = 74;
            this.log.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(280, 56);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 15);
            this.label16.TabIndex = 73;
            this.label16.Text = "Атаковать :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(279, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 15);
            this.label15.TabIndex = 72;
            this.label15.Text = "Защищать:";
            // 
            // cb_deff
            // 
            this.cb_deff.FormattingEnabled = true;
            this.cb_deff.Items.AddRange(new object[] {
            "Голова",
            "Грудь",
            "Пах",
            "Ноги"});
            this.cb_deff.Location = new System.Drawing.Point(390, 94);
            this.cb_deff.Name = "cb_deff";
            this.cb_deff.Size = new System.Drawing.Size(121, 23);
            this.cb_deff.TabIndex = 71;
            // 
            // cb_attack
            // 
            this.cb_attack.FormattingEnabled = true;
            this.cb_attack.Items.AddRange(new object[] {
            "Голова",
            "Грудь",
            "Пах",
            "Ноги"});
            this.cb_attack.Location = new System.Drawing.Point(390, 53);
            this.cb_attack.Name = "cb_attack";
            this.cb_attack.Size = new System.Drawing.Size(121, 23);
            this.cb_attack.TabIndex = 70;
            // 
            // btn_Attack
            // 
            this.btn_Attack.Location = new System.Drawing.Point(269, 195);
            this.btn_Attack.Name = "btn_Attack";
            this.btn_Attack.Size = new System.Drawing.Size(285, 23);
            this.btn_Attack.TabIndex = 69;
            this.btn_Attack.Text = "Атаковать";
            this.btn_Attack.UseVisualStyleBackColor = true;
            // 
            // Enemy_HP_bar
            // 
            this.Enemy_HP_bar.Location = new System.Drawing.Point(534, 36);
            this.Enemy_HP_bar.Name = "Enemy_HP_bar";
            this.Enemy_HP_bar.Size = new System.Drawing.Size(161, 23);
            this.Enemy_HP_bar.TabIndex = 68;
            // 
            // Enemy_HP
            // 
            this.Enemy_HP.AutoSize = true;
            this.Enemy_HP.Location = new System.Drawing.Point(607, 9);
            this.Enemy_HP.Name = "Enemy_HP";
            this.Enemy_HP.Size = new System.Drawing.Size(34, 15);
            this.Enemy_HP.TabIndex = 67;
            this.Enemy_HP.Text = "none";
            // 
            // Enemy_creed
            // 
            this.Enemy_creed.AutoSize = true;
            this.Enemy_creed.Location = new System.Drawing.Point(607, 166);
            this.Enemy_creed.Name = "Enemy_creed";
            this.Enemy_creed.Size = new System.Drawing.Size(34, 15);
            this.Enemy_creed.TabIndex = 65;
            this.Enemy_creed.Text = "none";
            // 
            // Enemy_dodge
            // 
            this.Enemy_dodge.AutoSize = true;
            this.Enemy_dodge.Location = new System.Drawing.Point(607, 131);
            this.Enemy_dodge.Name = "Enemy_dodge";
            this.Enemy_dodge.Size = new System.Drawing.Size(34, 15);
            this.Enemy_dodge.TabIndex = 64;
            this.Enemy_dodge.Text = "none";
            // 
            // Enemy_deff
            // 
            this.Enemy_deff.AutoSize = true;
            this.Enemy_deff.Location = new System.Drawing.Point(607, 103);
            this.Enemy_deff.Name = "Enemy_deff";
            this.Enemy_deff.Size = new System.Drawing.Size(34, 15);
            this.Enemy_deff.TabIndex = 63;
            this.Enemy_deff.Text = "none";
            // 
            // Enemy_dmg
            // 
            this.Enemy_dmg.AutoSize = true;
            this.Enemy_dmg.Location = new System.Drawing.Point(607, 71);
            this.Enemy_dmg.Name = "Enemy_dmg";
            this.Enemy_dmg.Size = new System.Drawing.Size(34, 15);
            this.Enemy_dmg.TabIndex = 62;
            this.Enemy_dmg.Text = "none";
            // 
            // My_HP
            // 
            this.My_HP.AutoSize = true;
            this.My_HP.Location = new System.Drawing.Point(147, 9);
            this.My_HP.Name = "My_HP";
            this.My_HP.Size = new System.Drawing.Size(34, 15);
            this.My_HP.TabIndex = 60;
            this.My_HP.Text = "none";
           
            // 
            // My_Hp_bar
            // 
            this.My_Hp_bar.Location = new System.Drawing.Point(95, 32);
            this.My_Hp_bar.Name = "My_Hp_bar";
            this.My_Hp_bar.Size = new System.Drawing.Size(161, 23);
            this.My_Hp_bar.TabIndex = 59;
          
            // 
            // My_creed
            // 
            this.My_creed.AutoSize = true;
            this.My_creed.Location = new System.Drawing.Point(146, 166);
            this.My_creed.Name = "My_creed";
            this.My_creed.Size = new System.Drawing.Size(34, 15);
            this.My_creed.TabIndex = 57;
            this.My_creed.Text = "none";
          
            // 
            // My_deff
            // 
            this.My_deff.AutoSize = true;
            this.My_deff.Location = new System.Drawing.Point(146, 103);
            this.My_deff.Name = "My_deff";
            this.My_deff.Size = new System.Drawing.Size(34, 15);
            this.My_deff.TabIndex = 55;
            this.My_deff.Text = "none";
          
            // 
            // My_damag
            // 
            this.My_damag.AutoSize = true;
            this.My_damag.Location = new System.Drawing.Point(147, 71);
            this.My_damag.Name = "My_damag";
            this.My_damag.Size = new System.Drawing.Size(34, 15);
            this.My_damag.TabIndex = 54;
            this.My_damag.Text = "none";
         
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(717, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "Здоровье";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(717, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 15);
            this.label9.TabIndex = 51;
            this.label9.Text = "Урон ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(714, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 15);
            this.label10.TabIndex = 50;
            this.label10.Text = "Защита %";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(685, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 15);
            this.label11.TabIndex = 49;
            this.label11.Text = "Шанс уклонения %";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(685, 166);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 15);
            this.label12.TabIndex = 48;
            this.label12.Text = "Шанс крит удара %";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 15);
            this.label8.TabIndex = 45;
            this.label8.Text = "Здоровье";
       
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 15);
            this.label7.TabIndex = 44;
            this.label7.Text = "Урон ";
        
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 15);
            this.label6.TabIndex = 43;
            this.label6.Text = "Защита %";
         
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 42;
            this.label5.Text = "Шанс уклонения %";
          
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 41;
            this.label4.Text = "Шанс крит удара %";
      
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 76;
            this.label1.Text = "Защищать:";
            // 
            // cb_deff2
            // 
            this.cb_deff2.FormattingEnabled = true;
            this.cb_deff2.Items.AddRange(new object[] {
            "Голова",
            "Грудь",
            "Пах",
            "Ноги"});
            this.cb_deff2.Location = new System.Drawing.Point(390, 131);
            this.cb_deff2.Name = "cb_deff2";
            this.cb_deff2.Size = new System.Drawing.Size(121, 23);
            this.cb_deff2.TabIndex = 75;
            // 
            // My_dodge
            // 
            this.My_dodge.AutoSize = true;
            this.My_dodge.Location = new System.Drawing.Point(146, 131);
            this.My_dodge.Name = "My_dodge";
            this.My_dodge.Size = new System.Drawing.Size(34, 15);
            this.My_dodge.TabIndex = 77;
            this.My_dodge.Text = "none";
       
            // 
            // Abordage_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.My_dodge);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_deff2);
            this.Controls.Add(this.log);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cb_deff);
            this.Controls.Add(this.cb_attack);
            this.Controls.Add(this.btn_Attack);
            this.Controls.Add(this.Enemy_HP_bar);
            this.Controls.Add(this.Enemy_HP);
            this.Controls.Add(this.Enemy_creed);
            this.Controls.Add(this.Enemy_dodge);
            this.Controls.Add(this.Enemy_deff);
            this.Controls.Add(this.Enemy_dmg);
            this.Controls.Add(this.My_HP);
            this.Controls.Add(this.My_Hp_bar);
            this.Controls.Add(this.My_creed);
            this.Controls.Add(this.My_deff);
            this.Controls.Add(this.My_damag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Abordage_Form";
            this.Text = "Abordage_Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cb_deff;
        private System.Windows.Forms.ComboBox cb_attack;
        private System.Windows.Forms.Button btn_Attack;
        private System.Windows.Forms.ProgressBar Enemy_HP_bar;
        private System.Windows.Forms.Label Enemy_HP;
        private System.Windows.Forms.Label Enemy_creed;
        private System.Windows.Forms.Label Enemy_dodge;
        private System.Windows.Forms.Label Enemy_deff;
        private System.Windows.Forms.Label Enemy_dmg;
        private System.Windows.Forms.Label My_HP;
        private System.Windows.Forms.ProgressBar My_Hp_bar;
        private System.Windows.Forms.Label My_creed;
        private System.Windows.Forms.Label my_Ship_count_cannon;
        private System.Windows.Forms.Label My_deff;
        private System.Windows.Forms.Label My_damag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_deff2;
        private System.Windows.Forms.Label My_dodge;
    }
}