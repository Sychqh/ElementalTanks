using System.Drawing;

namespace ElementalTanks
{
    public interface ITank : IEntity
    {
        public double Health { get; set; }
        public int MoveSpeed { get; }
        public Point GunPosition { get; }
        public int UpMovement { get; set; }
        public int DownMovement { get; set; }
        public int LeftMovement { get; set; }
        public int RightMovement { get; set; }
        public bool IsShooting { get; set; }

        public void Move(string direction);
        public void TakeDamage(Bullet bullet);
        public bool CanMove(IEntity[,] map);
        public Bullet Shoot();
    }
}
