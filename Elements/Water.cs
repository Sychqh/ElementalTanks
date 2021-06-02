using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class Water : IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get; }
        public BulletType Type { get; }

        public Water()
        {
            BaseDamage = 5.0;
            Width = Height = 64;
            Type = BulletType.Spray;
        }

        public double GetFinalDamage(IElement enemy)
        {
            return enemy.GetType().Name switch
            {
                "Fire" => 1.5 * BaseDamage,
                "Water" => 0.0,
                "Earth" => 0.5 * BaseDamage,
                "Wind" => 0.6 * BaseDamage,
                "Lightning" => 1.5 * BaseDamage,
                "Cold" => 0.5 * BaseDamage,
                _ => BaseDamage,
            };
        }
    }
}
