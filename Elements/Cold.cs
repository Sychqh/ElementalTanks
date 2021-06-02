using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class Cold : IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get; }

        public Cold()
        {
            BaseDamage = 10.0;
            Width = 34;
            Height = 88;
        }

        public double GetFinalDamage(IElement enemy)
        {
            return enemy.GetType().Name switch
            {
                "Fire" => 1.5 * BaseDamage,
                "Water" => 1 * BaseDamage,
                "Earth" => 0.5 * BaseDamage,
                "Wind" => 0.6 * BaseDamage,
                "Lightning" => 0.8 * BaseDamage,
                "Cold" => 0.0,
                _ => BaseDamage,
            };
        }
    }
}
