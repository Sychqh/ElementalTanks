using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public interface IEntity
    {
        public IElement Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public void Update();
    }
}
