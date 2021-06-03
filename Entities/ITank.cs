using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ElementalTanks
{
    public interface ITank : IEntity
    {
        public double Health { get; set; }
        public int MoveSpeed { get; set; }
        public Point GunPosition { get; }
        public int UpMovement { get; set; }
        public int DownMovement { get; set; }
        public int LeftMovement { get; set; }
        public int RightMovement { get; set; }

        public void Move(string direction);
        public void TakeDamage(Bullet bullet);
        public bool CanMove(IEntity[,] map);
    }
}
