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
        bool rot;

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

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = false;
                    break;
                case Keys.Right:
                    goRight = false;
                    break;
                case Keys.Down:
                    goDown = false;
                    break;
                case Keys.Up:
                    goUp = false;
                    break;
            }
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (goLeft)
                player.Sprite.Left -= 20;
            if (goRight)
                player.Sprite.Left += 20;
            if (goUp)
                player.Sprite.Top -= 20;
            if (goDown)
                player.Sprite.Top += 20;

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = true;
                    break;
                case Keys.Right:
                    goRight = true;
                    break;
                case Keys.Down:
                    goDown = true;
                    break;
                case Keys.Up:
                    goUp = true;
                    break;
            }
        }
    }
}
