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
        private bool rotated;
        private bool goLeft, goRight, goUp, goDown;

        public Form1()
        {
            InitializeComponent();

            gameTimer = new Timer
            {
                Enabled = true,
                Interval = 20,
            };
            gameTimer.Tick += MainTimerEvent;
            gameTimer.Start();

            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            Controls.Add(player.Sprite);
        }

        private void MainTimerEvent(object sender, EventArgs e)
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

            if (goLeft)
                player.Sprite.Left -= player.MoveSpeed;
            if (goRight)
                player.Sprite.Left += player.MoveSpeed;
            if (goUp)
                player.Sprite.Top -= player.MoveSpeed;
            if (goDown)
                player.Sprite.Top += player.MoveSpeed;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = false;
                    break;
                case Keys.Right:
                    goRight = false;
                    rotated = true;
                    break;
                case Keys.Down:
                    goDown = false;
                    break;
                case Keys.Up:
                    goUp = false;
                    break;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = true;
                    player.Direction = "Left";
                    break;
                case Keys.Right:
                    goRight = true;
                    player.Direction = "Right";
                    break;
                case Keys.Down:
                    goDown = true; 
                    player.Direction = "Down";
                    break;
                case Keys.Up:
                    goUp = true; 
                    player.Direction = "Up";
                    break;
            }
        }
    }
}
