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
                case Keys.Left:
                case Keys.A:
                case Keys.Right:
                case Keys.D:
                    game.player.dx = 0;
                    break;

                case Keys.Down:
                case Keys.S:
                case Keys.Up:
                case Keys.W:
                    //game.isGoing = false;
                    //game.player.Stop();
                    game.player.dy = 0;
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
        

        public static Dictionary<ElementType, Dictionary<ElementType, double>> elInterac = new Dictionary<ElementType, Dictionary<ElementType, double>>
        {
            [ElementType.Fire] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 0.0,
                [ElementType.Water] = 0.2,
                [ElementType.Earth] = 0.8,
                [ElementType.Wind] = 0.7,
                [ElementType.Lightning] = 1.1,
                [ElementType.Cold] = 1.5
            },
            [ElementType.Water] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1.5,
                [ElementType.Water] = 0.0,
                [ElementType.Earth] = 0.3,
                [ElementType.Wind] = 0.5,
                [ElementType.Lightning] = 1.5,
                [ElementType.Cold] = 0.5
            },
            [ElementType.Earth] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1.2,
                [ElementType.Water] = 0.4,
                [ElementType.Earth] = 0.0,
                [ElementType.Wind] = 0.5,
                [ElementType.Lightning] = 1.4,
                [ElementType.Cold] = 1
            },
            [ElementType.Wind] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1,
                [ElementType.Water] = 0.7,
                [ElementType.Earth] = 0.4,
                [ElementType.Wind] = 0.0,
                [ElementType.Lightning] = 0.7,
                [ElementType.Cold] = 0.7
            },
            [ElementType.Lightning] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1.0,
                [ElementType.Water] = 1.5,
                [ElementType.Earth] = 1.0,
                [ElementType.Wind] = 0.7,
                [ElementType.Lightning] = 0.0,
                [ElementType.Cold] = 0.8
            },
            [ElementType.Cold] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1.5,
                [ElementType.Water] = 1,
                [ElementType.Earth] = 0.5,
                [ElementType.Wind] = 0.6,
                [ElementType.Lightning] = 0.8,
                [ElementType.Cold] = 0.0
            }
        };
    }
}
