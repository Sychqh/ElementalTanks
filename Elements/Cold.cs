namespace ElementalTanks
{
    public class Cold : IElement
    {
        public double BaseDamage { get; }
        public int Width { get; }
        public int Height { get; }
        public BulletType Type { get; }

        public Cold()
        {
            BaseDamage = 10.0;
            Width = Height = 64;
            Type = BulletType.Projectile;
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
