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

       

        public int dx;
        public int dy;

        public Player(int x, int y, ElementType element)
        {
            X = x;
            Y = y;
            Element = element;
            Health = 100;
            MoveSpeed = 5;
            Direction = "Up";
            dx = dy = 0;
        }

        public void Update()
        {
            X += dx;
            Y += dy;
        }

        public void Move(string direction)
        {
            Direction = direction;

            dx = Game.MovementForDirection[direction].X * MoveSpeed;
            dy = Game.MovementForDirection[direction].Y * MoveSpeed;
        }

        public void MoveBack()
        {
            X -= dx;
            Y -= dy;
        }
        //public bool IsInBounds(Form form)
        //{
        //    return Direction switch
        //    {
        //        "Left" => X > 1,
        //        "Up" => Y > 1,
        //        "Right" => X + form..Width < form.ClientSize.Width - 1,
        //        "Down" => Y + Sprite.Height < form.ClientSize.Height,
        //        _ => true,
        //    };
        //}
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
