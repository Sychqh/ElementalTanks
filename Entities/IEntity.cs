namespace ElementalTanks
{
    public interface IEntity
    {
        public IElement Element { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; }
        public int Height { get; }
        public void Update(IEntity[,] map);
    }
}
