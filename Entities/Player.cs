using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ElementalTanks
{
    public class Player : ITank
    {
        public IElement Element { get; set; }
        public double Health { get; set; }
        public int MoveSpeed { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int UpMovement { get; set; } 
        public int DownMovement { get; set; }
        public int LeftMovement { get; set; }
        public int RightMovement { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsShooting { get; set; }

        public Point GunPosition => Direction switch
        {
            "Up" => new Point(X + 6, Y - Element.Height + 6),
            "Down" => new Point(X + 6, Y + Height - 6),
            "Left" => new Point(X - Element.Width + 6, Y + 6),
            "Right" => new Point(X + Width - 6, Y + 6),
            _ => Point.Empty
        };

        public Player(int x, int y, IElement element)
        {
            X = x;
            Y = y;
            Width = Height = 80;
            Element = element;
            Health = 100;
            MoveSpeed = 5;
            Direction = "Up";
        }

        public void Update(IEntity[,] map)
        {
            if (CanMove(map))
            {
                X += RightMovement - LeftMovement;
                Y += DownMovement - UpMovement;
            }
        }

        public void Move(string direction)
        {
            Direction = direction;

            switch (Direction)
            {
                case "Up":
                    UpMovement = MoveSpeed;
                    DownMovement = LeftMovement = RightMovement = 0;
                    break;
                case "Down":
                    DownMovement = MoveSpeed;
                    UpMovement = LeftMovement = RightMovement = 0;
                    break;
                case "Left":
                    LeftMovement = MoveSpeed;
                    UpMovement = DownMovement = RightMovement = 0;
                    break;
                case "Right":
                    RightMovement = MoveSpeed;
                    UpMovement = DownMovement = LeftMovement = 0;
                    break;
            }
        }

        public Bullet Shoot()
        {
            return new Bullet(this);
        }

        public void TakeDamage(Bullet bullet)
        {
            if (bullet.Sender is Enemy)
                Health -= bullet.Element.GetFinalDamage(Element);
        }

        public bool CanMove1(IEntity[,] map)
        {
            return Direction switch
            {
                "Left" => X > 0 && map[X - MoveSpeed, Y + Height / 2] is null,
                "Right" => X + Width + MoveSpeed < map.GetLength(0) && map[X + Width + MoveSpeed, Y + Height / 2] is null,
                "Up" => Y > 0 && map[X + Width / 2, Y - MoveSpeed] is null,
                "Down" => Y + Height + MoveSpeed < map.GetLength(1) && map[X + Width / 2, Y + Width + MoveSpeed] is null,
                _ => false
            };
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
