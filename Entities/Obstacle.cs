using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class Obstacle : IEntity
    {
        public IElement Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Obstacle(int x, int y, IElement element)
        {
            X = x;
            Y = y;
            Width = Height = 73;
            Element = element;
            Direction = "Up";
        }

        public void Update()
        {
            
        }
    }
}
