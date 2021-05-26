using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ElementalTanks
{
    class Enemy : ITank
    {
        public int Health { get; set; }
        public int MoveSpeed { get; set; }
        public ElementType Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Enemy(int x, int y, ElementType element)
        {
            X = x;
            Y = y;
            Element = element;
            Health = 100;
            MoveSpeed = 20;
            Direction = "Up";
        }
        public Point GunPosition(Image sprite)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            
        }
    }
}
