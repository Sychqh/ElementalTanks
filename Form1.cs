using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementalTanks
{
    public partial class Form1 : Form
    {
        private readonly Tank player = new Tank();
        private readonly List<Tank> enemies;
        private int score;
        private Timer gameTimer;
        private bool goLeft, goRight, goUp, goDown;

        public Form1()
        {
            InitializeComponent();
            player = new Tank();
            enemies = new List<Tank>();

            gameTimer = new Timer
            {
                Enabled = true,
                Interval = 20
            };
            gameTimer.Tick += MainTimerEvent;
            gameTimer.Start();

            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            Controls.Add(player.Sprite);
           
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            UpdatePlayerRotation();

            if (goLeft && player.Sprite.Left > 1)
                player.Sprite.Left -= player.MoveSpeed;
            if (goRight && player.Sprite.Left + player.Sprite.Width < ClientSize.Width)
                player.Sprite.Left += player.MoveSpeed;
            if (goUp && player.Sprite.Top > 1)
                player.Sprite.Top -= player.MoveSpeed;
            if (goDown && player.Sprite.Top + player.Sprite.Height < ClientSize.Height)
                player.Sprite.Top += player.MoveSpeed;
        }

        private void UpdatePlayerRotation()
        {
            switch (player.Direction)
            {
                case "Up":
                    player.Sprite.Image = Properties.Resources.tank1;
                    break;
                case "Down":
                    player.Sprite.Image = Properties.Resources.tank1;
                    player.Sprite.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
                case "Left":
                    player.Sprite.Image = Properties.Resources.tank1;
                    player.Sprite.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case "Right":
                    player.Sprite.Image = Properties.Resources.tank1;
                    player.Sprite.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    goLeft = false;
                    break;
                case Keys.Right:
                case Keys.D:
                    goRight = false;
                    break;
                case Keys.Down:
                case Keys.S:
                    goDown = false;
                    break;
                case Keys.Up:
                case Keys.W:
                    goUp = false;
                    break;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    goLeft = true;
                    player.Direction = "Left";
                    break;

                case Keys.Right:
                case Keys.D:
                    goRight = true;
                    player.Direction = "Right";
                    break;

                case Keys.Down:
                case Keys.S:
                    goDown = true; 
                    player.Direction = "Down";
                    break;

                case Keys.Up:
                case Keys.W:
                    goUp = true; 
                    player.Direction = "Up";
                    break;

                case Keys.Space:
                    Shoot();
                    break;
            }
        }

        private void Shoot()
        {
            var shootEffect = player.ShootEffect;
            Controls.Add(shootEffect);
        }
    }
}
