using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class Fire : IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get;}

        public Fire()
        {
            BaseDamage = 10.0;
            Width = 73;
            Height = 88;
        }

        public double GetFinalDamage(IElement enemy)
        {
            return enemy.GetType().Name switch
            {
                "Fire" => 0.0,
                "Water" => 0.2 * BaseDamage,
                "Earth" => 0.8 * BaseDamage,
                "Wind" => 0.7 * BaseDamage,
                "Lightning" => 1.1 * BaseDamage,
                "Cold" => 1.5 * BaseDamage,
                _ => BaseDamage,
            };
        }
    }
}
