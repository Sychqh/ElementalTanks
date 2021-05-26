using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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

        public Player(int x, int y, ElementType element)
        {
            X = x;
            Y = y;
            Element = element;
            Health = 100;
            MoveSpeed = 20;
            Direction = "Up";
        }

        public void Update()
        {
            
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
