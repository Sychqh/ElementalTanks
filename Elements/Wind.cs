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

        public Wind()
        {
            BaseDamage = 7.0;
            Width = 63;
            Height = 84;
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
