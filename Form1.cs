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
        private readonly Game game;
        private readonly Random rnd;
        private readonly Timer gameTimer;

        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            rnd = new Random();

            game = new Game(this);

            gameTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            gameTimer.Start();
            gameTimer.Tick += MainTimerEvent;

            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            Paint += (sender, args) =>
            {
                foreach (var entity in game.Entities)
                {
                    //var sprite = (Image)sourceImages[entity].Clone();
                    var spriteName = entity.GetType().Name + entity.Element.GetType().Name;
                    var sprite = (Image)Properties.Resources.ResourceManager.GetObject(spriteName, Properties.Resources.Culture);
                    sprite.RotateFlip(Game.SpriteRotations[entity.Direction]);
                    args.Graphics.DrawImage(sprite, entity.X, entity.Y, entity.Width, entity.Height);//sprite.Size.Width, sprite.Size.Height);
                }
            };
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            game.Update();
            Invalidate();
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    game.Player.UpMovement = 0;
                    break;

                case Keys.Down:
                case Keys.S:
                    game.Player.DownMovement = 0;
                    break;

                case Keys.Left:
                case Keys.A:
                    game.Player.LeftMovement = 0;
                    break;

                case Keys.Right:
                case Keys.D:
                    game.Player.RightMovement = 0;
                    break;


                case Keys.Space:
                    //bullets.Remove(bullets.First(b => b.Sender == player));
                    break;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    game.Player.Move("Left");
                    break;

                case Keys.Right:
                case Keys.D:
                    game.Player.Move("Right");
                    break;

                case Keys.Down:
                case Keys.S:
                    game.Player.Move("Down");
                    break;

                case Keys.Up:
                case Keys.W:
                    game.Player.Move("Up");
                    break;

                case Keys.Space:
                    //Shoot();
                    break;
            }
        }
    }
}
