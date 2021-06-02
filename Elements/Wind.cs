using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class Wind : IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get; }
        public BulletType Type { get; }

        public Wind()
        {
            BaseDamage = 7.0;
            Width = Height = 64;
            Type = BulletType.Spray;
        }

        public double GetFinalDamage(IElement enemy)
        {
            return enemy.GetType().Name switch
            {
                "Fire" => 1 * BaseDamage,
                "Water" => 0.7 * BaseDamage,
                "Earth" => 0.4 * BaseDamage,
                "Wind" => 0.0,
                "Lightning" => 0.7 * BaseDamage,
                "Cold" => 0.7 * BaseDamage,
                _ => BaseDamage,
            };
        }
    }
}
