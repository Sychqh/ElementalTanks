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
            MoveSpeed = 10;
            Direction = "Up";
        }

        public void Update()
        {
            X += RightMovement - LeftMovement;
            Y += DownMovement - UpMovement;
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

        public void MoveBack()
        {
            X -= RightMovement - LeftMovement;
            Y -= DownMovement - UpMovement;
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
    }
}
