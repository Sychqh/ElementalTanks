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
        private readonly Tank player;
        private readonly List<Tank> enemies;
        private int score;
        private readonly Timer gameTimer;
        private bool isGoing = false;

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

            if (isGoing && player.IsInBounds(this))
            {
                player.X += player.MovementForDirection[player.Direction].Item1;
                player.Y += player.MovementForDirection[player.Direction].Item2;
            }
        }   

        private void UpdatePlayerRotation()
        {
            player.Sprite = Properties.Resources.tank1;
            player.Sprite.RotateFlip(player.Rotations[player.Direction]);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                case Keys.Right:
                case Keys.D:
                case Keys.Down:
                case Keys.S:
                case Keys.Up:
                case Keys.W:
                    isGoing = false;
                    break;

            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    isGoing = true;
                    player.Direction = "Left";
                    break;

                case Keys.Right:
                case Keys.D:
                    isGoing = true;
                    player.Direction = "Right";
                    break;

                case Keys.Down:
                case Keys.S:
                    isGoing = true; 
                    player.Direction = "Down";
                    break;

                case Keys.Up:
                case Keys.W:
                    isGoing = true; 
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
