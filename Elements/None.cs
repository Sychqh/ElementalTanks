using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class None : IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get; }
        public BulletType Type { get; }

        public None()
        {
            BaseDamage = 5.0;
            Width = Height = 64;
            Type = BulletType.Projectile;
        }

        public double GetFinalDamage(IElement enemy) => BaseDamage;
    }
}
