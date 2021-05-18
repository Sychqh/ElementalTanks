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
            DoubleBuffered = true;
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

            Paint += (sender, args) =>
            {
                args.Graphics.DrawImage(player.Sprite, player.X, player.Y, player.Sprite.Size.Width, player.Sprite.Size.Height);
            };
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            UpdatePlayerRotation();
            Invalidate();

            if (goLeft && player.X > 1)
                player.X -= player.MoveSpeed;
            if (goRight && player.X + player.Sprite.Width < ClientSize.Width)
                player.X += player.MoveSpeed;
            if (goUp && player.Y > 1)
                player.Y -= player.MoveSpeed;
            if (goDown && player.Y + player.Sprite.Height < ClientSize.Height)
                player.Y += player.MoveSpeed;
        }

        private void UpdatePlayerRotation()
        {
            switch (player.Direction)
            {
                case "Up":
                    player.Sprite = Properties.Resources.tank1;
                    break;
                case "Down":
                    player.Sprite = Properties.Resources.tank1;
                    player.Sprite.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
                case "Left":
                    player.Sprite = Properties.Resources.tank1;
                    player.Sprite.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case "Right":
                    player.Sprite = Properties.Resources.tank1;
                    player.Sprite.RotateFlip(RotateFlipType.Rotate90FlipNone);
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
