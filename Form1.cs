using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ElementalTanks
{
    public partial class Form1 : Form
    {
        private readonly Game game;
        private readonly Timer gameTimer;
        private Label score;
        private ProgressBar playerHealth;
        public Form1()
        {
            game = new Game();
            DoubleBuffered = true;
            InitializeComponent();

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
                    var spriteName = entity.GetType().Name + entity.Element.GetType().Name;
                    var sprite = (Image)Properties.Resources.ResourceManager.GetObject(spriteName, Properties.Resources.Culture);
                    sprite.RotateFlip(Game.SpriteRotations[entity.Direction]);
                    args.Graphics.DrawImage(sprite, entity.X, entity.Y, entity.Width, entity.Height);
                }
            };

            score = new Label
            {
                AutoSize = true,
                Text = "Очки: " + game.Score,
                ForeColor = Color.White,
                Font = new Font("Arial", 14.25F, FontStyle.Bold),
                Location = new Point(game.MapWidth / 2 - 100, 0)
            };
            playerHealth = new ProgressBar
            {
                Location = new Point(game.Player.X + game.Player.Width / 2, game.Player.X + game.Player.Width),
                Value = (int)Math.Round(game.Player.Health),
                Size = new Size(game.Player.Width, 10)
            };
            Controls.Add(score);
            Controls.Add(playerHealth);
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            game.Update();

            score.Text = "Очки: " + game.Score;
            playerHealth.Location = new Point(game.Player.X, game.Player.Y + game.Player.Height);
            if (game.Player.Health > 0)
                playerHealth.Value = (int)Math.Round(game.Player.Health);
            else
                playerHealth.Dispose();
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
                    game.Player.IsShooting = false;
                    if (game.Player.Element.Type == BulletType.Spray)
                        game.Deleted.Add(game.Entities.FirstOrDefault(en => en is Bullet && (en as Bullet).Sender is Player));
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
                    if (!game.Player.IsShooting)
                    {
                        game.Player.IsShooting = true;
                        game.Entities.Add(game.Player.Shoot());
                    }
                    break;
            }
        }
    }
}
