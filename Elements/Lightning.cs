using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public class Lightning : IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get; }

        public Lightning()
        {
            BaseDamage = 15.0;
            Width = 48;
            Height = 100;
        }

        public double GetFinalDamage(IElement enemy)
        {
            return enemy.GetType().Name switch
            {
                "Fire" => 1.0 * BaseDamage,
                "Water" => 1.5 * BaseDamage,
                "Earth" => 1.0 * BaseDamage,
                "Wind" => 0.7 * BaseDamage,
                "Lightning" => 0.0,
                "Cold" => 0.8 * BaseDamage,
                _ => BaseDamage,
            };
        }
    }
}
