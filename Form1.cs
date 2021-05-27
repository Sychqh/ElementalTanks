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
        public readonly Dictionary<IEntity, Image> sourceImages;

        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            rnd = new Random();

            sourceImages = new Dictionary<IEntity, Image>();
            game = new Game(this, sourceImages);
            foreach (var entity in game.entities)
            {
                var spriteName = entity.GetType().Name + entity.Element.ToString();
                sourceImages[entity] = (Image)Properties.Resources.ResourceManager.GetObject(spriteName, Properties.Resources.Culture);
            };

            gameTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            gameTimer.Start();
            gameTimer.Tick += game.MainTimerEvent;
            gameTimer.Tick += UpdateSpriteImages;
            gameTimer.Tick += (sender, args) => Invalidate();

            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            Paint += (sender, args) =>
            {
                foreach (var entity in game.entities)
                {
                    var sprite = (Image)sourceImages[entity].Clone();
                    sprite.RotateFlip(Game.SpriteRotations[entity.Direction]);
                    args.Graphics.DrawImage(sprite, entity.X, entity.Y, sprite.Size.Width, sprite.Size.Height);
                }
            };
        }

        private void UpdateSpriteImages(object sender, EventArgs e)
        {
            foreach (var entity in game.entities)
            {
                if (!sourceImages.ContainsKey(entity))
                {
                    var spriteName = entity.GetType().Name + entity.Element.ToString();
                    sourceImages[entity] = (Image)Properties.Resources.ResourceManager.GetObject(spriteName, Properties.Resources.Culture);
                }
            };

            if (game.deleted != null)
                foreach (var enitity in game.deleted)
                    sourceImages.Remove(enitity);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    game.player.UpMovement = 0;
                    break;

                case Keys.Down:
                case Keys.S:
                    game.player.DownMovement = 0;
                    break;

                case Keys.Left:
                case Keys.A:
                    game.player.LeftMovement = 0;
                    break;

                case Keys.Right:
                case Keys.D:
                    game.player.RightMovement = 0;
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
                    game.player.Move("Left");
                    break;

                case Keys.Right:
                case Keys.D:
                    game.player.Move("Right");
                    break;

                case Keys.Down:
                case Keys.S:
                    game.player.Move("Down");
                    break;

                case Keys.Up:
                case Keys.W:
                    game.player.Move("Up");
                    break;

                case Keys.Space:
                    //Shoot();
                    break;
            }
        }
    }
}
