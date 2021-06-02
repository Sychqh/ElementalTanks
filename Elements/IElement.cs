using System;
using System.Collections.Generic;
using System.Text;

namespace ElementalTanks
{
    public interface IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get; }

        public double GetFinalDamage(IElement enemy);
    }
}
