using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ElementalTanks
{
    public interface ITank : IEntity
    {
        public int Health { get; set; }
        public int MoveSpeed { get; set; }
        public Point GunPosition(Image sprite);
    }
}
