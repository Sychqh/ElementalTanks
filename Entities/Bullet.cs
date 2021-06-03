namespace ElementalTanks
{
    public class Bullet : IEntity
    {
        public IElement Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; }
        public int Height { get; }

        public int MoveSpeed { get; set; }
        public ITank Sender { get; }

        public Bullet(ITank sender)
        {
            Sender = sender;
            Element = sender.Element;
            X = sender.GunPosition.X;
            Y = sender.GunPosition.Y;
            Direction = sender.Direction;
            Width = Height = 64;
            MoveSpeed = 10;
        }

        public void Update(IEntity[,] map)
        {
            if (Sender.Element.Type == BulletType.Spray)
            {
                Direction = Sender.Direction;
                X = Sender.GunPosition.X;
                Y = Sender.GunPosition.Y;
            }
            else
            {
                X += Game.MovementForDirection[Direction].X * MoveSpeed;
                Y += Game.MovementForDirection[Direction].Y * MoveSpeed;
            }
        }
    }
}
