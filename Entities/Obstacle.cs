namespace ElementalTanks
{
    public class Obstacle : IEntity
    {
        public IElement Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; }
        public int Height { get; }

        public Obstacle(int x, int y, IElement element)
        {
            X = x;
            Y = y;
            Direction = "Up";
            Width = Height = 80;
            Element = element;
            Direction = "Up";
        }

        public void Update(IEntity[,] map) { }
    }
}
