using System.Drawing;

namespace ElementalTanks
{
    class Enemy : ITank
    {
        private readonly Player player;

        public IElement Element { get; set; }
        public double Health { get; set; }
        public int MoveSpeed { get; }

        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; }
        public int Height { get; }

        public int UpMovement { get; set; }
        public int DownMovement { get; set; }
        public int LeftMovement { get; set; }
        public int RightMovement { get; set; }

        public bool IsShooting { get; set; }
        public int ShootingTime { get; private set; }

        public Point GunPosition => Direction switch
        {
            "Up" => new Point(X + 6, Y - Element.Height + 6),
            "Down" => new Point(X + 6, Y + Height - 6),
            "Left" => new Point(X - Element.Width + 6, Y + 6),
            "Right" => new Point(X + Width - 6, Y + 6),
            _ => Point.Empty
        };

        public Enemy(int x, int y, IElement element, Player player, int moveSpeed)
        {
            this.player = player;
            X = x;
            Y = y;
            Width = Height = 80;
            Element = element;
            Health = 100;
            MoveSpeed = moveSpeed;
            Direction = "Up";
        }

        public void Update(IEntity[,] map)
        {
            Move(Direction);
            if (CanMove(map))
            {
                X += RightMovement - LeftMovement;
                Y += DownMovement - UpMovement;
            }

            if (IsShooting)
                ShootingTime++;
            
            if (ShootingTime > 100 && Element.Type == BulletType.Spray
                || ShootingTime > 1 && Element.Type == BulletType.Projectile)
            {
                ShootingTime = 0;
                IsShooting = false;
            }
        }

        public void Move(string direction)
        {
            if (Y > player.Y)
            {
                Direction = "Up";
                UpMovement = MoveSpeed;
                DownMovement = LeftMovement = RightMovement = 0;
            }
            if (Y < player.Y)
            {
                Direction = "Down";
                DownMovement = MoveSpeed;
                UpMovement = LeftMovement = RightMovement = 0;
            }
            if (X > player.X)
            {
                Direction = "Left";
                LeftMovement = MoveSpeed;
                UpMovement = DownMovement = RightMovement = 0;
            }
            if (X < player.X)
            {
                Direction = "Right";
                RightMovement = MoveSpeed;
                UpMovement = DownMovement = LeftMovement = 0;
            }
        }

        public void TakeDamage(Bullet bullet)
        {
            if (bullet.Sender is Player)
                Health -= bullet.Element.GetFinalDamage(Element);
        }

        public bool CanMove(IEntity[,] map)
        {
            switch (Direction)
            {
                case "Left" when X > 0:
                    for (var y = Y; y < Y + Height; y++)
                        if (!(map[X - MoveSpeed, y] is null))
                            return false;
                    return true;
                case "Right" when X + Width + MoveSpeed < map.GetLength(0):
                    for (var y = Y; y < Y + Height; y++)
                        if (!(map[X + Width + MoveSpeed, y] is null))
                            return false;
                    return true;
                case "Up" when Y > 0:
                    for (var x = X; x < X + Width; x++)
                        if (!(map[x, Y - MoveSpeed] is null))
                            return false;
                    return true;
                case "Down" when Y + Height + MoveSpeed < map.GetLength(1):
                    for (var x = X; x < X + Width; x++)
                        if (!(map[x, Y + Width + MoveSpeed] is null))
                            return false;
                    return true;
                default:
                    return false;
            }
        }

        public Bullet Shoot() => new Bullet(this);
    }
}
