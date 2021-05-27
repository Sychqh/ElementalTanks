using System.Collections.Generic;

namespace ElementalTanks
{
    public enum ElementType
    {
        Fire,
        Water,
        Earth,
        Wind,
        Lightning,
        Cold
    }

    public partial class Form1
    {
        public static Dictionary<ElementType, Dictionary<ElementType, double>> elInterac = new Dictionary<ElementType, Dictionary<ElementType, double>>
        {
            [ElementType.Fire] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 0.0,
                [ElementType.Water] = 0.2,
                [ElementType.Earth] = 0.8,
                [ElementType.Wind] = 0.7,
                [ElementType.Lightning] = 1.1,
                [ElementType.Cold] = 1.5
            },
            [ElementType.Water] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1.5,
                [ElementType.Water] = 0.0,
                [ElementType.Earth] = 0.3,
                [ElementType.Wind] = 0.5,
                [ElementType.Lightning] = 1.5,
                [ElementType.Cold] = 0.5
            },
            [ElementType.Earth] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1.2,
                [ElementType.Water] = 0.4,
                [ElementType.Earth] = 0.0,
                [ElementType.Wind] = 0.5,
                [ElementType.Lightning] = 1.4,
                [ElementType.Cold] = 1
            },
            [ElementType.Wind] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1,
                [ElementType.Water] = 0.7,
                [ElementType.Earth] = 0.4,
                [ElementType.Wind] = 0.0,
                [ElementType.Lightning] = 0.7,
                [ElementType.Cold] = 0.7
            },
            [ElementType.Lightning] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1.0,
                [ElementType.Water] = 1.5,
                [ElementType.Earth] = 1.0,
                [ElementType.Wind] = 0.7,
                [ElementType.Lightning] = 0.0,
                [ElementType.Cold] = 0.8
            },
            [ElementType.Cold] = new Dictionary<ElementType, double>
            {
                [ElementType.Fire] = 1.5,
                [ElementType.Water] = 1,
                [ElementType.Earth] = 0.5,
                [ElementType.Wind] = 0.6,
                [ElementType.Lightning] = 0.8,
                [ElementType.Cold] = 0.0
            }
        };
    }
};