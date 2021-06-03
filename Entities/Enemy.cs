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

        public void Update(IEntity[,] map)
        {
            Move(Direction);
            if (CanMove(map))
            {
                X += RightMovement - LeftMovement;
                Y += DownMovement - UpMovement;
            }
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

        public void TakeDamage(Bullet bullet)
        {
            if (bullet.Sender is Player)
                Health -= bullet.Element.GetFinalDamage(Element);
        }

        public bool CanMove(IEntity[,] map)
        {
            switch (Direction)
            {
                case "Left" when X > 0:
                    for (var y = Y; y < Y + Height; y++)
                        if (!(map[X - MoveSpeed, y] is null))
                            return false;
                    return true;
                case "Right" when X + Width + MoveSpeed < map.GetLength(0):
                    for (var y = Y; y < Y + Height; y++)
                        if (!(map[X + Width + MoveSpeed, y] is null))
                            return false;
                    return true;
                case "Up" when Y > 0:
                    for (var x = X; x < X + Width; x++)
                        if (!(map[x, Y - MoveSpeed] is null))
                            return false;
                    return true;
                case "Down" when Y + Height + MoveSpeed < map.GetLength(1):
                    for (var x = X; x < X + Width; x++)
                        if (!(map[x, Y + Width + MoveSpeed] is null))
                            return false;
                    return true;
                default:
                    return false;
            }
        }
    }
}
