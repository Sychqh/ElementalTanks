using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class Obstacle : IEntity
    {
        public ElementType Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Obstacle(int x, int y, ElementType element)
        {
            X = x;
            Y = y;
            Element = element;
            Direction = "Up";
        }

        public void Update()
        {
            
        }
    }
}
