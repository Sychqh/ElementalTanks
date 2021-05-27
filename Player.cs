using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ElementalTanks
{
    public class Player : ITank
    {
        public ElementType Element { get; set; }
        public int Health { get; set; }
        public int MoveSpeed { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int UpMovement { get; set; } 
        public int DownMovement { get; set; }
        public int LeftMovement { get; set; }
        public int RightMovement { get; set; }

        public Player(int x, int y, ElementType element)
        {
            X = x;
            Y = y;
            Element = element;
            Health = 100;
            MoveSpeed = 5;
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

        public Point GunPosition(Image sprite)
        {
            return Direction switch
            {
                "Up" => new Point(X, Y - sprite.Height),
                "Down" => new Point(X, Y + sprite.Height),
                "Left" => new Point(X - sprite.Width, Y),
                "Right" => new Point(X + sprite.Width, Y),
                _ => Point.Empty
            };
        }
    }
}
