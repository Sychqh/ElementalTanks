using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace ElementalTanks
{
    class Enemy : ITank
    {
        public double Health { get; set; }
        public int MoveSpeed { get; set; }
        public IElement Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int UpMovement { get; set; }
        public int DownMovement { get; set; }
        public int LeftMovement { get; set; }
        public int RightMovement { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private readonly Player player;
        public Point GunPosition => Direction switch
        {
            "Up" => new Point(X + 6, Y - Element.Height + 6),
            "Down" => new Point(X + 6, Y + Height - 6),
            "Left" => new Point(X - Element.Width + 6, Y + 6),
            "Right" => new Point(X + Width - 6, Y + 6),
            _ => Point.Empty
        };

        public Enemy(int x, int y, IElement element, Player player, int moveSpeed)
        {
            this.player = player;
            X = x;
            Y = y;
            Width = Height = 80;
            Element = element;
            Health = 100;
            MoveSpeed = moveSpeed;
            Direction = "Up";
        }

        public void Update()
        {
            Move(Direction);
            X += RightMovement - LeftMovement;
            Y += DownMovement - UpMovement;
        }

        public void Move(string direction)
        {
            if (Y > player.Y)
            {
                Direction = "Up";
                UpMovement = MoveSpeed;
                DownMovement = LeftMovement = RightMovement = 0;
            }
            if (Y < player.Y)
            {
                Direction = "Down";
                DownMovement = MoveSpeed;
                UpMovement = LeftMovement = RightMovement = 0;
            }
            if (X > player.X)
            {
                Direction = "Left";
                LeftMovement = MoveSpeed;
                UpMovement = DownMovement = RightMovement = 0;
            }
            if (X < player.X)
            {
                Direction = "Right";
                RightMovement = MoveSpeed;
                UpMovement = DownMovement = LeftMovement = 0;
            }
        }

        public void MoveBack()
        {
            X -= RightMovement - LeftMovement;
            Y -= DownMovement - UpMovement;
        }

        public void TakeDamage(Bullet bullet)
        {
            if (bullet.Sender is Player)
                Health -= bullet.Element.GetFinalDamage(Element);
        }
    }
}
