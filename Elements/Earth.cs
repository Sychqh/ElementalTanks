using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class Earth : IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get; }
        public BulletType Type { get; }

        public Earth()
        {
            BaseDamage = 30.0;
            Width = Height = 64;
            Type = BulletType.Projectile;
        }

        public double GetFinalDamage(IElement enemy)
        {
            return enemy.GetType().Name switch
            {
                "Fire" => 1.2 * BaseDamage,
                "Water" => 0.4 * BaseDamage,
                "Earth" => 0.0,
                "Wind" => 0.5 * BaseDamage,
                "Lightning" => 1.4 * BaseDamage,
                "Cold" => 1 * BaseDamage,
                _ => BaseDamage,
            };
        }
    }
}
