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
        private readonly List<Bullet> bullets;

        private int score;
        private readonly Timer gameTimer;
        private bool isGoing;
        private Random rnd;

        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            rnd = new Random();

            player = new Tank(1, 300, 300, ElementType.Fire, 10);
            enemies = new List<Tank>
            {
                new Tank(2, 100, 100, ElementType.Fire, 5),
                new Tank(2, 400, 300, ElementType.Fire, 5),
                new Tank(2, 500, 100, ElementType.Fire, 5)
            };
            bullets = new List<Bullet>();

            gameTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            gameTimer.Tick += MainTimerEvent;
            gameTimer.Start();

            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;

            Paint += (sender, args) =>
            {
                args.Graphics.DrawImage(player.Sprite, player.X, player.Y, player.Sprite.Size.Width, player.Sprite.Size.Height);
                foreach (var enemy in enemies)
                    args.Graphics.DrawImage(enemy.Sprite, enemy.X, enemy.Y, enemy.Sprite.Size.Width, enemy.Sprite.Size.Height);
                foreach(var bullet in bullets)
                    args.Graphics.DrawImage(bullet.Sprite, bullet.X, bullet.Y, bullet.Sprite.Size.Width, bullet.Sprite.Size.Height);
            };
        }


        private void MainTimerEvent(object sender, EventArgs e)
        {
            player.Rotate();
            Invalidate();

            if (isGoing && player.IsInBounds(this))
            {
                player.X += player.MovementForDirection[player.Direction].Item1;
                player.Y += player.MovementForDirection[player.Direction].Item2;
            }
            
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
            var bullet = new Bullet(player.Element, 1, rnd.Next(0, 800), rnd.Next(0, 600));
            bullets.Add(bullet);
        }
    }
}
